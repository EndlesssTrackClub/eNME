using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

using eNME.Procgen;
using System.IO;

namespace eNME
{
    public static class ProceduralGenre
    {
        public enum GenerationSpirit
        {
            Safe,
            Adventurous,
            Untethered
        }

        // -------------------------------------------------------------------------------------------------------------
        public class ProceduralSourceData
        {
            static public readonly List<String> _PreSubStyle = new List<string>()
            {
                "Anti-",
                "New",
                "Old",
                "Pre-",
                "Post-",
                "Nu-",
                "Non-",
                "Neo",
                "Semi-",
                "Quasi-",
            };

            static public readonly List<String> _SubStyle = new List<string>()
            {
                "Abstract",
                "Abattoir",
                "Aggressive",
                "Alien",
                "Alternative",
                "Anarchist",
                "Ancient",
                "Antiquated",
                "Artificial",
                "Boomer",
                "Bloated",
                "Bubblegum",
                "Carbon-Neutral",
                "Catholic",
                "Children's",
                "Christian",
                "Classic",
                "Comedy",
                "Contemporary",
                "Cybernetic",
                "Depressing",
                "Distant",
                "Doomer",
                "Downtempo",
                "Dystopic",
                "Emotional",
                "Emotionless",
                "Energetic",
                "Environmental",
                "Erotic",
                "Evil",
                "Experimental",
                "Expressionist",
                "Fallen",
                "Fantasy",
                "Forbidden",
                "Kawaii",
                "Kindergarten",
                "Garbage",
                "Hardcore",
                "Harsh",
                "Haunted",
                "Impossible",
                "Industrial",
                "Ironic",
                "Mainstream",
                "Melancholy",
                "Minimalist",
                "Maximalist",
                "Misunderstood",
                "Modern",
                "Monarchist",
                "Nuevo",
                "Orchestral",
                "School",
                "Patriotic",
                "Progressive",
                "Psychedelic",
                "Reflective",
                "Reclusive",
                "Reintegrated",
                "Residential",
                "Romantic",
                "Rural",
                "Satanic",
                "Softcore",
                "Suburban",
                "Symphonic",
                "Technical",
                "Tiresome",
                "Traditional",
                "Underground",
                "Unrefined",
                "Upbeat",
                "Uptempo",
                "Vintage",
                "Willowy",
                "Zoomer",
            };

            static public readonly List<String> _Instruments = new List<string>()
            {
                "Accordion",
                "Bagpipe",
                "Banjo",
                "Bassoon",
                "Bugle",
                "Cello",
                "Choral",
                "Clarinet",
                "Clavichord",
                "Cowbell",
                "Didgeridoo",
                "Euphonium",
                "Flugelhorn",
                "Flute",
                "Glockenspiel",
                "Gong",
                "Guitar",
                "Harp",
                "Harpsichord",
                "Honky-Tonk",
                "Hurdy-Gurdy",
                "Kalimba",
                "Kazoo",
                "Klaxon",
                "Mellotron",
                "Oboe",
                "Otomatone",
                "Piano",
                "Piccolo",
                "Pipe Organ",
                "Recorder",
                "Saxophone",
                "Sousaphone",
                "Synthesiser",
                "Tambourine",
                "Telephone",
                "Theorbo",
                "Theremin",
                "Timpani",
                "Trombone",
                "Trumpet",
                "Tuba",
                "Ukulele",
                "Violin",
                "Vocal",
                "Vuvuzela",
                "Wind Chime",
                "Wurlitzer",
                "Zither",
            };

            static public readonly List<String> _Locales = new List<string>()
            {
                "African",
                "British",
                "Canadian",
                "Eastern",
                "European",
                "French",
                "G-",
                "German",
                "Highland",
                "Irish",
                "J-",
                "K-",
                "Latin",
                "Moon",
                "Northern",
                "Regional",
                "Russian",
                "Southern",
                "Texan",
                "Tropical",
                "Welsh",
            };

            static public readonly List<String> _Ages = new List<string>()
            {
                "60s",
                "70s",
                "80s",
                "90s",
                "Bronze Age",
                "Dark Age",
                "Distant Future",
                "Forgotten",
                "Jurassic",
                "Medieval",
                "Near Future",
                "Next Phase",
                "Pandemic",
                "Timeless",
                "Victorian",
            };

