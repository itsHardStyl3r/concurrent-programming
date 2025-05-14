namespace Data
{
    public class Ball : IEntity
    {
        private static readonly Random random = new Random();
        public double X { get; set; }
        public double Y { get; set; }
        public double MovX { get; set; }
        public double MovY { get; set; }
        public double Radius { get; set; }

        public static IEntity CreateBall(int maxWidth, int maxHeight)
        {
            double radius = random.Next(20, 50);
            return new Ball
            {
                Radius = radius,
                X = random.Next(12, maxWidth - (int)radius),
                Y = random.Next(12, maxHeight - (int)radius),
                MovX = random.Next(2, 5) * (random.Next(2) == 0 ? 1 : -1),
                MovY = random.Next(2, 5) * (random.Next(2) == 0 ? 1 : -1),
            };
        }
    }
}