using System.Drawing;

namespace Data
{
    public class Ball : IEntity
    {
        private static readonly Random random = new Random();

        public double X { get; set; }
        public double Y { get; set; }
        public double MovX { get; set; }
        public double MovY { get; set; }
        public Color Color { get; set; }
        public double Radius { get; set; }

        public static Ball CreateBall(int maxWidth, int maxHeight)
        {
            double radius = random.Next(20, 50);
            return new Ball
            {
                Radius = radius,
                Color = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256)),
                X = random.Next(12, maxWidth - (int)radius),
                Y = random.Next(12, maxHeight - (int)radius),
                MovX = random.Next(2, 5) * (random.Next(2) == 0 ? 1 : -1),
                MovY = random.Next(2, 5) * (random.Next(2) == 0 ? 1 : -1),
            };
        }
    }
}