            static public readonly List<String> _Prefix = new List<string>()
            {
                "Adult",
                "Attack",
                "Big",
                "Brit",
                "Chill",
                "Cheeky",
                "Club",
                "Cool",
                "Core",
                "Crust",
                "Cypher",
                "Cyber",
                "Dark",
                "Deep",
                "Death",
                "Doom",
                "Drum",
                "Electro",
                "Euro",
                "Filth",
                "Future",
                "Fuzz",
                "Fury",
                "Glam",
                "Glitch",
                "Grind",
                "Grin",
                "Hard",
                "Hyper",
                "Jerk",
                "Jingle",
                "Laser",
                "Limp",
                "LoFi",
                "Love",
                "Macro",
                "Micro",
                "Math",
                "Nerd",
                "Otter",
                "Past",
                "Pirate",
                "Quiet",
                "Real",
                "Scream",
                "Sleek",
                "Small",
                "Smooth",
                "Speed",
                "Sour",
                "Steam",
                "Step",
                "Sweet",
                "Synth",
                "Tech",
                "Think",
                "Trad",
                "Ultra",
                "Vague",
                "Vapor",
                "Wave",
                "Witch",
                "Whisper",
                "Wonky",
            };

            static public readonly List<String> _Suffix = new List<string>()
            {
                "beam",
                "bass",
                "club",
                "core",
                "dub",
                "down",
                "edge",
                "feed",
                "feel",
                "gas",
                "grind",
                "mode",
                "noise",
                "pop",
                "pulse",
                "punk",
                "rhythm",
                "slam",
                "step",
                "sweep",
                "treble",
                "track",
                "vibe",
                "wash",
                "wave",
            };

            static public readonly List<String> _CommonStyle = new List<string>()
            {
                "Ballad",
                "Blues",
                "Bop",
                "Breaks",
                "Country",
                "Crunk",
                "Dance",
                "Drone",
                "Drum and Bass",
                "Dub",
                "Folk",
                "Funk",
                "Garage",
                "Gospel",
                "Hip-Hop",
                "House",
                "Indie",
                "Jazz",
                "Jungle",
                "Metal",
                "Pop",
                "Punk",
                "Ragtime",
                "Reggae",
                "Rap",
                "Rock",
                "Salsa",
                "Silence",
                "Ska",
                "Soul",
                "Swing",
                "Techno",
                "Trance",
                "Trap",
            };

            public ProceduralSourceData( RNG32 rng )
            {
                localRng = new RNG32( rng.GenUInt32() );
            }

            private class OnDisk
            {
                public List<String> subStyle    { get; set; }
                public List<String> instruments { get; set; }
                public List<String> locales     { get; set; }
                public List<String> ages        { get; set; }
                public List<String> prefix      { get; set; }
                public List<String> suffix      { get; set; }
                public List<String> commonStyle { get; set; }
            }

            static private string PersistenceFilename = @"ProceduralSourceData.json";

            public void SaveToDisk()
            {
                try
                {
                    OnDisk od = new OnDisk();
                    od.subStyle = this.subStyle;
                    od.instruments = this.instruments;
                    od.locales = this.locales;
                    od.ages = this.ages;
                    od.prefix = this.prefix;
                    od.suffix = this.suffix;
                    od.commonStyle = this.commonStyle;

                    string json = JsonConvert.SerializeObject(od, Formatting.Indented);
                    File.WriteAllText( PersistenceFilename, json );
                }
                catch ( Exception ex )
                {
                    Console.WriteLine( "PSD::SaveToDisk failed\n" + ex.Message );
                }
                Console.WriteLine( "PSD::SaveToDisk complete" );
            }

            public void LoadFromDisk()
            {
                if ( File.Exists( PersistenceFilename ) )
                {
                    try
                    {
                        var jsonData = File.ReadAllText( PersistenceFilename );
                        var odResult = JsonConvert.DeserializeObject<OnDisk>( jsonData );

                        this.subStyle = odResult.subStyle;
                        this.instruments = odResult.instruments;
                        this.locales = odResult.locales;
                        this.ages = odResult.ages;
                        this.prefix = odResult.prefix;
                        this.suffix = odResult.suffix;
                        this.commonStyle = odResult.commonStyle;
                    }
                    catch ( Exception ex )
                    {
                        Console.WriteLine( "PSD::LoadFromDisk failed\n" + ex.Message );
                    }
                }
                Console.WriteLine( "PSD::LoadFromDisk complete" );
            }

            RNG32   localRng;

