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
        public double Mass { get; set; }

        public static IEntity CreateBall(int maxWidth, int maxHeight)
        {
            double radius = random.Next(20, 60);
            return new Ball
            {
                Radius = radius,
                X = random.Next(12, maxWidth - (int)radius),
                Y = random.Next(12, maxHeight - (int)radius),
                MovX = random.Next(2, 5) * (random.Next(2) == 0 ? 1 : -1),
                MovY = random.Next(2, 5) * (random.Next(2) == 0 ? 1 : -1),
                Mass = radius * 0.8
            };
        }

        public void Move(int maxWidth, int maxHeight)
        {
            lock (_syncLock)
            {
                X += MovX;
                Y += MovY;

                if (X <= 0)
                {
                    X = 0;
                    MovX = -MovX;
                }
                else if (X + Radius >= maxWidth)
                {
                    X = maxWidth - Radius;
                    MovX = -MovX;
                }

                if (Y <= 0)
                {
                    Y = 0;
                    MovY = -MovY;
                }
                else if (Y + Radius >= maxHeight)
                {
                    Y = maxHeight - Radius;
                    MovY = -MovY;
                }
            }
        }
    }
}