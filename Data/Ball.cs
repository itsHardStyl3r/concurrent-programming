namespace Data
{
    public class Ball : IEntity
    {
        private static readonly Random random = new Random();
        private readonly object _syncLock = new object();
        private double _x;
        private double _y;

        public double X
        {
            get
            {
                lock (_syncLock) return _x;
            }
            set
            {
                lock (_syncLock) _x = value;
            }
        }
        public double Y
        {
            get
            {
                lock (_syncLock) return _y;
            }
            set
            {
                lock (_syncLock) _y = value;
            }
        }

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

        public void Move(int maxWidth, int maxHeight)
        {
            lock (_syncLock)
            {
                X += MovX;
                Y += MovY;
                if (X <= 0 || X + Radius >= maxWidth)
                    MovX = -MovX;
                if (Y <= 0 || Y + Radius >= maxHeight)
                    MovY = -MovY;
            }
        }
    }
}