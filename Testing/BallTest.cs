using Data;

namespace BallTesting
{
    [TestFixture]
    public class BallTest
    {
        private const int Width = 500;
        private const int Height = 500;

        [Test]
        public void TestConstructor()
        {
            int x = 20;
            int y = 20;
            double rad = 20;
            int movX = 8;
            int movY = -4;
            IEntity testBall = Ball.CreateBall(x, y, movX, movY, rad, 1);

            Assert.That(testBall.X == x);
            Assert.That(testBall.Y == y);
            Assert.That(testBall.Radius == testBall.Radius);
            Assert.That(testBall.MovX == testBall.MovX);
            Assert.That(testBall.MovY == testBall.MovY);
        }

        [Test]
        public void TestWallBouncing()
        {
            double radius = 30;
            double mass = 10;

            var entity = Ball.CreateBall(0, 100, -5, 0, radius, mass);
            entity.Move(Width, Height);
            Assert.That(entity.MovX, Is.GreaterThan(0), "Nie odbija sie od lewej sciany.");

            entity = Ball.CreateBall(Width - (int)radius, 100, 5, 0, radius, mass);
            entity.Move(Width, Height);
            Assert.That(entity.MovX, Is.LessThan(0), "Nie odbija sie od prawej sciany.");

            entity = Ball.CreateBall(100, 0, 0, -5, radius, mass);
            entity.Move(Width, Height);
            Assert.That(entity  .MovY, Is.GreaterThan(0), "Nie odbija sie od gornej sciany.");

            entity = Ball.CreateBall(100, Height - (int)radius, 0, 5, radius, mass);
            entity.Move(Width, Height);
            Assert.That(entity.MovY, Is.LessThan(0), "Nie odbija sie od dolnej sciany.");
        }
    }
}