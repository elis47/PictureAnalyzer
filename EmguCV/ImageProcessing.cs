using System;
using System.Collections.Generic;
using System.Drawing;
using static EmguCV.ColorSpaces;
using System.Linq;

namespace EmguCV
{
    public class ImageProcessing
    {
        public List<Pixel> cielabIndices = new List<Pixel>();
        public Dictionary<Pixel, double> hueDictionary = new Dictionary<Pixel, double>();
        public int numberOfColors = 5;

        public ImageProcessing(string filename)
        {
            PopulateCielabIndices();

            var colorDistribution = GetColorDistribution(filename);
            var distinctPixels = GetDistinctPixels(colorDistribution);

            colorDistribution = GetSortedDictionary(colorDistribution);
            colorDistribution = GetTopRecords(colorDistribution);

            var currentPixels = GetPixels(colorDistribution, distinctPixels);

            #region Compare 2 by 2 colors and get the harmonic index

            Dictionary<List<Pixel>, double> harmonicDistribution = new Dictionary<List<Pixel>, double>();
            double finalHarmonicMean = 1;

            if (currentPixels.Count > 1)
                finalHarmonicMean = 0;

            for (int i = 0; i < Math.Min(numberOfColors, currentPixels.Count); i++)
            {
                for (int j = i + 1; j < Math.Min(numberOfColors, currentPixels.Count); j++)
                {
                    var deltaHab = currentPixels[i].Hue - currentPixels[j].Hue;
                    var deltaCab = currentPixels[i].Chroma - currentPixels[j].Chroma;
                    var deltaC = Math.Pow((Math.Pow(deltaHab, 2) + Math.Pow(deltaCab / 1.46, 2)), 0.5);

                    var Hc = 0.04 + 0.53 * Math.Tanh(0.8 - 0.045 * deltaC);

                    var Lsum = currentPixels[i].L + currentPixels[j].L;
                    var HLsum = 0.28 + 0.54 * Math.Tanh(-3.88 + 0.029 * Lsum);
                    var deltaL = Math.Abs(currentPixels[i].L - currentPixels[j].L);
                    var HdeltaL = 0.14 + 0.15 * Math.Tanh(-2 + 0.2 * deltaL);

                    var Hl = HLsum + HdeltaL;

                    var delta_hab = currentPixels[i].HueAngle + currentPixels[j].HueAngle;
                    var EC = 0.5 + 0.5 * Math.Tanh(-2 + 0.5 * deltaC);
                    var HS = -0.08 - 0.14 * Math.Sin(delta_hab + 50) - 0.07 * Math.Sin(2 * delta_hab + 90);
                    var EY1 = (0.22 * currentPixels[i].L - 12.8) / 10 * Math.Exp((90 - delta_hab) / 10 - Math.Exp((90 - delta_hab) / 10));
                    var EY2 = (0.22 * currentPixels[j].L - 12.8) / 10 * Math.Exp((90 - delta_hab) / 10 - Math.Exp((90 - delta_hab) / 10));

                    var Hh = EC * (HS + EY1) + EC * (HS + EY2);

                    var harmonyIndex = Hc + Hl + Hc;

                    finalHarmonicMean += harmonyIndex;

                    harmonicDistribution.Add(new List<Pixel>() { currentPixels[i], currentPixels[j] }, harmonyIndex);
                }
            }

            if (harmonicDistribution.Count > 0)
                finalHarmonicMean /= harmonicDistribution.Count;

            #endregion Compare 2 by 2 colors and get the harmonic index

        }

