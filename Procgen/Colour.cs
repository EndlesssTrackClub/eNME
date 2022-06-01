using System;
using System.ComponentModel;
using System.Drawing;

// this file is taken from my Asphalt control library, MIT licence

namespace eNME.Procgen
{
    // https://www.materialui.co/flatuicolors
    public static class ConstantsFlatUI
    {
        public static readonly Color Turqoise           = Color.FromArgb( 26,188,156 );
        public static readonly Color Emerland           = Color.FromArgb( 46,204,113 );
        public static readonly Color PeterRiver         = Color.FromArgb( 52,152,219 );
        public static readonly Color Amethyst           = Color.FromArgb( 155,89,182 );
        public static readonly Color WetAsphalt         = Color.FromArgb( 52,73,94 );
                                                                          
        public static readonly Color GreenSea           = Color.FromArgb( 22,160,133 );
        public static readonly Color Nephritis          = Color.FromArgb( 39,174,96 );
        public static readonly Color BelizeHole         = Color.FromArgb( 41,128,185 );
        public static readonly Color Wisteria           = Color.FromArgb( 142,68,173 );
        public static readonly Color MidnightBlue       = Color.FromArgb( 44,62,80 );
                                                                          
        public static readonly Color Sunflower          = Color.FromArgb( 241,196,15 );
        public static readonly Color Carrot             = Color.FromArgb( 230,126,34 );
        public static readonly Color Alizarin           = Color.FromArgb( 231,76,60 );
        public static readonly Color Clouds             = Color.FromArgb( 236,240,241 );
        public static readonly Color Concrete           = Color.FromArgb( 149,165,166 );
                                                                          
        public static readonly Color Orange             = Color.FromArgb( 243,156,18 );
        public static readonly Color Pumpkin            = Color.FromArgb( 211,84,0 );
        public static readonly Color Pomegranate        = Color.FromArgb( 192,57,43 );
        public static readonly Color Silver             = Color.FromArgb( 189,195,199 );
        public static readonly Color Asbestos           = Color.FromArgb( 127,140,141 );
    }

    // https://www.materialui.co/metrocolors
    public static class ConstantsMetro
    {
        public static readonly Color Lime               = Color.FromArgb( 164,196,0 );
        public static readonly Color Green              = Color.FromArgb( 96,169,23 );
        public static readonly Color Emerald            = Color.FromArgb( 0,138,0 );
        public static readonly Color Teal               = Color.FromArgb( 0,171,169 );
        public static readonly Color Cyan               = Color.FromArgb( 27,161,226 );
        public static readonly Color Cobalt             = Color.FromArgb( 0,80,239 );
        public static readonly Color Indigo             = Color.FromArgb( 106,0,255 );
                                              
        public static readonly Color Violet             = Color.FromArgb( 170,0,255 );
        public static readonly Color Pink               = Color.FromArgb( 244,114,208 );
        public static readonly Color Magenta            = Color.FromArgb( 216,0,115 );
        public static readonly Color Crimson            = Color.FromArgb( 162,0,37 );
        public static readonly Color Red                = Color.FromArgb( 229,20,0 );
        public static readonly Color Orange             = Color.FromArgb( 250,104,0 );
        public static readonly Color Amber              = Color.FromArgb( 240,163,10 );
    }

    // https://codepen.io/devi8/pen/lvIeh ( by Chris Cifonie )
    public static class ConstantsErigon
    {
        public static readonly Color Grapefruit_L       = Color.FromArgb( 238, 88, 100 );
        public static readonly Color Grapefruit_D       = Color.FromArgb( 219, 71, 82 );
        public static readonly Color Bittersweet_L      = Color.FromArgb( 253, 113, 80 );
        public static readonly Color Bittersweet_D      = Color.FromArgb( 234, 90, 62 );
        public static readonly Color Sunflower_L        = Color.FromArgb( 253, 209, 84 );
        public static readonly Color Sunflower_D        = Color.FromArgb( 244, 190, 66 );
        public static readonly Color Grass_L            = Color.FromArgb( 155, 214, 104 );
        public static readonly Color Grass_D            = Color.FromArgb( 135, 195, 83 );
                                              
