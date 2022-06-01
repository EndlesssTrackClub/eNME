using System;
using System.Collections.Generic;
using System.Text;

using eNME.Procgen;

namespace eNME
{
    static class ProceduralGenre
    {
        internal class ProceduralSourceData
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
                "Alternative",
                "Ancient",
                "Boomer",
                "Bloated",
                "Bubblegum",
                "Carbon-Neutral",
                "Classic",
                "Contemporary",
                "Cybernetic",
                "Doomer",
                "Emotional",
                "Emotionless",
                "Environmental",
                "Experimental",
                "Expressionist",
                "Kawaii",
                "Kindergarten",
                "Garbage",
                "Hardcore",
                "Industrial",
                "Ironic",
                "Mainstream",
                "Minimalist",
                "Misunderstood",
                "Modern",
                "Nuevo",
                "Orchestral",
                "School",
                "Progressive",
                "Psychedelic",
                "Reflective",
                "Reclusive",
                "Reintegrated",
                "Residential",
                "Romantic",
                "Softcore",
                "Technical",
                "Tiresome",
                "Traditional",
                "Underground",
                "Unrefined",
                "Vintage",
                "Willowy",
                "Zoomer",
            };

            static public readonly List<String> _Locales = new List<string>()
            {
                "British",
                "Russian",
                "German",
                "French",
                "African",
                "Canadian",
                "Texan",
                "Latin",
                "Tropical",
                "Regional",
                "Northern",
                "Southern",
                "Eastern",
                "J-",
                "K-",
                "G-",
                "Moon",
            };

            static public readonly List<String> _Ages = new List<string>()
            {
                "60s",
                "70s",
                "80s",
                "90s",
                "Forgotten",
                "Next Phase",
                "Pandemic",
                "Near Future",
                "Distant Future",
                "Medieval",
                "Victorian",
                "Dark Age",
                "Bronze Age",
                "Timeless",
                "Jurassic",
            };

            static public readonly List<String> _Prefix = new List<string>()
            {
                "Dark",
                "Electro",
                "Hard",
                "Synth",
                "Euro",
                "Drum",
                "Tech",
                "Brit",
                "Adult",
                "Doom",
                "Death",
                "Nerd",
                "Hyper",
                "Ultra",
                "Cool",
                "Smooth",
                "Grind",
                "Math",
                "LoFi",
                "Crust",
                "Steam",
                "Sweet",
                "Sour",
                "Vague",
                "Think",
                "Real",
                "Quiet",
                "Chill",
                "Otter",
                "Big",
                "Small",
                "Trad",
                "Limp",
                "Fuzz",
                "Sleek",
                "Wave",
                "Core",
                "Jingle",
                "Wonky",
                "Step",
                "Club",
                "Love",
                "Glam",
                "Laser",
                "Future",
                "Past",
                "Attack",
            };

            static public readonly List<String> _Suffix = new List<string>()
            {
                "wave",
                "core",
                "step",
                "dub",
                "club",
                "noise",
                "pulse",
                "feed",
                "feel",
                "grind",
                "vibe",
                "mode",
                "pop",
                "slam",
                "sweep",
                "wash",
                "beam"
            };

            static public readonly List<String> _CommonStyle = new List<string>()
            {
                "Ballad",
                "Blues",
                "Breaks",
                "Country",
                "Crunk",
                "Dance",
                "Drum and Bass",
                "Drone",
                "Dub",
                "Funk",
                "Folk",
                "Gospel",
                "Hip-Hop",
                "House",
                "Indie",
                "Jazz",
                "Metal",
                "Pop",
                "Bop",
                "Punk",
                "Rap",
                "Rock",
                "Salsa",
                "Silence",
                "Ska",
                "Soul",
                "Swing",
                "Techno",
                "Trance",
            };

