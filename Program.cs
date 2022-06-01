using Discord;
using Discord.Commands;
using Discord.WebSocket;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using System;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;

using eNME.Services;
using eNME.Procgen;

namespace eNME
{
    class Program
    {
        static void Main( string[] args )
                   => new Program().MainAsync().GetAwaiter().GetResult();

        // list of words we can use to make the image search vaguely more interesting / weird / on brand
        static public readonly List<String> SearchMixture = new List<string>()
        {
            " audio album",
            " thoughts vision",
            " concept art",
            " illustration ink",
            " product idea",
            " fan art detailed",
            " radio station promotion",
        };

        public async Task MainAsync()
        {
            RNG32 rng = new RNG32( Hash.Reduce64To32( (ulong) DateTime.Now.Ticks ) );

            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddUserSecrets<Program>()
                .Build();

            {
                var suggestions = ProceduralGenre.Generate( rng, 10 );
                foreach ( var v in suggestions )
                    Console.WriteLine( v );
            }

#if BINGLY

            BingImageSearch.SubscriptionKey = config.GetConnectionString( "Bing1" );
            HttpClient xo = new HttpClient();


            var hatchStyles = Enum.GetValues( typeof( HatchStyle ) );

            int index = 0;
            for ( int i = 0; i < 2; i++ )
            {
                var suggestions = ProceduralGenre.Generate( rng, 5 );
                foreach ( var v in suggestions)
                {
                    var foo = BingImageSearch.RunQuery(v + rng.RandomItemFrom(SearchMixture) );
                    if ( foo.Count < 5 )
                        continue;

                    using ( Bitmap suggestionImage = new Bitmap( 768, 768 ) )
                    {
                        for (; ; )
                        {
                            try
                            {
                                string chosenImage = rng.RandomItemFrom( foo );
                                var resp = await xo.GetAsync(chosenImage);

                                if ( resp.StatusCode != System.Net.HttpStatusCode.OK )
                                    throw new Exception( "http failed" );

                                var imgg = await resp.Content.ReadAsStreamAsync();

                                using ( Bitmap fromStream = new Bitmap( imgg ) )
                                using ( Font overlayFont = new Font( "BigNoodleTitling", rng.GenFloat( 48.0f, 78.0f ), FontStyle.Bold ) )
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

                                    g.DrawImage( fromStream, 0, 0, 768, 768 );

                                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

                                    var roughSize = g.MeasureString( v, overlayFont, new SizeF( 768 - 32, 768 - 32 ) );
                                    g.FillRectangle( new HatchBrush( (HatchStyle)hatchStyles.GetValue( rng.GenInt32( 0, hatchStyles.Length - 1 ) ), System.Drawing.Color.Black, System.Drawing.Color.Transparent ), 16.0f, 16.0f, roughSize.Width, roughSize.Height * 3 );
                                    g.FillRectangle( new HatchBrush( (HatchStyle)hatchStyles.GetValue( rng.GenInt32( 0, hatchStyles.Length - 1 ) ), System.Drawing.Color.Black, System.Drawing.Color.Transparent ), 35.0f, 25.0f, roughSize.Width * 2, roughSize.Height );
                                    g.FillRectangle( new HatchBrush( (HatchStyle)hatchStyles.GetValue( rng.GenInt32( 0, hatchStyles.Length - 1 ) ), System.Drawing.Color.Black, System.Drawing.Color.Transparent ), 8.0f, 60.0f, roughSize.Width, roughSize.Height / 2 );

                                    g.DrawString( v, overlayFont, Brushes.White, new RectangleF( 32, 32, 768, 768 ) );
                                    g.DrawString( v, overlayFont, Brushes.HotPink, new RectangleF( 39, 39, 768, 768 ) );
                                    g.DrawString( v, overlayFont, Brushes.Cyan, new RectangleF( 36, 36, 768, 768 ) );
                                    g.DrawString( v, overlayFont, Brushes.Black, new RectangleF( 34, 34, 768, 768 ) );
                                }
                                break;
                            }
                            catch (Exception ex)
                            {

                            }
                        }

                        suggestionImage.Save( $"{index}_suggestion.jpg" );
                    }

                    index++;

                    Console.WriteLine( v );
                }
            }

#endif 



#if NOO
            var discordBotToken = config.GetConnectionString( "DiscordBotToken" );

            // You should dispose a service provider created using ASP.NET
            // when you are finished using it, at the end of your app's lifetime.
            // If you use another dependency injection framework, you should inspect
            // its documentation for the best way to do this.
            using ( var services = ConfigureServices() )
            {
                var client = services.GetRequiredService<DiscordSocketClient>();

                client.Log += LogAsync;
                services.GetRequiredService<CommandService>().Log += LogAsync;

                // Tokens should be considered secret data and never hard-coded.
                // We can read from the environment variable to avoid hard coding.
                await client.LoginAsync( TokenType.Bot, discordBotToken );
                await client.StartAsync();

                // Here we initialize the logic required to register our commands.
                await services.GetRequiredService<CommandHandlingService>().InitializeAsync();

                await Task.Delay( Timeout.Infinite );
            }
#endif
        }

        private Task LogAsync( LogMessage log )
        {
            Console.WriteLine( log.ToString() );

            return Task.CompletedTask;
        }

        private ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton<DiscordSocketClient>()
                .AddSingleton<CommandService>()
                .AddSingleton<CommandHandlingService>()
                .AddSingleton<HttpClient>()
                .AddSingleton<GenreService>()
                .BuildServiceProvider();
        }
    }
}
