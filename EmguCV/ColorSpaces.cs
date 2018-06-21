using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmguCV
{
    public class ColorSpaces
    {
        public struct RGB
        {
            /// <summary>
            /// Gets an empty RGB structure;
            /// </summary>
            public static readonly RGB Empty = new RGB();

            private int red;
            private int green;
            private int blue;

            public static bool operator ==(RGB item1, RGB item2)
            {
                return (
                    item1.Red == item2.Red
                    && item1.Green == item2.Green
                    && item1.Blue == item2.Blue
                    );
            }

            public static bool operator !=(RGB item1, RGB item2)
            {
                return (
                    item1.Red != item2.Red
                    || item1.Green != item2.Green
                    || item1.Blue != item2.Blue
                    );
            }

            /// <summary>
            /// Gets or sets red value.
            /// </summary>
            public int Red
            {
                get
                {
                    return red;
                }
                set
                {
                    red = (value > 255) ? 255 : ((value < 0) ? 0 : value);
                }
            }

            /// <summary>
            /// Gets or sets red value.
            /// </summary>
            public int Green
            {
                get
                {
                    return green;
                }
                set
                {
                    green = (value > 255) ? 255 : ((value < 0) ? 0 : value);
                }
            }

            /// <summary>
            /// Gets or sets red value.
            /// </summary>
            public int Blue
            {
                get
                {
                    return blue;
                }
                set
                {
                    blue = (value > 255) ? 255 : ((value < 0) ? 0 : value);
                }
            }

            public RGB(int R, int G, int B)
            {
                this.red = (R > 255) ? 255 : ((R < 0) ? 0 : R);
                this.green = (G > 255) ? 255 : ((G < 0) ? 0 : G);
                this.blue = (B > 255) ? 255 : ((B < 0) ? 0 : B);
            }

            public override bool Equals(Object obj)
            {
                if (obj == null || GetType() != obj.GetType()) return false;

                return (this == (RGB)obj);
            }

            public override int GetHashCode()
            {
                return Red.GetHashCode() ^ Green.GetHashCode() ^ Blue.GetHashCode();
            }
        }

        public struct HSB
        {
            /// <summary>
            /// Gets an empty HSB structure;
            /// </summary>
            public static readonly HSB Empty = new HSB();

            private double hue;
            private double saturation;
            private double brightness;

            public static bool operator ==(HSB item1, HSB item2)
            {
                return (
                    item1.Hue == item2.Hue
                    && item1.Saturation == item2.Saturation
                    && item1.Brightness == item2.Brightness
                    );
            }

            public static bool operator !=(HSB item1, HSB item2)
            {
                return (
                    item1.Hue != item2.Hue
                    || item1.Saturation != item2.Saturation
                    || item1.Brightness != item2.Brightness
                    );
            }

            /// <summary>
            /// Gets or sets the hue component.
            /// </summary>
            public double Hue
            {
                get
                {
                    return hue;
                }
                set
                {
                    hue = (value > 360) ? 360 : ((value < 0) ? 0 : value);
                }
            }

            /// <summary>
            /// Gets or sets saturation component.
            /// </summary>
            public double Saturation
            {
                get
                {
                    return saturation;
                }
                set
                {
                    saturation = (value > 1) ? 1 : ((value < 0) ? 0 : value);
                }
            }

            /// <summary>
            /// Gets or sets the brightness component.
            /// </summary>
            public double Brightness
            {
                get
                {
                    return brightness;
                }
                set
                {
                    brightness = (value > 1) ? 1 : ((value < 0) ? 0 : value);
                }
            }

            /// <summary>
            /// Creates an instance of a HSB structure.
            /// </summary>
            /// <param name="h">Hue value.</param>
            /// <param name="s">Saturation value.</param>
            /// <param name="b">Brightness value.</param>
            public HSB(double h, double s, double b)
            {
                hue = (h > 360) ? 360 : ((h < 0) ? 0 : h);
                saturation = (s > 1) ? 1 : ((s < 0) ? 0 : s);
                brightness = (b > 1) ? 1 : ((b < 0) ? 0 : b);
            }

            public override bool Equals(Object obj)
            {
                if (obj == null || GetType() != obj.GetType()) return false;

                return (this == (HSB)obj);
            }

            public override int GetHashCode()
            {
                return Hue.GetHashCode() ^ Saturation.GetHashCode() ^
                    Brightness.GetHashCode();
            }
        }

        public struct CIELab
        {
            /// <summary>
            /// Gets an empty CIELab structure.
            /// </summary>
            public static readonly CIELab Empty = new CIELab();

            private double l;
            private double a;
            private double b;


            public static bool operator ==(CIELab item1, CIELab item2)
            {
                return (
                    item1.L == item2.L
                    && item1.A == item2.A
                    && item1.B == item2.B
                    );
            }

            public static bool operator !=(CIELab item1, CIELab item2)
            {
                return (
                    item1.L != item2.L
                    || item1.A != item2.A
                    || item1.B != item2.B
                    );
            }


            /// <summary>
            /// Gets or sets L component.
            /// </summary>
            public double L
            {
                get
                {
                    return this.l;
                }
                set
                {
                    this.l = value;
                }
            }

            /// <summary>
            /// Gets or sets a component.
            /// </summary>
            public double A
            {
                get
                {
                    return this.a;
                }
                set
                {
                    this.a = value;
                }
            }

            /// <summary>
            /// Gets or sets a component.
            /// </summary>
            public double B
            {
                get
                {
                    return this.b;
                }
                set
                {
                    this.b = value;
                }
            }

            public CIELab(double l, double a, double b)
            {
                this.l = l;
                this.a = a;
                this.b = b;
            }

            public override bool Equals(Object obj)
            {
                if (obj == null || GetType() != obj.GetType()) return false;

                return (this == (CIELab)obj);
            }

            public override int GetHashCode()
            {
                return L.GetHashCode() ^ a.GetHashCode() ^ b.GetHashCode();
            }

        }

        public struct CIEXYZ
        {
            /// <summary>
            /// Gets an empty CIEXYZ structure.
            /// </summary>
            public static readonly CIEXYZ Empty = new CIEXYZ();
            /// <summary>
            /// Gets the CIE D65 (white) structure.
            /// </summary>
            public static readonly CIEXYZ D65 = new CIEXYZ(0.9505, 1.0, 1.0890);


            private double x;
            private double y;
            private double z;

            public static bool operator ==(CIEXYZ item1, CIEXYZ item2)
            {
                return (
                    item1.X == item2.X
                    && item1.Y == item2.Y
                    && item1.Z == item2.Z
                    );
            }

            public static bool operator !=(CIEXYZ item1, CIEXYZ item2)
            {
                return (
                    item1.X != item2.X
                    || item1.Y != item2.Y
                    || item1.Z != item2.Z
                    );
            }

            /// <summary>
            /// Gets or sets X component.
            /// </summary>
            public double X
            {
                get
                {
                    return this.x;
                }
                set
                {
                    this.x = (value > 0.9505) ? 0.9505 : ((value < 0) ? 0 : value);
                }
            }

            /// <summary>
            /// Gets or sets Y component.
            /// </summary>
            public double Y
            {
                get
                {
                    return this.y;
                }
                set
                {
                    this.y = (value > 1.0) ? 1.0 : ((value < 0) ? 0 : value);
                }
            }

            /// <summary>
            /// Gets or sets Z component.
            /// </summary>
            public double Z
            {
                get
                {
                    return this.z;
                }
                set
                {
                    this.z = (value > 1.089) ? 1.089 : ((value < 0) ? 0 : value);
                }
            }

            public CIEXYZ(double x, double y, double z)
            {
                this.x = (x > 0.9505) ? 0.9505 : ((x < 0) ? 0 : x);
                this.y = (y > 1.0) ? 1.0 : ((y < 0) ? 0 : y);
                this.z = (z > 1.089) ? 1.089 : ((z < 0) ? 0 : z);
            }

            public override bool Equals(Object obj)
            {
                if (obj == null || GetType() != obj.GetType()) return false;

                return (this == (CIEXYZ)obj);
            }

            public override int GetHashCode()
            {
                return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
            }

        }
    }
}