            // each instance of the data contains a shuffled copy of the lists ready for use
            public ProceduralSourceData( RNG32 rng )
            {
                SubStyle        = new List<string>( _SubStyle );
                Locales         = new List<string>( _Locales );
                Ages            = new List<string>( _Ages );
                Prefix          = new List<string>( _Prefix );
                Suffix          = new List<string>( _Suffix );
                CommonStyle     = new List<string>( _CommonStyle );

                rng.ShuffleList( SubStyle );    SubStyle.Reverse();     rng.ShuffleList( SubStyle );
                rng.ShuffleList( Locales );     Locales.Reverse();      rng.ShuffleList( Locales );
                rng.ShuffleList( Ages );        Ages.Reverse();         rng.ShuffleList( Ages );
                rng.ShuffleList( Prefix );      Prefix.Reverse();       rng.ShuffleList( Prefix );
                rng.ShuffleList( Suffix );      Suffix.Reverse();       rng.ShuffleList( Suffix );
                rng.ShuffleList( CommonStyle ); CommonStyle.Reverse();  rng.ShuffleList( CommonStyle );
            }

            public readonly List<String> SubStyle;
            public readonly List<String> Locales;
            public readonly List<String> Ages;
            public readonly List<String> Prefix;
            public readonly List<String> Suffix;
            public readonly List<String> CommonStyle;
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


        interface Generator
        {
            public string Generate( ProceduralSourceData data, RNG32 rng, bool ChaosMode );
        }

        internal class PrimaryStyle : Generator
        {
            public string Generate( ProceduralSourceData data, RNG32 rng, bool ChaosMode )
            {
                var baseStyle = data.CommonStyle.PopEnd();

                if ( rng.WithPercentageChance( (ChaosMode ? 80 : 50) ) )
                {
                    if ( rng.WithPercentageChance( 25 ) )
                    {
                        if ( baseStyle.ToLower().Contains( "and" ) )
                        {
                            baseStyle += " and ";
                        }
                        else
                        {
                            baseStyle += rng.RandomItemFrom( Bindings );
                        }
                        baseStyle += data.CommonStyle.PopEnd();
                    }
                    else
                    {
                        baseStyle = data.Prefix.PopEnd() + " " + baseStyle;
                    }
                }

                return baseStyle;
            }
        }

        internal class FusedStyle : Generator
        {
            public string Generate( ProceduralSourceData data, RNG32 rng, bool ChaosMode )
            {
                string result = rng.WithPercentageChance( (ChaosMode ? 70 : 25 ) ) ? ( data.Prefix.PopEnd() + " " ) : "";

                return result + data.Prefix.PopEnd() + data.Suffix.PopEnd();
            }
        }

        internal class Complicator : Generator
        {
            public string Generate( ProceduralSourceData data, RNG32 rng, bool ChaosMode )
            {
                string result = "";

                if ( rng.WithPercentageChance( (ChaosMode ? 40 : 10) ) )
                {
                    result += data.Ages.PopEnd();
                    if ( !result.EndsWith( "-" ) )
                        result += " ";

                    if ( rng.WithPercentageChance( 60 ) )
                        return result;
                }
                if ( rng.WithPercentageChance( (ChaosMode ? 90 : 60) ) )
                {
                    if ( rng.WithPercentageChance( (ChaosMode ? 70 : 25) ) )
                    {
                        result += rng.RandomItemFrom( ProceduralSourceData._PreSubStyle );
                        if ( !result.EndsWith( "-" ) )
                            result += " ";
                    }

                    result += data.SubStyle.PopEnd();
                    if ( !result.EndsWith( "-" ) )
                        result += " ";

                    if ( rng.WithPercentageChance( 60 ) )
                        return result;
                }
                if ( rng.WithPercentageChance( (ChaosMode ? 60 : 30) ) )
                {
                    result += data.Locales.PopEnd();
                    if ( !result.EndsWith("-") )
                        result += " ";
                }

                return result;
            }
        }

        public static List<string> Generate( RNG32 rng, int num )
        {
            ProceduralSourceData sourceData = new ProceduralSourceData( rng );

            PrimaryStyle primaryStyle = new PrimaryStyle();
            FusedStyle fusedStyle = new FusedStyle();
            Complicator complicator = new Complicator();

            List<string> results = new List<string>(num);
            for ( int i =0; i<num; i++ )
            {
                string style = "";

                if ( rng.WithPercentageChance( 65 ) ) 
                    style = primaryStyle.Generate( sourceData, rng, rng.GenBool() );
                else
                    style = fusedStyle.Generate( sourceData, rng, rng.GenBool() );

                var complex = complicator.Generate( sourceData, rng, rng.GenBool() );

                results.Add( complex + style );
            }

            return results;
        }
    }
}
