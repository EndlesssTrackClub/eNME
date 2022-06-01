using System;

namespace eNME.Procgen
{
    // portions from Austin Appleby's MurmurHash ( public domain )
    // Google's CityHash ( MIT https://github.com/google/cityhash/blob/master/COPYING )

    public static class Hash
    {
        // barrel rotate right 32b
        public static UInt32 RotateR32( UInt32 value, Int32 shift )
        {
            if ( shift == 0 )
                return value;

            return ( value >> shift ) | ( value << ( 32 - shift ) );
        }

        // .. and left
        public static UInt32 RotateL32( UInt32 value, Int32 shift )
        {
            return ( value << shift ) | ( value >> ( 32 - shift ) );
        }

        // barrel rotate right 64b
        public static UInt64 RotateR64( UInt64 value64, Int32 shift )
        {
            if ( shift == 0 )
                return value64;

            return ( value64 >> shift ) | ( value64 << ( 64 - shift ) );
        }

        public static UInt64 RotateL64( UInt64 value64, Int32 shift )
        {
            if ( shift == 0 )
                return value64;

            return ( value64 << shift ) | ( value64 >> ( 64 - shift ) );
        }


        // U32 -> hashed U32
        public static UInt32 MixU32( UInt32 value, UInt32 key = 0x85ebca6b )
        {
            value  = ( value ^ 61 ) ^ ( value >> 16 );
            value += ( value << 3 );
            value ^= ( value >> 4 );
            value *= key;
            value ^= ( value >> 15 );
            return value;
        }

        // U64 -> hashed U64
        public static UInt64 MixU64( UInt64 value )
        {
            value ^= value >> 33;
            value *= 0x64dd81482cbd31d7UL;
            value ^= value >> 33;
            value *= 0xe36aa5c613612997UL;
            value ^= value >> 33;

            return value;
        }

        // U64 -> hashed U32
        public static UInt32 Reduce64To32( UInt64 value, UInt32 key = 0x1b873593 )
        {
            UInt32 hash = key, tmp;

            hash += (UInt32) ( value & 0xFFFF );
            tmp   = ( (UInt32) ( value & 0xFFFF0000 ) >> 5 ) ^ hash;
            hash  = ( hash << 16 ) ^ tmp;
            hash += hash >> 11;

            hash += (UInt32) ( ( value >> 32 ) & 0xFFFF );
            tmp   = ( ( (UInt32) ( value >> 32 ) & 0xFFFF0000 ) >> 5 ) ^ hash;
            hash  = ( hash << 16 ) ^ tmp;
            hash += hash >> 11;

            hash ^= hash << 3;
            hash += hash >> 5;
            hash ^= hash << 4;
            hash += hash >> 17;
            hash ^= hash << 25;
            hash += hash >> 6;

            return hash;
        }

        // U32 -> hashed U64
        public static UInt64 Expand32To64( UInt32 value, UInt32 key = 0xcc9e2d51 )
        {
            UInt32 lower32 = MixU32( value, key );
            UInt32 upper32 = MixU32( value, MixU32( key ) );

            return ( (UInt64) lower32 ) | ( (UInt64) upper32 << 32 );
        }

        // U32, U32 -> hashed U32
        public static UInt32 Crush32( UInt32 value1, UInt32 value2, UInt32 key = 0xe6546b64 )
        {
            UInt32 v1 = value1 * 0xcc9e2d51;
            v1 = Hash.RotateR32( v1, 17 );
            v1 *= 0x1b873593;

            UInt32 v2 = value2 ^ v1;
            v2 = Hash.RotateR32( v2, 19 );
            return v2 * 5 + key;
        }

        // U64, U64 -> hashed U64
        public static UInt64 Crush64( UInt64 value1, UInt64 value2, UInt64 key = 0x9ddfea08eb382d69UL )
        {
            // Murmur-inspired hashing
            UInt64 a = ( value2 ^ value1 ) * key;
            a ^= ( a >> 47 );
            UInt64 b = ( value1 ^ a ) * key;
            b ^= ( b >> 47 );
            b *= key;
            return b;
        }

        // originally from Pixar MJS
        // bit-scrambles and produces a result under [limit]
        public static UInt32 PermuteToLimit( UInt32 value, in UInt32 limit, in UInt32 key )
        {
            UInt32 w = limit - 1;
            w |= w >> 1;
            w |= w >> 2;
            w |= w >> 4;
            w |= w >> 8;
            w |= w >> 16;

            do
            {
                value ^= key;
                value *= 0xe170893d;
                value ^= key >> 16;
                value ^= ( value & w ) >> 4;
                value ^= key >> 8;
                value *= 0x0929eb3f;
                value ^= key >> 23;
                value ^= ( value & w ) >> 1;
                value *= 1 | key >> 27;
                value *= 0x6935fa69;
                value ^= ( value & w ) >> 11;
                value *= 0x74dcb303;
                value ^= ( value & w ) >> 2;
                value *= 0x9e501cc3;
                value ^= ( value & w ) >> 2;
                value *= 0xc860a3df;
                value &= w;
                value ^= value >> 5;
            } while ( value >= limit );

            return ( value + key ) % limit;
        }

    } // class Hash

} // namespace eNME