using System;
using System.Collections.Generic;
using System.Linq;

namespace eNME.Procgen
{
    // Marsaglia RNG
    public sealed class RNG32 : IEquatable<RNG32>
    {
        private UInt32 seed0;
        private UInt32 seed1;

        public RNG32( UInt32 seed )
        {
            Reseed( seed );
        }

        public void Reseed( UInt32 seed )
        {
            seed0 = ( seed == 0 ) ? 1 : seed;
            seed1 = Hash.MixU32( seed0 );
        }

        public void ScrambleSeed()
        {
            Reseed( Hash.MixU32( GenUInt32() ) );
        }


        // random float in range [0..1]
        public float GenFloat()
        {
            // 1.0 / (2^32-1)
            const double randomOneOverRandMax = ( 1.0 / 4294967295.0 );

            double randomValue = (double) GenUInt32();
            return (float) ( randomValue * randomOneOverRandMax );
        }

        // random float in given range (inclusive)
        public float GenFloat( float min, float max )
        {
            if ( min == max )
                return min;

            if ( max < min )
                throw new ArgumentException( $"invalid random range {min} - {max}" );

            return ( GenFloat() * ( max - min ) + min );
        }

        // random i32 in given range (inclusive)
        public Int32 GenInt32( Int32 min, Int32 max )
        {
            if ( min == max )
                return min;

            if ( max < min )
                throw new ArgumentException( $"invalid random range {min} - {max}" );

            UInt64 uRange = (UInt64) ( max - min + 1 );
            UInt64 uRand = (UInt64) GenUInt32();

            uRand = ( uRand * uRange ) >> 32;
            Int32 result = min + (Int32) uRand;

            return result;
        }

        // random unsigned integer
        // http://cliodhna.cop.uop.edu/~hetrick/na_faq.html
        public UInt32 GenUInt32()
        {
            UInt64 temp = (UInt64) 1893513180 * (UInt64) seed0 + (UInt64) seed1;
            seed0 = (UInt32) ( temp & ~0u );
            seed1 = (UInt32) ( ( temp >> 32 ) & ~0u );

            return seed0;
        }

        public bool GenBool()
        {
            return (GenUInt32() & 1) == 1;
        }

        public bool WithPercentageChance( Int32 pctToPass )
        {
            if ( pctToPass <= 0 )
                return false;
            if ( pctToPass >= 100 )
                return true;

            return GenInt32(0, 100) <= pctToPass;
        }

        public bool Equals( RNG32 other )
        {
            if ( other == null )
                return false;
            if ( seed0 != other.seed0 )
                return false;
            if ( seed1 != other.seed1 )
                return false;

            return true;
        }

        public T RandomItemFrom<T>( IEnumerable<T> container )
        {
            var enumerableCount = container.Count();
            switch ( enumerableCount )
            {
                case 0:
                    throw new ArgumentOutOfRangeException( "cannot select random element of empty storage input" );
                case 1:
                    return container.First();

                default:
                {
                    var randomSelection = GenInt32( 0, enumerableCount - 1 );    // upper range is inclusive, so -1
                    return container.ElementAt( randomSelection );
                }
            }
        }

        public void ShuffleList<T>( IList<T> container )
        {
            var enumerableCount = container.Count();
            for ( int i = 0; i < enumerableCount; i++ )
            {
                var randomSelection = GenInt32( 0, enumerableCount - 1 ); // upper range is inclusive, so -1

                // swap with a random element
                container.Swap( i, randomSelection );
            }
        }

    } // class RNG32

    // -----------------------------------------------------------------------------------------------

    // xoroshiro128+ RNG
    // public domain by David Blackman and Sebastiano Vigna 
    public sealed class RNG64 : IEquatable<RNG64>
    {
        private UInt64 seed0;
        private UInt64 seed1;

        public RNG64( UInt64 Seed )
        {
            Reseed( Seed );
        }

        public void Reseed( UInt64 seed )
        {
            seed0 = SplitMix( seed );
            seed1 = SplitMix( seed0 );
        }

        public UInt64 U64
        {
            get {
                UInt64 s0 = seed0;
                UInt64 s1 = seed1;
                UInt64 result = s0 + s1;

                s1 ^= s0;
                seed0 = Hash.RotateL64( s0, 24 ) ^ s1 ^ ( s1 << 16 ); // a, b
                seed1 = Hash.RotateL64( s1, 37 );                     // c

                return result;
            }
        }

        public UInt32 U32
        {
            get => Hash.Reduce64To32( U64 );
        }

        private const UInt64 DOUBLE_MASK = ( 1L << 53 ) - 1;
        private const double NORM_53 = 1.0 / ( 1L << 53 );

        // random double in range [0..1]
        public double UnitDouble
        {
            get => ( U64 & DOUBLE_MASK ) * NORM_53;
        }


        // splitmix64
        private static UInt64 SplitMix( in UInt64 value )
        {
            UInt64 z = ( value + 0x9E3779B97F4A7C15UL );
            z = ( z ^ ( z >> 30 ) ) * 0xBF58476D1CE4E5B9UL;
            z = ( z ^ ( z >> 27 ) ) * 0x94D049BB133111EBUL;
            return z ^ ( z >> 31 );
        }

        public bool Equals( RNG64 other )
        {
            if ( other == null )
                return false;
            if ( seed0 != other.seed0 )
                return false;
            if ( seed1 != other.seed1 )
                return false;

            return true;
        }
    } // class RNG64
} // namespace PCGK