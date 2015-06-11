using Go.Models;
using NUnit.Framework;

namespace Go.Engine.Test.SmartBoardTests
{
    [TestFixture]
    public class WhenCaptureUnderTheStones : SmartBoardTestBase
    {
        readonly Position _throwIn = new Position(1, 1);
        readonly Position _kill = new Position(2, 1);

        [SetUp]
        public void SetUp()
        {
            Given(@"
            AC3
            10.
            B2.", Move.White(_throwIn), Move.Black(_kill));
        }

        [Test]
        public void ThenCapturedPointIsEmpty()
        {
            Assert.That(SmartBoard.IsEmpty(_throwIn));
        }

        [Test]
        public void ThenCapturedPointIsNotPristine()
        {
            Assert.IsFalse(SmartBoard.IsPristine(_throwIn));
        }
    }
}
