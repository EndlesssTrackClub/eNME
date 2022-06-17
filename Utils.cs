using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace eNME
{
    public static class Utils
    {
        public static void CompressBitmapToStream( Stream stream, Bitmap image, long quality )
        {
            using ( EncoderParameters encoderParameters = new EncoderParameters( 1 ) )
            using ( EncoderParameter encoderParameter = new EncoderParameter( System.Drawing.Imaging.Encoder.Quality, quality ) )
            {
                ImageCodecInfo codecInfo = ImageCodecInfo.GetImageDecoders().First(codec => codec.FormatID == System.Drawing.Imaging.ImageFormat.Jpeg.Guid);
                encoderParameters.Param[0] = encoderParameter;
                image.Save( stream, codecInfo, encoderParameters );
                stream.Seek( 0, SeekOrigin.Begin );
            }
        }
    }
}
