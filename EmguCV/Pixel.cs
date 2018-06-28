namespace EmguCV
{
    public class Pixel
    {
        public string Tone { get; set; }

        public string Color { get; set; }

        public double L { get; set; }

        public double a { get; set; }

        public double b { get; set; }

        public double Chroma { get; set; }

        public double HueAngle { get; set; }

        public double Hue { get; set; }

        public Pixel()
        {

        }

        public Pixel(string tone, string color, double l, double a, double b, double chroma, double hueAngle)
        {
            Tone = tone;
            Color = color; 
            L = l;
            this.a = a;
            this.b = b;
            Chroma = chroma;
            HueAngle = hueAngle;
        }
    }
}
