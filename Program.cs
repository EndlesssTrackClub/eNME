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
using System.Drawing;
using System.IO;

namespace eNME
{
    class Program
    {
        static void Main( string[] args )
                   => new Program().MainAsync().GetAwaiter().GetResult();



        public async Task MainAsync()
        {
            RNG32 rng = new RNG32( Hash.Reduce64To32( (ulong) DateTime.Now.Ticks ) );

            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddUserSecrets<Program>()
                .Build();

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

            BingImageSearch.SubscriptionKey = config.GetConnectionString( "Bing1" );


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
