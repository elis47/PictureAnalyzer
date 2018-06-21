using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using static EmguCV.ColorSpaces;

namespace EmguCV
{
    public class ImageProcessing
    {
        public List<CIELab> cielabIndices = new List<CIELab>(54);

        public ImageProcessing(string filename)
        {
            PopulateCielabIndices();
            

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


            // Build Bitmap and extract rgb/hsb/cielab values

            Bitmap bmap = new Bitmap(filename);
            Dictionary<string, int> colorDictionary = new Dictionary<string, int>();

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

                    var labResult = RGBtoLab(red, green, blue);

                    var L = labResult.L;
                    var a = labResult.A;
                    var b = labResult.B;

                    var min = 1000.0;
                    var index = 0;
                    foreach(var cielabIndex in cielabIndices)
                    {
                        var min0 = Math.Sqrt(Math.Pow(L - cielabIndex.L, 2) + Math.Pow(a - cielabIndex.A, 2) + Math.Pow(b - cielabIndex.B, 2));
                        if (min0 < min)
                        {
                            min = min0;
                            index = cielabIndices.IndexOf(cielabIndex);
                        }
                    }

                    var color = GetColor(index);
                    if (colorDictionary.ContainsKey(color))
                        colorDictionary[color] += 1;
                    else
                        colorDictionary.Add(color, 1);
                }
            }

