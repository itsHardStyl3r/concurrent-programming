using Data;

namespace Tesitng
{
    [TestFixture]
    public class BallTest
    {
        [Test]
        public void TestSetterAndGetter()
        {
            IEntity ballData = new Ball();
            double x = 20;
            double y = 20;
            double rad = 20;
            double movX = 8;
            double movY = -4;

            ballData.X = x;
            ballData.Y = y;
            ballData.Radius = rad;
            ballData.MovX = movX;
            ballData.MovY = movY;

            Assert.That(ballData.X, Is.EqualTo(x));
            Assert.That(ballData.Y, Is.EqualTo(y));
            Assert.That(rad, Is.EqualTo(ballData.Radius));
            Assert.That(movX, Is.EqualTo(ballData.MovX));
            Assert.That(movY, Is.EqualTo(ballData.MovY));
        }
    }
}