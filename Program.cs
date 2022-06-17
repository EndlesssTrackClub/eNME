using Discord;
using Discord.Commands;
using Discord.WebSocket;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

using eNME.Services;
using eNME.Procgen;
using System.Threading;
using System.IO;

namespace eNME
{
    class Program
    {
        static void Main( string[] args )
                   => new Program().MainAsync().GetAwaiter().GetResult();

        static ulong ChosenGuildSID = 0;
        static ulong ChosenChannelSID = 0;

        static ServiceProvider provisionedServices;

        public async Task MainAsync()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<Program>()
                .Build();

            BingImageSearch.SubscriptionKey = config.GetConnectionString( "Bing1" );

#if QUICKTEST
            RNG32 rng = new RNG32( Hash.Reduce64To32( (ulong) DateTime.Now.Ticks ) );
            var sourceData = new ProceduralGenre.ProceduralSourceData( rng );

            var defaultGenerations = new List<ProceduralGenre.GenerationSpirit>() {
                    ProceduralGenre.GenerationSpirit.Safe,
                    ProceduralGenre.GenerationSpirit.Adventurous,
                    ProceduralGenre.GenerationSpirit.Adventurous,
                    ProceduralGenre.GenerationSpirit.Untethered,
                };

            for ( int i = 0; i < 1; i++ )
            {
                var suggestions = ProceduralGenre.Generate( sourceData, rng, defaultGenerations );
                foreach ( var v in suggestions )
                    Console.WriteLine( v );
            }
#endif

            var guildSID    = config.GetConnectionString( "DiscordGuildSID" );
            var channelSID  = config.GetConnectionString( "DiscordChannelSID" );

            if ( !string.IsNullOrEmpty( guildSID ) && 
                 !string.IsNullOrEmpty( channelSID ) )
            {
                ChosenGuildSID = ulong.Parse( guildSID );
                ChosenChannelSID = ulong.Parse( channelSID );
            }

            var discordBotToken = config.GetConnectionString( "DiscordBotToken" );

            using ( provisionedServices = ConfigureServices() )
            {
                var client = provisionedServices.GetRequiredService<DiscordSocketClient>();

                client.Log += LogAsync;
                provisionedServices.GetRequiredService<CommandService>().Log += LogAsync;

                await client.LoginAsync( TokenType.Bot, discordBotToken );
                await client.StartAsync();

                await provisionedServices.GetRequiredService<CommandHandlingService>().InitializeAsync();

                if ( ChosenGuildSID != 0 && ChosenChannelSID != 0 )
                    client.GuildAvailable += Client_GuildAvailable;

                await Task.Delay( Timeout.Infinite );
            }
        }

        private async Task Client_GuildAvailable( SocketGuild arg )
        {
            if ( arg.Id == ChosenGuildSID )
            {
                Console.WriteLine( " -- generating new genre as default, posting to chosen channel -- " );

                var genreChannel =  arg.GetChannel( ChosenChannelSID ) as ISocketMessageChannel;
                if ( genreChannel != null )
                {
                    try
                    {
                        var rng = new RNG32( Hash.Reduce64To32( (ulong)DateTime.Now.Ticks ) );

                        var potentialSpirits = Enum.GetNames( typeof( ProceduralGenre.GenerationSpirit ) );
                        var chosenSpiritName = rng.RandomItemFrom( potentialSpirits );

                        var chosenSpirit = Enum.Parse<ProceduralGenre.GenerationSpirit>( chosenSpiritName );
                        List<ProceduralGenre.GenerationSpirit> choices = new List<ProceduralGenre.GenerationSpirit>()
                        {
                            chosenSpirit
                        };

                        var gs = provisionedServices.GetService<Services.GenreService>();

                        await genreChannel.SendMessageAsync( $"The new genre will be .. *{chosenSpirit}*" );

                        List < GenreSuggestion > suggestions = await gs.GenerateSelectionOfGenres(choices);
                        foreach ( var sg in suggestions )
                        {
                            using ( var imageStream = new MemoryStream( 1024 * 256 ) )
                            {
                                Utils.CompressBitmapToStream( imageStream, sg.Image, 97L );
                                await genreChannel.SendFileAsync( imageStream, sg.Genre.ToLower().Replace( ' ', '_' ) + ".jpg" );
                            }
                        }

                        gs.SerializeState();
                    }
                    catch ( Exception ex )
                    {
                        Console.WriteLine( "Failed to generate a genre on demand" );
                        Console.WriteLine( ex.Message );
                        Console.WriteLine( ex.StackTrace );
                        Console.WriteLine( ex.InnerException );
                    }
                }
                else
                {
                    Console.WriteLine( "Could not resolve chosen channel SID as ISocketMessageChannel" );
                }
            }

            await Task.Delay( 2000 );

            Environment.Exit( 0 );

            await Task.CompletedTask;
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
