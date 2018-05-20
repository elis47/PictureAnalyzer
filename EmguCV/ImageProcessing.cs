using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace EmguCV
{
    public class ImageProcessing
    {
        Image<Bgr, byte> _bgrImage;
        Image<Lab, byte> _labImage;

        public ImageProcessing(string filename)
        {
            _bgrImage = new Image<Bgr, byte>(filename);
            _labImage = new Image<Lab, byte>(filename);


            Image<Bgr, Byte> image = new Image<Bgr, byte>(filename);

            //Convert to Bitmap
            var imageBitmap = image.ToBitmap();

            //Convert to gray
            var grayImage = image.Convert<Gray, byte>();

            // Build matrix
            Matrix<Byte> matrix = new Matrix<Byte>(image.Rows, image.Cols, image.NumberOfChannels);

            // Build Lab image
            Image<Lab, Byte> labImage = new Image<Lab, Byte>(image.Cols, image.Rows);

            for (int i = 0; i < labImage.Rows; i++)
                for (int j = 0; j < labImage.Cols; j++)
                {
                    var pixel = labImage[i, j];

                    var x = pixel.X;
                    var y = pixel.Y;
                    var z = pixel.Z;

                    var dim = pixel.Dimension;
                }


            // Build Bitmap 
            Bitmap bmap = new Bitmap(filename);

            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    Color c = bmap.GetPixel(i, j);

                    var blue = c.B;
                    var red = c.R;
                    var green = c.G;

                    var saturation = c.GetSaturation();
                    var hue = c.GetHue();
                    var brightness = c.GetBrightness();
                }
            }
        }

        public List<double> ConvertXYZ_To_CIELAB(double x, double y, double z)
        {
            var triple = new List<double> { x, y, z };
            var t0 = 0.008856;
            var a = 7.787;
            var b = 16 / 116;

            var refNumbers = new List<double> { 95.047, 100.0, 108.883 };
            foreach (var i in triple)
            {

            }

            for (int i = 0; i < 3; i++)
            {
                triple[i] /= refNumbers[i];
                var c = triple[i];

                if (c > t0)
                    triple[i] = Math.Pow(c, 1 / 3);
                else
                    triple[i] = a * c + b;

            }

            var l = 116 * triple[0] - 16;
            a = 500 * (triple[0] - triple[1]);
            b = (int)(200 * (triple[1] - triple[2]));
            return new List<double> { l, a, b };
        }
    }
}
