
using Discord;
using Discord.Commands;

using System.IO;
using System.Threading.Tasks;

using eNME.Services;

namespace eNME.Modules
{
    public class PublicModule : ModuleBase<SocketCommandContext>
    {
        public GenreService GenreService { get; set; }

        [Command( "genre" )]
        public async Task GenerateGenre()
        {
            await Context.Channel.SendMessageAsync( GenreService.GenerateMusicGenre() );
        }
    }
}


