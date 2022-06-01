using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace eNME.Procgen
{
    public static class Extensions
    {
        public static IList<T> Swap<T>( this IList<T> list, Int32 indexA, Int32 indexB )
        {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
            return list;
        }

        public static T PopAt<T>( this List<T> list, int index )
        {
            T r = list[index];
            list.RemoveAt( index );
            return r;
        }

        public static T PopEnd<T>( this List<T> list )
        {
            return PopAt( list, list.Count - 1 );
        }

        public static void Iterate<T>( this IEnumerable<T> self, Action<T> action )
        {
            if ( self != null )
            {
                foreach ( var element in self )
                {
                    action( element );
                }
            }
        }

        public static double Clamp( this double v, double min, double max )
        {
            return Math.Min( Math.Max( (v), min ), max );
        }
    }
}