        public Dictionary<Pixel, int> GetColorDistribution(string filename)
        {
            // Build Bitmap and extract rgb/hsb/cielab values

            Bitmap bmap = new Bitmap(filename);
            List<Pixel> distinctPixels = new List<Pixel>();
            Dictionary<Pixel, int> colorDistribution = new Dictionary<Pixel, int>();

            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    Color c = bmap.GetPixel(i, j);

                    var red = c.R;
                    var green = c.G;
                    var blue = c.B;

                    var saturation = c.GetSaturation();
                    var hue = c.GetHue();
                    var brightness = c.GetBrightness();

                    var labResult = RGBtoLab(red, green, blue);

                    var L = labResult.L;
                    var a = labResult.A;
                    var b = labResult.B;

                    var min = 1000.0;
                    var index = 0;

                    foreach (var cielabIndex in cielabIndices)
                    {
                        var min0 = Math.Sqrt(Math.Pow(L - cielabIndex.L, 2) + Math.Pow(a - cielabIndex.a, 2) + Math.Pow(b - cielabIndex.b, 2));
                        if (min0 < min)
                        {
                            min = min0;
                            index = cielabIndices.IndexOf(cielabIndex);
                        }
                    }

                    var cielabColor = cielabIndices[index];
                    var searchedPixel = distinctPixels.Where(p => p.Color == cielabColor.Color && p.Tone == cielabColor.Tone).FirstOrDefault();

                    if (searchedPixel == null)
                    {
                        var pixel = new Pixel();

                        pixel.Color = cielabColor.Color;
                        pixel.Tone = cielabColor.Tone;
                        pixel.L = cielabColor.L;
                        pixel.a = cielabColor.a;
                        pixel.a = cielabColor.b;
                        pixel.Chroma = cielabColor.Chroma;
                        pixel.HueAngle = cielabColor.HueAngle;
                        pixel.Hue = GetHue(pixel.L, pixel.a, pixel.b);

                        distinctPixels.Add(pixel);
                    }

                    searchedPixel = distinctPixels.Where(p => p.Color == cielabColor.Color && p.Tone == cielabColor.Tone).FirstOrDefault();

                    #region Set color distribution

                    if (colorDistribution.ContainsKey(searchedPixel))
                        colorDistribution[searchedPixel] += 1;
                    else
                        colorDistribution.Add(searchedPixel, 1);

                    #endregion Set color distribution
                }
            }

