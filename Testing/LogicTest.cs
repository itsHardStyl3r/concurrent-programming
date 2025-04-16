using Logic;
using NUnit.Framework.Legacy;

namespace Testing
{
    [TestFixture]
    public class LogicTest
    {
        [Test]
        public void BallLogicTestFields()
        {
            EntityLogic logic = new EntityLogic(80, 80);

            ClassicAssert.IsNotNull(logic.EntityData);
            // min 12 from random + 80 from maxWidth and maxHeight.
            ClassicAssert.IsTrue(logic.EntityData.X >= 12 && logic.EntityData.X <= 130);
            ClassicAssert.IsTrue(logic.EntityData.Y >= 12 && logic.EntityData.Y <= 130);
        }
    }
}