        public static readonly Color Mint_L             = Color.FromArgb( 64, 207, 173 );
        public static readonly Color Mint_D             = Color.FromArgb( 46, 188, 155 );
        public static readonly Color Aqua_L             = Color.FromArgb( 82, 191, 233 );
        public static readonly Color Aqua_D             = Color.FromArgb( 64, 173, 218 );
        public static readonly Color BlueJeans_L        = Color.FromArgb( 100, 152, 236 );
        public static readonly Color BlueJeans_D        = Color.FromArgb( 82, 133, 220 );
        public static readonly Color Lavender_L         = Color.FromArgb( 176, 143, 236 );
        public static readonly Color Lavender_D         = Color.FromArgb( 155, 118, 220 );

        public static readonly Color PinkRose_L         = Color.FromArgb( 238, 134, 192 );
        public static readonly Color PinkRose_D         = Color.FromArgb( 217, 111, 173 );
        public static readonly Color LightGray_L        = Color.FromArgb( 245, 247, 250 );
        public static readonly Color LightGray_D        = Color.FromArgb( 230, 233, 237 );
        public static readonly Color MediumGray_L       = Color.FromArgb( 204, 209, 217 );
        public static readonly Color MediumGray_D       = Color.FromArgb( 170, 178, 189 );
        public static readonly Color DarkGray_L         = Color.FromArgb( 101, 108, 120 );
        public static readonly Color DarkGray_D         = Color.FromArgb( 67, 73, 84 );

        public static readonly Color Teal_L             = Color.FromArgb( 160, 206, 203 );
        public static readonly Color Teal_D             = Color.FromArgb( 125, 177, 177 );
        public static readonly Color Straw_L            = Color.FromArgb( 232, 206, 77 );
        public static readonly Color Straw_D            = Color.FromArgb( 224, 195, 65 );
        public static readonly Color Plum_L             = Color.FromArgb( 128, 103, 183 );
        public static readonly Color Plum_D             = Color.FromArgb( 106, 80, 167 );
        public static readonly Color Ruby_L             = Color.FromArgb( 216, 51, 74 );
        public static readonly Color Ruby_D             = Color.FromArgb( 191, 38, 60 );
        public static readonly Color Charcoal_L         = Color.FromArgb( 60, 59, 61 );
        public static readonly Color Charcoal_D         = Color.FromArgb( 50, 49, 51 );

        public static readonly Color Void_L             = Color.FromArgb( 27, 27, 28 );
        public static readonly Color Void_D             = Color.FromArgb( 15, 14, 15 );
    }


    public abstract class ColourBase
    {
        #region Events

        protected void ProcessSetProperty( ref double storage, double v, string pname )
        {
            if ( v < 0.0 || v > 1.0 )
                throw new ArgumentOutOfRangeException( pname, "must be between 0.0 and 1.0" );

            storage = v;

            PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( pname ) );
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }

    static class Utils
    {
        public static void Swap<T>(ref T lhs, ref T rhs)
        {
            T temp = lhs;
            lhs = rhs;
            rhs = temp;
        }

        public static double SmoothStep(double a, double b, double x)
        {
            if (x < a) return 0.0;
            else if (x >= b) return 1.0;

            x = (x - a) / (b - a);
            return (x * x * (3.0 - 2.0 * x));
        }
    }

    public class RGB : ColourBase
    {
        #region Properties 

        private double   Red        = 0;
        private double   Green      = 0;
        private double   Blue       = 0;

        public double R  { get { return Red; }          set { ProcessSetProperty( ref Red, value, "Red" );                  } }
        public double G  { get { return Green; }        set { ProcessSetProperty( ref Green, value, "Green" );              } }
        public double B  { get { return Blue; }         set { ProcessSetProperty( ref Blue, value, "Blue" );                } }

        #endregion


        public override string ToString()
        {
            return "RGB";
        }

        public RGB()
        {
        }

        public RGB( double inR, double inG, double inB )
        {
            R = inR;
            G = inG;
            B = inB;
        }

        public RGB( Color from )
        {
            R = (double)from.R / 255.0;
            G = (double)from.G / 255.0;
            B = (double)from.B / 255.0;
        }

        static public RGB Lerp( RGB lhs, RGB rhs, double t )
        {
            double oneOverT = 1.0 - t;
            return new RGB(
                (lhs.R * oneOverT) + (rhs.R * t),
                (lhs.G * oneOverT) + (rhs.G * t),
                (lhs.B * oneOverT) + (rhs.B * t)
                );
        }

