using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using eNME.Procgen;

namespace eNME.Services
{
    public class GenreService
    {
        private readonly HttpClient _http;

        public GenreService( HttpClient http )
            => _http = http;

        public string GenerateMusicGenre()
        {
            RNG32 rng = new RNG32( Hash.Reduce64To32( (ulong) DateTime.Now.Ticks ) );

            var suggestions = ProceduralGenre.Generate( rng, 5 );
            
            return String.Join( "\n", suggestions );
        }
    }
}