            var dict = colorDictionary;
        }

        public string GetColor(int index)
        {
            switch (index)
            {
                case 0:
                    return "VIVID Red";
                case 1:
                    return "VIVID Orange";
                case 2:
                    return "VIVID Yellow";
                case 3:
                    return "VIVID Green";
                case 4:
                    return "VIVID Cyan";
                case 5:
                    return "VIVID Blue";
                case 6:
                    return "VIVID Purple";
                case 7:
                    return "PALE Red";
                case 8:
                    return "PALE Orange";
                case 9:
                    return "PALE Yellow";
                case 10:
                    return "PALE Green";
                case 11:
                    return "PALE Cyan";
                case 12:
                    return "PALE Blue";
                case 13:
                    return "PALE Purple";
                case 14:
                    return "DULL Red";
                case 15:
                    return "DULL Orange";
                case 16:
                    return "DULL Yellow";
                case 17:
                    return "DULL Green";
                case 18:
                    return "DULL Cyan";
                case 19:
                    return "DULL Blue";
                case 20:
                    return "DULL Purple";
                case 21:
                    return "DARK Red";
                case 22:
                    return "DARK Orange";
                case 23:
                    return "DARK Yellow";
                case 24:
                    return "DARK Green";
                case 25:
                    return "DARK Cyan";
                case 26:
                    return "DARK Blue";
                case 27:
                    return "DARK Purple";
                case 28:
                    return "LIGHT GRAYISH Red";
                case 29:
                    return "LIGHT GRAYISH Orange";
                case 30:
                    return "LIGHT GRAYISH Yellow";
                case 31:
                    return "LIGHT GRAYISH Green";
                case 32:
                    return "LIGHT GRAYISH Cyan";
                case 33:
                    return "LIGHT GRAYISH Blue";
                case 34:
                    return "LIGHT GRAYISH Purple";
                case 35:
                    return "GRAYISH Red";
                case 36:
                    return "GRAYISH Orange";
                case 37:
                    return "GRAYISH Yellow";
                case 38:
                    return "GRAYISH Green";
                case 39:
                    return "GRAYISH Cyan";
                case 40:
                    return "GRAYISH Blue";
                case 41:
                    return "GRAYISH Purple";
                case 42:
                    return "DARK GRAYISH Red";
                case 43:
                    return "DARK GRAYISH Orange";
                case 44:
                    return "DARK GRAYISH Yellow";
                case 45:
                    return "DARK GRAYISH Green";
                case 46:
                    return "DARK GRAYISH Cyan";
                case 47:
                    return "DARK GRAYISH Blue";
                case 48:
                    return "DARK GRAYISH Purple";
                case 49:
                    return "BLACK";
                case 50:
                    return "DARK GRAY";
                case 51:
                    return "MID GRAY";
                case 52:
                    return "LIGHT GRAYISH";
                case 53:
                    return "WHITE";
            }
            return "";
        }

        /* Pupulate the list of indices used in the study
         * 1-49 indices :  tones with colors Red, Orange, Yellow, Green, Cyan, Blue, Purple
         * 50-54 : achromatic tones consisting of black, dark gray, mid gray, light gray, white
         */
        public void PopulateCielabIndices()
        {
            #region VIVID 
            cielabIndices.Add(new CIELab(35.0, 46.4, 23.2));  // Red
            cielabIndices.Add(new CIELab(56.7, 30.6, 52.1));  // Orange
            cielabIndices.Add(new CIELab(79.2, 2.6, 61.3));   // Yellow
            cielabIndices.Add(new CIELab(34.1, -25.8, 17.0)); // Green
            cielabIndices.Add(new CIELab(34.6, -17.8, -8.9)); // Cyan
            cielabIndices.Add(new CIELab(35.8, -2.4, -33.4)); //Blue
            cielabIndices.Add(new CIELab(35.7, 36.9, -23.6)); // Purple
            #endregion VIVID

            #region PALE 
            cielabIndices.Add(new CIELab(79.8, 21.9, 10.9));  // Red
            cielabIndices.Add(new CIELab(79.6, 13.1, 22.2));  // Orange
            cielabIndices.Add(new CIELab(79.4, 1.4, 25.2));   // Yellow
            cielabIndices.Add(new CIELab(79.5, -19.4, 12.6)); // Green
            cielabIndices.Add(new CIELab(80.8, -13.3, -5.9)); // Cyan
            cielabIndices.Add(new CIELab(80.6, -1.9, -24.3)); //Blue
            cielabIndices.Add(new CIELab(80.3, 19.5, -12.1)); // Purple
            #endregion PALE

            #region DULL 
            cielabIndices.Add(new CIELab(56.9, 21.8, 10.6));  // Red
            cielabIndices.Add(new CIELab(56.5, 12.8, 21.9));  // Orange
            cielabIndices.Add(new CIELab(56.4, 1.0, 25.1));   // Yellow
            cielabIndices.Add(new CIELab(56.6, -19.5, 12.6)); // Green
            cielabIndices.Add(new CIELab(57.0, -12.9, -6.3)); // Cyan
            cielabIndices.Add(new CIELab(57.6, -2.0, -24.4)); //Blue
            cielabIndices.Add(new CIELab(57.3, 19.3, -12.3)); // Purple
            #endregion DULL

            #region DARK 
            cielabIndices.Add(new CIELab(34.4, 21.0, 10.4));  // Red
            cielabIndices.Add(new CIELab(34.0, 12.1, 21.3));  // Orange
            cielabIndices.Add(new CIELab(34.0, 1.1, 24.3));   // Yellow
            cielabIndices.Add(new CIELab(34.0, -18.6, 12.3)); // Green
            cielabIndices.Add(new CIELab(34.4, -12.6, -5.8)); // Cyan
            cielabIndices.Add(new CIELab(35.0, -1.4, -24.1)); //Blue
            cielabIndices.Add(new CIELab(34.8, 18.4, -11.8)); // Purple
            #endregion DARK

            #region LIGHT GRAYISH 
            cielabIndices.Add(new CIELab(79.8, 11.1, 5.8));  // Red
            cielabIndices.Add(new CIELab(79.7, 6.7, 11.3));  // Orange
            cielabIndices.Add(new CIELab(79.6, 0.9, 12.9));   // Yellow
            cielabIndices.Add(new CIELab(79.6, -9.7, 6.6)); // Green
            cielabIndices.Add(new CIELab(79.9, -6.8, -2.8)); // Cyan
            cielabIndices.Add(new CIELab(80.8, -0.9, -12.0)); //Blue
            cielabIndices.Add(new CIELab(79.8, 9.8, -6.1)); // Purple
            #endregion LIGHT GRAYISH

            #region GRAYISH 
            cielabIndices.Add(new CIELab(56.9, 11.0, 5.3));  // Red
            cielabIndices.Add(new CIELab(56.6, 6.2, 11.1));  // Orange
            cielabIndices.Add(new CIELab(56.6, 0.8, 12.4));   // Yellow
            cielabIndices.Add(new CIELab(56.6, -10.0, 6.5)); // Green
            cielabIndices.Add(new CIELab(56.8, -6.6, -3.3)); // Cyan
            cielabIndices.Add(new CIELab(57.1, -0.9, -12.3)); //Blue
            cielabIndices.Add(new CIELab(57.1, 9.4, -5.9)); // Purple
            #endregion GRAYISH

            #region DARK GRAYISH 
            cielabIndices.Add(new CIELab(34.1, 10.5, 5.3));  // Red
            cielabIndices.Add(new CIELab(34.1, 6.1, 10.7));  // Orange
            cielabIndices.Add(new CIELab(34.0, 0.6, 12.3));   // Yellow
            cielabIndices.Add(new CIELab(34.0, -9.6, 6.3)); // Green
            cielabIndices.Add(new CIELab(34.2, -6.3, -2.9)); // Cyan
            cielabIndices.Add(new CIELab(34.6, -0.9, -12.0)); //Blue
            cielabIndices.Add(new CIELab(34.6, 9.2, -5.6)); // Purple
            #endregion DARK GRAYISH

            #region ACHROMATIC 
            cielabIndices.Add(new CIELab(2.3, -0.1, -0.4));  // Black
            cielabIndices.Add(new CIELab(34.2, -0.2, 0.2));  // Dark gray
            cielabIndices.Add(new CIELab(56.9, -0.1, 0.3));   // Mid gray
            cielabIndices.Add(new CIELab(80.0, 3.2, -0.9)); // Light gray
            cielabIndices.Add(new CIELab(100.0, -0.1, 0.3)); // White
            #endregion ACHROMATIC
        }

        public static CIELab RGBtoLab(int red, int green, int blue)
        {
            var result = RGBtoXYZ(red, green, blue);
            return XYZtoLab(result.X, result.Y, result.Z);
        }

        private static double Fxyz(double t)
        {
            return ((t > 0.008856) ? Math.Pow(t, (1.0 / 3.0)) : (7.787 * t + 16.0 / 116.0));
        }

        public static CIELab XYZtoLab(double x, double y, double z)
        {
            CIELab lab = CIELab.Empty;

            lab.L = 116.0 * Fxyz(y / CIEXYZ.D65.Y) - 16;
            lab.A = 500.0 * (Fxyz(x / CIEXYZ.D65.X) - Fxyz(y / CIEXYZ.D65.Y));
            lab.B = 200.0 * (Fxyz(y / CIEXYZ.D65.Y) - Fxyz(z / CIEXYZ.D65.Z));

            return lab;
        }

        public static CIEXYZ RGBtoXYZ(int red, int green, int blue)
        {
            // normalize red, green, blue values
            double rLinear = (double)red / 255.0;
            double gLinear = (double)green / 255.0;
            double bLinear = (double)blue / 255.0;

            // convert to a sRGB form
            double r = (rLinear > 0.04045) ? Math.Pow((rLinear + 0.055) / (
                1 + 0.055), 2.2) : (rLinear / 12.92);
            double g = (gLinear > 0.04045) ? Math.Pow((gLinear + 0.055) / (
                1 + 0.055), 2.2) : (gLinear / 12.92);
            double b = (bLinear > 0.04045) ? Math.Pow((bLinear + 0.055) / (
                1 + 0.055), 2.2) : (bLinear / 12.92);

            // converts
            return new CIEXYZ(
                (r * 0.4124 + g * 0.3576 + b * 0.1805),
                (r * 0.2126 + g * 0.7152 + b * 0.0722),
                (r * 0.0193 + g * 0.1192 + b * 0.9505)
                );
        }
    }
}
