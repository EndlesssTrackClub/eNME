using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using eNME.Procgen;

namespace eNME.Services
{
    public class GenreSuggestion
    {
        public Bitmap Image { get; set; }
        public String Genre { get; set; }
    }

    public class GenreService
    {
        private readonly HttpClient _http;
        
        private readonly RNG32 _rng;
        private readonly ProceduralGenre.ProceduralSourceData _pds;

        private readonly Array _hatchStyles;

        public GenreService( HttpClient http )
        { 
            _http = http;
            _http.Timeout = new TimeSpan( 0, 0, 4 );

            _rng = new RNG32( Hash.Reduce64To32( (ulong) DateTime.Now.Ticks ) );
            _pds = new ProceduralGenre.ProceduralSourceData( _rng );
            _pds.LoadFromDisk();

            _hatchStyles = Enum.GetValues( typeof( HatchStyle ) );
        }

        public void SerializeState()
        {
            _pds.SaveToDisk();
        }

        // list of words we can use to make the image search vaguely more interesting / weird / on brand
        static internal readonly List<String> SearchMixture = new List<string>()
        {
            "",
            " music genre",
            " wallpaper",
            " audio album",
            " thoughts vision",
            " concept art",
            " product idea",
            " ai generated art",
            " film art",
        };

        public string GenerateNewGender()
        {
            return (_pds.Prefix.PopEnd() + " " + _pds.Instruments.PopEnd());
        }

        public async Task<List<GenreSuggestion>> GenerateSelectionOfGenres( List<ProceduralGenre.GenerationSpirit> choices )
        {
            var suggestions = ProceduralGenre.Generate( _pds, _rng, choices );
            List<GenreSuggestion> results = new List<GenreSuggestion>(suggestions.Count);

            foreach ( var v in suggestions )
            {
                string searchMix;
                List<string> bingResults;
                for (; ; )
                {
                    string genreText = v;

                    try
                    {
                        searchMix = _rng.RandomItemFrom( SearchMixture );

                        bingResults = BingImageSearch.RunQuery( genreText + searchMix );
                        if ( bingResults.Count > 5 )
                            break;
                    }
                    catch ( Exception ex )
                    {
                        Console.WriteLine( " - bing failed -" );
                    }

                    await Task.Delay( 250 );
                    // increase chance of a hit by removing the genre. nvm. better to actually finish executing.

                    genreText = _rng.RandomItemFrom( SearchMixture );
                }

                Bitmap suggestionImage = new Bitmap( 768, 768 );
                {
                    float baseScale = 1.0f;
                    if ( v.Length <= 25 )
                        baseScale *= 1.25f;

                    for ( ; ; )
                    {
                        try
                        {
                            string chosenImage = _rng.RandomItemFrom( bingResults );
                            var resp = await _http.GetAsync(chosenImage);

                            if ( resp.StatusCode != System.Net.HttpStatusCode.OK )
                                throw new Exception( "http failed" );

                            var imgg = await resp.Content.ReadAsStreamAsync();

                            var fontSize = _rng.GenFloat( 52.0f, 70.0f ) * baseScale;

                            using ( Bitmap fromStream = new Bitmap( imgg ) )
                            using ( Font overlayFont = new Font( "BigNoodleTitling", fontSize, FontStyle.Bold ) )
                            using ( Graphics g = Graphics.FromImage( suggestionImage ) )
                            {
                                g.Clear( System.Drawing.Color.DarkMagenta );
                                g.CompositingQuality = CompositingQuality.HighQuality;
                                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                                g.SmoothingMode = SmoothingMode.HighQuality;

                                g.TranslateTransform( (768 / 2), (768 / 2) );
                                g.RotateTransform( 22.0f );
                                g.DrawImage( fromStream, -768, -768, 768 * 2, 768 * 2 );
                                g.ResetTransform();

                                if ( _rng.GenBool() )
                                    g.ScaleTransform( -1.0f, 1.0f );
                                g.DrawImage( fromStream, 0, 0, 768, 768 );
                                g.ResetTransform();

                                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

                                Procgen.HSV block1 = new HSV( _rng.GenFloat(), 1.0f, 1.0f );

                                var thickBlockH = _rng.GenFloat();
                                Procgen.HSV thickBlockA = new HSV( thickBlockH, 1.0f, 1.0f );
                                Procgen.HSV thickBlockB = new HSV( thickBlockH, 0.6f, 1.0f );

                                var roughSize = g.MeasureString( v, overlayFont, new SizeF( 768 - 32, 768 - 32 ) );

                                g.RotateTransform( _rng.GenFloat( -20.0f, 20.0f ) );
                                g.FillRectangle( new HatchBrush( (HatchStyle)_hatchStyles.GetValue( _rng.GenInt32( 0, _hatchStyles.Length - 1 ) ), System.Drawing.Color.Black, System.Drawing.Color.Transparent ), 16.0f, 16.0f, roughSize.Width, roughSize.Height * 3 );
                                g.ResetTransform();

                                g.RotateTransform( _rng.GenFloat( -10.0f, 10.0f ) );
                                g.FillRectangle( new HatchBrush( (HatchStyle)_hatchStyles.GetValue( _rng.GenInt32( 0, _hatchStyles.Length - 1 ) ), block1.ToColor( _rng.GenFloat( 0.6f, 0.9f ) ), System.Drawing.Color.Transparent ), 35.0f, 25.0f, roughSize.Width * 2, roughSize.Height );
                                g.ResetTransform();

                                g.RotateTransform( _rng.GenFloat( -3.0f, 3.0f ) );
                                g.FillRectangle( new SolidBrush( thickBlockA.ToColor( _rng.GenFloat( 0.6f, 0.9f ) ) ), 8.0f, 60.0f, roughSize.Width * baseScale * _rng.GenFloat( 0.5f, 1.5f ), roughSize.Height * _rng.GenFloat( 0.4f, 0.8f ) );
                                g.ResetTransform();
                                g.RotateTransform( _rng.GenFloat( -15.0f, 15.0f ) );
                                g.FillRectangle( new SolidBrush( thickBlockB.ToColor( _rng.GenFloat( 0.4f, 0.7f ) ) ), 0.0f, 10.0f, roughSize.Width * baseScale * _rng.GenFloat( 0.9f, 1.8f ), roughSize.Height * _rng.GenFloat( 0.8f, 1.6f ) );
                                g.ResetTransform();

                                g.RotateTransform( _rng.GenFloat( -3.0f, 3.0f ) );
                                g.DrawString( v, overlayFont, Brushes.White, new RectangleF( 32, 32, 768, 768 ) );
                                g.DrawString( v, overlayFont, Brushes.HotPink, new RectangleF( 39, 39, 768, 768 ) );
                                g.DrawString( v, overlayFont, Brushes.Cyan, new RectangleF( 36, 36, 768, 768 ) );
                                g.DrawString( v, overlayFont, Brushes.Black, new RectangleF( 34, 34, 768, 768 ) );
                                g.ResetTransform();
                            }
                            break;
                        }
                        catch ( Exception ex )
                        {
                            Console.WriteLine( " - image fetch failed -" );
                        }
                    }

                    results.Add( new GenreSuggestion { Image = suggestionImage, Genre = v } );
                }

                Console.WriteLine( $"{v}  [{searchMix} ]" );
            }

            return results;
        }
    }
}