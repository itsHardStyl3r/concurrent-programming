using Data;
using Logic;

namespace Testing
{
    public class LogicBall : IEntity
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double MovX { get; set; }
        public double MovY { get; set; }
        public double Radius { get; set; }
    }

    [TestFixture]
    public class LogicTest
    {
        [Test]
        public void BallLogicTestFields()
        {
            IEntity testBall = new LogicBall
            {
                X = 10,
                Y = 20,
                MovX = 5,
                MovY = 10,
                Radius = 15
            };
            EntityLogic logic = new EntityLogic(testBall);

            Assert.That(logic.EntityData != null);
            Assert.That(logic.EntityData.X == 10);
            Assert.That(logic.EntityData.Y == 20);
            Assert.That(logic.EntityData.MovX == 5);
            Assert.That(logic.EntityData.MovY == 10);
            Assert.That(logic.EntityData.Radius == 15);
        }
    }
}