            List<String> ReseedFrom( List<String> input )
            {
                List<String> result = new List<string>( input );
                localRng.ShuffleList( result );
                result.Reverse();
                localRng.ShuffleList( result );
                return result;
            }

            public List<String> SubStyle        { get { if ( subStyle.Count      == 0 ) subStyle     = ReseedFrom( _SubStyle );      return subStyle; } }
            public List<String> Instruments     { get { if ( instruments.Count   == 0 ) instruments  = ReseedFrom( _Instruments );   return instruments; } }
            public List<String> Locales         { get { if ( locales.Count       == 0 ) locales      = ReseedFrom( _Locales );       return locales; } }
            public List<String> Ages            { get { if ( ages.Count          == 0 ) ages         = ReseedFrom( _Ages );          return ages; } }
            public List<String> Prefix          { get { if ( prefix.Count        == 0 ) prefix       = ReseedFrom( _Prefix );        return prefix; } }
            public List<String> Suffix          { get { if ( suffix.Count        == 0 ) suffix       = ReseedFrom( _Suffix );        return suffix; } }
            public List<String> CommonStyle     { get { if ( commonStyle.Count   == 0 ) commonStyle  = ReseedFrom( _CommonStyle );   return commonStyle; } }

            private List<String> subStyle = new List<string>();
            private List<String> instruments = new List<string>();
            private List<String> locales = new List<string>();
            private List<String> ages = new List<string>();
            private List<String> prefix = new List<string>();
            private List<String> suffix = new List<string>();
            private List<String> commonStyle = new List<string>();
        }


        static public readonly List<String> Bindings = new List<string>()
        {
            " ",
            " and ",
            "'n'",
            "-y ",
            "/",
            "-",
            "-ful ",
        };


        // -------------------------------------------------------------------------------------------------------------
        internal class GenResult
        {
            public GenResult( string words, int complex )
            {
                Words = words;
                Complexity = complex;
            }

            public string Words;
            public int Complexity;
        }


        // -------------------------------------------------------------------------------------------------------------
        internal interface Generator
        {
            public GenResult Generate( ProceduralSourceData data, RNG32 rng, GenerationSpirit spirit );
        }

        // -------------------------------------------------------------------------------------------------------------
        // plain combinations of a style and potentially some prefixing / bonus style
        // eg.
        //      Hip-Hop
        //      Hip-Hop'n'Ska
        //      Hyper Hip-Hop
        //
        internal class NormalStyle : Generator
        {
            public GenResult Generate( ProceduralSourceData data, RNG32 rng, GenerationSpirit spirit )
            {
                var baseStyle = data.CommonStyle.PopEnd();
                int resultCx = 1;

                var spDoubleStyleChance = 0;
                switch (spirit)
                {
                    case GenerationSpirit.Adventurous:
                        spDoubleStyleChance = 40;
                        break;
                    case GenerationSpirit.Untethered:
                        spDoubleStyleChance = 85;
                        break;
                }

                if ( rng.WithPercentageChance( spDoubleStyleChance ) )
                {
                    resultCx++;

                    // double-up the styles for a confusing treat
                    if ( rng.WithPercentageChance( 25 ) )
                    {
                        var doubledStyle = data.CommonStyle.PopEnd();

                        // catch stuff like "Drum and Bass" and always use another "and" to fuse them (eg. Drum and Bass and Soul)
                        if ( baseStyle.ToLower().Contains( "and" ) ||
                             doubledStyle.ToLower().Contains( "and" ) )
                        {
                            baseStyle += " and ";
                        }
                        else
                        {
                            // pick some other kind of binding, like "-" or "'n'"
                            baseStyle += rng.RandomItemFrom( Bindings );
                        }

                        baseStyle += doubledStyle;
                    }
                    // bolt on a random style prefix, like "Wonky" or "Vapor"
                    else
                    {
                        baseStyle = data.Prefix.PopEnd() + " " + baseStyle;
                    }
                }

                return new GenResult( baseStyle, resultCx );
            }
        }

