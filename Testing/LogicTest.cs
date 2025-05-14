using Logic;
using Data;

namespace LogicTesting
{
    [TestFixture]
    public class EntityLogicTests
    {
        private EntityLogic logic;
        private const int Width = 500;
        private const int Height = 500;

        [SetUp]
        public void SetUp()
        {
            logic = new EntityLogic(Width, Height);
        }

        [Test]
        public void TestStart()
        {
            double initialX = logic.EntityData.X;
            double initialY = logic.EntityData.Y;
            bool entityMoved = false;

            logic.EntityData.EntityChanged += (x, y) =>
            {
                if (x != initialX || y != initialY) entityMoved = true;
            };

            logic.Start(Width, Height);
            bool changed = SpinWait.SpinUntil(() => entityMoved, 1000);
            logic.Stop();

            Assert.That(changed);
        }

        [Test]
        public void TestStop()
        {
            logic.Start(Width, Height);
            logic.Stop();
            double xAfterStop = logic.EntityData.X;
            double yAfterStop = logic.EntityData.Y;

            Assert.That(logic.EntityData.X, Is.EqualTo(xAfterStop));
            Assert.That(logic.EntityData.Y, Is.EqualTo(yAfterStop));
        }

        [Test]
        public void TestHasCollided_Overlap()
        {
            var entity1 = new EntityLogic(Ball.CreateBall(100, 100, 0, 0, 20, 10));
            var entity2 = new EntityLogic(Ball.CreateBall(105, 105, 0, 0, 20, 10));

            bool collided = entity1.HasCollided(entity2);
            Assert.That(collided);
        }

        [Test]
        public void TestHasCollided_NoOverlap()
        {
            var entity1 = new EntityLogic(Ball.CreateBall(0, 0, 0, 0, 20, 10));
            var entity2 = new EntityLogic(Ball.CreateBall(500, 500, 0, 0, 20, 10));

            bool collided = entity1.HasCollided(entity2);
            Assert.That(!collided);
        }

        [Test]
        public void TestResolveCollision_Collision()
        {
            var entity1 = new EntityLogic(Ball.CreateBall(100, 100, 2, 0, 30, 10));
            var entity2 = new EntityLogic(Ball.CreateBall(120, 100, -2, 0, 30, 10));

            if (!entity1.HasCollided(entity2)) entity2.EntityData.X = 110;

            double vx1Before = entity1.EntityData.MovX;
            double vx2Before = entity2.EntityData.MovX;

            entity1.ResolveCollision(entity2);

            double vx1After = entity1.EntityData.MovX;
            double vx2After = entity2.EntityData.MovX;

            Assert.That(vx1After, Is.LessThan(vx1Before));
            Assert.That(vx2After, Is.GreaterThan(vx2Before));
        }

        [Test]
        public void TestResolveCollision_NoCollision()
        {
            var entity1 = new EntityLogic(Ball.CreateBall(100, 100, -2, 0, 30, 10));
            var entity2 = new EntityLogic(Ball.CreateBall(120, 100, 2, 0, 30, 10));

            double vx1Before = entity1.EntityData.MovX;
            double vx2Before = entity2.EntityData.MovX;

            entity1.ResolveCollision(entity2);

            Assert.That(entity1.EntityData.MovX, Is.EqualTo(vx1Before));
            Assert.That(entity2.EntityData.MovX, Is.EqualTo(vx2Before));
        }
    }
}