        #region Conversion

        public Color ToColor()
        {
            return Color.FromArgb(
                (int)( R * 255.0 ),
                (int)( G * 255.0 ),
                (int)( B * 255.0 ) );
        }

        public Color ToColor( double alpha )
        {
            return Color.FromArgb(
                (int)( alpha.Clamp(0.0, 1.0) * 255.0 ),
                (int)( R * 255.0 ),
                (int)( G * 255.0 ),
                (int)( B * 255.0 ) );
        }

        // conversion from HSV to RGB
        public static RGB fromHSV(HSV input)
        {
            RGB result = new RGB();

            double i = Math.Floor( input.H * 6.0 );

            double f = input.H * 6.0 - i;
            double p = input.V * ( 1.0 - input.S );
            double q = input.V * ( 1.0 - f * input.S );
            double t = input.V * ( 1.0 - ( 1.0 - f ) * input.S );

            switch ( (int)i % 6 )
            {
                case 0: result.R = input.V; result.G = t;          result.B = p;        break;
                case 1: result.R = q;       result.G = input.V;    result.B = p;        break;
                case 2: result.R = p;       result.G = input.V;    result.B = t;        break;
                case 3: result.R = p;       result.G = q;          result.B = input.V;  break;
                case 4: result.R = t;       result.G = p;          result.B = input.V;  break;
                case 5: result.R = input.V; result.G = p;          result.B = q;        break;
            }
            
            return result;
        }

        #endregion
    }

    public class HSV : ColourBase
    {
        #region Properties 

        private double   Hue        = 0;
        private double   Saturation = 0;
        private double   Value      = 0;

        public double H  { get { return Hue; }          set { ProcessSetProperty( ref Hue, value, "Hue" );                  } }
        public double S  { get { return Saturation; }   set { ProcessSetProperty( ref Saturation, value, "Saturation" );    } }
        public double V  { get { return Value; }        set { ProcessSetProperty( ref Value, value, "Value" );              } }

        #endregion

 
        public override string ToString()
        {
            return "HSV";
        }

        public HSV()
        {
        }

        public HSV( double inH, double inS, double inV )
        {
            H = inH;
            S = inS;
            V = inV;
        }

        public HSV( Color from )
        {
            HSV converted = fromRGB( new RGB(from) );
            H = converted.H;
            S = converted.S;
            V = converted.V;
        }

        #region Adjustments

        public void ScaleH( double factor )
        {
            H = ( Hue * factor ).Clamp( 0.0, 1.0 );
        }

        public void ScaleS( double factor )
        {
            S = ( Saturation * factor ).Clamp( 0.0, 1.0 );
        }

        public void ScaleV( double factor )
        {
            V = ( Value * factor ).Clamp( 0.0, 1.0 );
        }

        public void OffsetH( double offset )
        {
            H = ( Hue + offset ).Clamp( 0.0, 1.0 );
        }

        public void OffsetS( double offset )
        {
            S = ( Saturation + offset ).Clamp( 0.0, 1.0 );
        }

        public void OffsetV( double offset )
        {
            V = ( Value + offset ).Clamp( 0.0, 1.0 );
        }

        #endregion

        #region Conversion

        // convert to .NET Colour via conversion to RGB first
        public Color ToColor()
        {
            return RGB.fromHSV( this ).ToColor();
        }

        public Color ToColor( double alpha )
        {
            return RGB.fromHSV( this ).ToColor( alpha );
        }

        // conversion from RGB to HSV
        public static HSV fromRGB(RGB input)
        {
            // extract the RGB values as we will modify them inline
            double input_R = input.R;
            double input_G = input.G;
            double input_B = input.B;

            HSV result = new HSV();

            const double avoidDbZ = 1e-20;

            double K = 0;

            if ( input_G < input_B )
            {
                Utils.Swap( ref input_G, ref input_B );
                K = -1.0;
            }

            if ( input_R < input_G )
            {
                Utils.Swap( ref input_R, ref input_G );
                K = -2.0 / 6.0 - K;
            }

            double chroma = input_R - Math.Min( input_G, input_B );

            result.H = Math.Abs( K + ( input_G - input_B ) / ( 6.0 * chroma + avoidDbZ ) );
            result.S = chroma / ( input_R + avoidDbZ );
            result.V = input_R;

            return result;
        }

        #endregion
    }
}
