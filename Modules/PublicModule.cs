
using Discord;
using Discord.Commands;

using System.Linq;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Threading.Tasks;

using eNME.Services;

namespace eNME.Modules
{
    public class PublicModule : ModuleBase<SocketCommandContext>
    {
        public GenreService GenreService { get; set; }


        public static void CompressBitmapToStream( Stream stream, Bitmap image, long quality )
        {
            using ( EncoderParameters encoderParameters = new EncoderParameters( 1 ) )
            using ( EncoderParameter encoderParameter = new EncoderParameter( Encoder.Quality, quality ) )
            {
                ImageCodecInfo codecInfo = ImageCodecInfo.GetImageDecoders().First(codec => codec.FormatID == System.Drawing.Imaging.ImageFormat.Jpeg.Guid);
                encoderParameters.Param[0] = encoderParameter;
                image.Save( stream, codecInfo, encoderParameters );
                stream.Seek( 0, SeekOrigin.Begin );
            }
        }

        [Command( "genre" )]
        public async Task GenerateGenre( params string[] objects )
        {
            List < ProceduralGenre.GenerationSpirit > choices = new List<ProceduralGenre.GenerationSpirit>();
            foreach ( var c in objects )
            {
                switch ( c.ToLower() )
                {
                    case "low": { choices.Add( ProceduralGenre.GenerationSpirit.Safe ); break; }
                    case "medium": { choices.Add( ProceduralGenre.GenerationSpirit.Adventurous ); break; }
                    case "high": { choices.Add( ProceduralGenre.GenerationSpirit.Untethered ); break; }
                }
                if ( choices.Count >= 4 )
                    break;

            }

            if ( choices.Count == 0 )
            {
                await Context.Channel.SendMessageAsync( "didnt see any choices there. use low, medium or high to choose your untethered level" );
                return;
            }

            var plural = choices.Count > 1 ? "s" : "";
            await Context.Channel.SendMessageAsync( $"Okay, imagining {choices.Count} hot new sound{plural}..." );

            List< GenreSuggestion > suggestions = await GenreService.GenerateSelectionOfGenres(choices);
            foreach ( var sg in suggestions )
            {
                using ( var imageStream = new MemoryStream(1024 * 128) )
                {
                    CompressBitmapToStream( imageStream, sg.Image, 85L );
                    await Context.Channel.SendFileAsync( imageStream, sg.Genre.ToLower().Replace(' ', '_') + ".jpg" );
                }
            }
        }

        [Command( "gender" )]
        public async Task GenerateGender()
        {
            var newGender = GenreService.GenerateNewGender();
            await Context.Channel.SendMessageAsync( $"Ok {Context.User.Username}, your new gender is : {newGender}" );
        }
    }
}