        // -------------------------------------------------------------------------------------------------------------
        // create hot new trends by crushing a vague style Prefix and Suffix together, eg. Darkwave, Hardpop
        internal class FusedStyle : Generator
        {
            public GenResult Generate( ProceduralSourceData data, RNG32 rng, GenerationSpirit spirit )
            {
                int resultCx = 1;

                var spDoubleStyleChance = 0;
                switch ( spirit )
                {
                    case GenerationSpirit.Adventurous:
                        spDoubleStyleChance = 30;
                        break;
                    case GenerationSpirit.Untethered:
                        spDoubleStyleChance = 80;
                        break;
                }

                string result = "";

                // potentially seed the result with another prefix too, eg. Hard Darkwave
                if ( rng.WithPercentageChance( spDoubleStyleChance ) )
                {
                    result += ( data.Prefix.PopEnd() + " " );
                    resultCx++;
                }

                return new GenResult( result + data.Prefix.PopEnd() + data.Suffix.PopEnd(), resultCx );
            }
        }

        // -------------------------------------------------------------------------------------------------------------
        internal class Complicator : Generator
        {
            public GenResult Generate( ProceduralSourceData data, RNG32 rng, GenerationSpirit spirit )
            {
                int resultCx = 0;

                string result = "";

                // in Safe mode, keep things simple with maybe just one or either Ages or Locales
                if ( spirit == GenerationSpirit.Safe )
                {
                    if ( rng.WithPercentageChance( 50 ) )
                    {
                        if ( rng.WithPercentageChance( 50 ) )
                            result = data.Ages.PopEnd();
                        else
                            result = data.Locales.PopEnd();

                        if ( !result.EndsWith( "-" ) )
                            result += " ";

                        resultCx++;
                    }
                    return new GenResult( result, resultCx );
                }

                // 
                var spMaxNumberOfComponents = 2;
                var spMoreChaotic = false;
                switch ( spirit )
                {
                    case GenerationSpirit.Untethered:
                        spMaxNumberOfComponents = 3;
                        spMoreChaotic = true;
                        break;
                }

                if ( rng.WithPercentageChance( (spMoreChaotic ? 40 : 10) ) )
                {
                    result += data.Ages.PopEnd();
                    if ( !result.EndsWith( "-" ) )
                        result += " ";

                    resultCx++;

                    spMaxNumberOfComponents--;
                    if ( spMaxNumberOfComponents <= 0 || rng.WithPercentageChance( 60 ) )
                        return new GenResult( result, resultCx );
                }
                if ( rng.WithPercentageChance( (spMoreChaotic ? 90 : 60) ) )
                {
                    if ( rng.WithPercentageChance( (spMoreChaotic ? 70 : 25) ) )
                    {
                        result += rng.RandomItemFrom( ProceduralSourceData._PreSubStyle );
                        if ( !result.EndsWith( "-" ) )
                            result += " ";

                        resultCx++;
                    }

                    result += data.SubStyle.PopEnd();
                    if ( !result.EndsWith( "-" ) )
                        result += " ";

                    resultCx++;

                    spMaxNumberOfComponents--;
                    if ( spMaxNumberOfComponents <= 0 || rng.WithPercentageChance( 60 ) )
                        return new GenResult( result, resultCx );
                }

                if ( rng.WithPercentageChance( (spMoreChaotic ? 75 : 30) ) )
                {
                    result += data.Instruments.PopEnd();
                    result += " ";

                    resultCx++;
                }
                else if ( rng.WithPercentageChance( (spMoreChaotic ? 75 : 30) ) )
                {
                    result += data.Locales.PopEnd();
                    if ( !result.EndsWith("-") )
                        result += " ";

                    resultCx++;
                }

                return new GenResult( result, resultCx );
            }
        }

        static readonly NormalStyle primaryStyle    = new NormalStyle();
        static readonly FusedStyle fusedStyle       = new FusedStyle();
        static readonly Complicator complicator     = new Complicator();

        public static List<string> Generate( ProceduralSourceData sourceData, RNG32 rng, List<GenerationSpirit> generations )
        {
            List<string> results = new List<string>(generations.Count);
            foreach ( var gs in generations )
            {
                GenResult style;

                if ( rng.WithPercentageChance( 50 ) )
                    style = primaryStyle.Generate( sourceData, rng, gs );
                else
                    style = fusedStyle.Generate( sourceData, rng, gs );

                GenResult complex = complicator.Generate( sourceData, rng, gs );

                string generated = complex.Words + style.Words;

                if ( ( style.Complexity + complex.Complexity ) == 1 )
                {
                    if ( rng.WithPercentageChance( 25 ) )
                        generated = sourceData.Locales.PopEnd() + " " + generated;
                    else
                        generated = sourceData.Instruments.PopEnd() + " " + generated;
                }

                results.Add( generated );
            }

            return results;
        }
    }
}