            return colorDistribution;
        }

        public List<Pixel> GetDistinctPixels(Dictionary<Pixel, int> dictionary)
        {
            var distinctPixels = new List<Pixel>();

            foreach (var item in dictionary)
            {
                distinctPixels.Add(item.Key);
            }
            return distinctPixels;
        }

        public Dictionary<Pixel, int> GetSortedDictionary(Dictionary<Pixel, int> dictionary)
        {
            var dictionaryList = dictionary.ToList();
            dictionaryList.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
            return dictionaryList.ToDictionary(x => x.Key, x => x.Value);
        }

        public Dictionary<Pixel, int> GetTopRecords(Dictionary<Pixel, int> dictionary)
        {
            var topRecords = new Dictionary<Pixel, int>();
            var index = 0;
            foreach (KeyValuePair<Pixel, int> pair in dictionary)
            {
                if (index >= dictionary.Count - numberOfColors - 1)
                {
                    topRecords.Add(pair.Key, pair.Value);
                }
                index++;
            }
            return topRecords;
        }

        public List<Pixel> GetPixels(Dictionary<Pixel, int> dictionary, List<Pixel> distinctPixels)
        {
            var pixels = new List<Pixel>();

            foreach (var color in dictionary)
            {
                var foundPixel = distinctPixels.Where(x => x.Tone == color.Key.Tone && x.Color == color.Key.Color).First();
                pixels.Add(foundPixel);
            }
            return pixels;
        }

        public void PopulateCielabIndices()
        {
            /* Pupulate the list of indices used in the study
             * 1-49 indices :  tones of Red, Orange, Yellow, Green, Cyan, Blue, Purple
             * 50-54 : achromatic tones consisting of black, dark gray, mid gray, light gray, white
             */

            #region VIVID 

            cielabIndices.Add(new Pixel("Vivid", "Red", 35.0, 46.4, 23.2, 51.8, 27));
            cielabIndices.Add(new Pixel("Vivid", "Orange", 56.7, 30.6, 52.1, 60.4, 60));
            cielabIndices.Add(new Pixel("Vivid", "Yellow", 79.2, 2.6, 61.3, 61.4, 88));
            cielabIndices.Add(new Pixel("Vivid", "Green", 34.1, -25.8, 17.0, 30.8, 147));
            cielabIndices.Add(new Pixel("Vivid", "Cyan", 34.6, -17.8, -8.9, 19.9, 206));
            cielabIndices.Add(new Pixel("Vivid", "Blue", 35.8, -2.4, -33.4, 33.5, 266));
            cielabIndices.Add(new Pixel("Vivid", "Purple", 35.7, 36.9, -23.6, 43.8, 327));

            #endregion VIVID

            #region PALE 

            cielabIndices.Add(new Pixel("Pale", "Red", 79.8, 21.9, 10.9, 24.5, 26));
            cielabIndices.Add(new Pixel("Pale", "Orange", 79.6, 13.1, 22.2, 25.8, 59));
            cielabIndices.Add(new Pixel("Pale", "Yellow", 79.4, 1.4, 25.2, 25.3, 87));
            cielabIndices.Add(new Pixel("Pale", "Green", 79.5, -19.4, 12.6, 23.1, 147));
            cielabIndices.Add(new Pixel("Pale", "Cyan", 80.8, -13.3, -5.9, 14.5, 204));
            cielabIndices.Add(new Pixel("Pale", "Blue", 80.6, -1.9, -24.3, 24.4, 265));
            cielabIndices.Add(new Pixel("Pale", "Purple", 80.3, 19.5, -12.1, 23.0, 328));

            #endregion PALE

            #region DULL 

            cielabIndices.Add(new Pixel("Dull", "Red", 56.9, 21.8, 10.6, 24.2, 26));
            cielabIndices.Add(new Pixel("Dull", "Orange", 56.5, 12.8, 21.9, 25.3, 60));
            cielabIndices.Add(new Pixel("Dull", "Yellow", 56.4, 1.0, 25.1, 25.1, 88));
            cielabIndices.Add(new Pixel("Dull", "Green", 56.6, -19.5, 12.6, 23.2, 147));
            cielabIndices.Add(new Pixel("Dull", "Cyan", 57.0, -12.9, -6.3, 14.3, 206));
            cielabIndices.Add(new Pixel("Dull", "Blue", 57.6, -2.0, -24.4, 24.5, 265));
            cielabIndices.Add(new Pixel("Dull", "Purple", 57.3, 19.3, -12.3, 22.9, 327));

            #endregion DULL

            #region DARK 

            cielabIndices.Add(new Pixel("Dark", "Red", 34.4, 21.0, 10.4, 23.4, 26));
            cielabIndices.Add(new Pixel("Dark", "Orange", 34.0, 12.1, 21.3, 24.5, 60));
            cielabIndices.Add(new Pixel("Dark", "Yellow", 34.0, 1.1, 24.3, 24.3, 87));
            cielabIndices.Add(new Pixel("Dark", "Green", 34.0, -18.6, 12.3, 22.3, 146));
            cielabIndices.Add(new Pixel("Dark", "Cyan", 34.4, -12.6, -5.8, 13.9, 205));
            cielabIndices.Add(new Pixel("Dark", "Blue", 35.0, -1.4, -24.1, 24.2, 267));
            cielabIndices.Add(new Pixel("Dark", "Purple", 34.8, 18.4, -11.8, 21.9, 327));

            #endregion DARK

            #region LIGHT GRAYISH 

            cielabIndices.Add(new Pixel("Light grayish", "Red", 79.8, 11.1, 5.8, 12.5, 28));
            cielabIndices.Add(new Pixel("Light grayish", "Orange", 79.7, 6.7, 11.3, 13.1, 60));
            cielabIndices.Add(new Pixel("Light grayish", "Yellow", 79.6, 0.9, 12.9, 13.0, 86));
            cielabIndices.Add(new Pixel("Light grayish", "Green", 79.6, -9.7, 6.6, 11.7, 146));
            cielabIndices.Add(new Pixel("Light grayish", "Cyan", 79.9, -6.8, -2.8, 7.3, 203));
            cielabIndices.Add(new Pixel("Light grayish", "Blue", 80.8, -0.9, -12.0, 12.0, 266));
            cielabIndices.Add(new Pixel("Light grayish", "Purple", 79.8, 9.8, -6.1, 11.6, 328));

            #endregion LIGHT GRAYISH

            #region GRAYISH 

            cielabIndices.Add(new Pixel("Grayish", "Red", 56.9, 11.0, 5.3, 12.2, 26));
            cielabIndices.Add(new Pixel("Grayish", "Orange", 56.6, 6.2, 11.1, 12.7, 61));
            cielabIndices.Add(new Pixel("Grayish", "Yellow", 56.6, 0.8, 12.4, 12.4, 87));
            cielabIndices.Add(new Pixel("Grayish", "Green", 56.6, -10.0, 6.5, 11.9, 147));
            cielabIndices.Add(new Pixel("Grayish", "Cyan", 56.8, -6.6, -3.3, 7.3, 207));
            cielabIndices.Add(new Pixel("Grayish", "Blue", 57.1, -0.9, -12.3, 12.4, 266));
            cielabIndices.Add(new Pixel("Grayish", "Purple", 57.1, 9.4, -5.9, 11.2, 328));

            #endregion GRAYISH

            #region DARK GRAYISH 

            cielabIndices.Add(new Pixel("Dark grayish", "Red", 34.1, 10.5, 5.3, 11.7, 27));
            cielabIndices.Add(new Pixel("Dark grayish", "Orange", 34.1, 6.1, 10.7, 12.3, 60));
            cielabIndices.Add(new Pixel("Dark grayish", "Yellow", 34.0, 0.6, 12.3, 12.3, 87));
            cielabIndices.Add(new Pixel("Dark grayish", "Green", 34.0, -9.6, 6.3, 11.5, 147));
            cielabIndices.Add(new Pixel("Dark grayish", "Cyan", 34.2, -6.3, -2.9, 7.0, 204));
            cielabIndices.Add(new Pixel("Dark grayish", "Blue", 34.6, -0.9, -12.0, 12.0, 266));
            cielabIndices.Add(new Pixel("Dark grayish", "Purple", 34.6, 9.2, -5.6, 10.8, 329));

            #endregion DARK GRAYISH

            #region ACHROMATIC 

            cielabIndices.Add(new Pixel("Achromatic", "Black", 2.3, -0.1, -0.4, 0.4, 262));
            cielabIndices.Add(new Pixel("Achromatic", "Dark gray", 34.2, -0.2, 0.2, 0.3, 124));
            cielabIndices.Add(new Pixel("Achromatic", "Mid gray", 56.9, -0.1, 0.3, 0.3, 115));
            cielabIndices.Add(new Pixel("Achromatic", "Light gray", 80.0, 3.2, -0.9, 3.4, 345));
            cielabIndices.Add(new Pixel("Achromatic", "White", 100.0, -0.1, 0.3, 0.3, 106));

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
            double normalizedRed = (double)red / 255.0;
            double normalizedGreen = (double)green / 255.0;
            double normalizedBlue = (double)blue / 255.0;

            // convert to a RGB form
            double r = (normalizedRed > 0.04045) ? Math.Pow((normalizedRed + 0.055) / (
                1 + 0.055), 2.2) : (normalizedRed / 12.92);
            double g = (normalizedGreen > 0.04045) ? Math.Pow((normalizedGreen + 0.055) / (
                1 + 0.055), 2.2) : (normalizedGreen / 12.92);
            double b = (normalizedBlue > 0.04045) ? Math.Pow((normalizedBlue + 0.055) / (
                1 + 0.055), 2.2) : (normalizedBlue / 12.92);

            // convert
            return new CIEXYZ
                (
                    (r * 0.4124 + g * 0.3576 + b * 0.1805),
                    (r * 0.2126 + g * 0.7152 + b * 0.0722),
                    (r * 0.0193 + g * 0.1192 + b * 0.9505)
                );
        }

        public List<double> LabToRGB(CIELab cielab)
        {
            var y = (cielab.L + 16) / 116;
            var x = cielab.A / 500 + y;
            var z = y - cielab.B / 200;
            double r, g, b;

            x = 0.95047 * ((x * x * x > 0.008856) ? x * x * x : (x - 16 / 116) / 7.787);
            y = 1.00000 * ((y * y * y > 0.008856) ? y * y * y : (y - 16 / 116) / 7.787);
            z = 1.08883 * ((z * z * z > 0.008856) ? z * z * z : (z - 16 / 116) / 7.787);

            r = x * 3.2406 + y * -1.5372 + z * -0.4986;
            g = x * -0.9689 + y * 1.8758 + z * 0.0415;
            b = x * 0.0557 + y * -0.2040 + z * 1.0570;

            r = (r > 0.0031308) ? (1.055 * Math.Pow(r, 1 / 2.4) - 0.055) : 12.92 * r;
            g = (g > 0.0031308) ? (1.055 * Math.Pow(g, 1 / 2.4) - 0.055) : 12.92 * g;
            b = (b > 0.0031308) ? (1.055 * Math.Pow(b, 1 / 2.4) - 0.055) : 12.92 * b;

            return new List<double>()
            {
                Math.Max(0, Math.Min(1, r)) * 255,
                    Math.Max(0, Math.Min(1, g)) * 255,
                    Math.Max(0, Math.Min(1, b)) * 255
            };
        }

        public double GetHueFromRGB(double r, double g, double b)
        {
            var min = Math.Min(r, Math.Min(g, b));
            var max = Math.Max(r, Math.Max(g, b));

            double hue = 0;

            if (r == max)
                hue = (g - b) / (max - min);
            if (g == max)
                hue = 2 + (b - r) / (max - min);
            if (b == max)
                hue = 4 + (r - g) / (max - min);

            return hue;
        }

        public double GetHue(double L, double a, double b)
        {
            var rgb = LabToRGB(new CIELab(L, a, b));
            return GetHueFromRGB(rgb[0], rgb[1], rgb[2]);
        }
    }
}
