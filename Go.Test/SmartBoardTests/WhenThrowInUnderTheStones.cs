using Go.Models;
using NUnit.Framework;

namespace Go.Engine.Test.SmartBoardTests
{
    [TestFixture]
    public class WhenThrowInUnderTheStones : SmartBoardTestBase
    {
        readonly Move _whiteThrowIn = Move.White(new Position(1, 1));
        readonly Move _blackKill = Move.Black(new Position(2, 1));

        [SetUp]
        public void SetUp()
        {
            Given(@"
            AB2
            01.
            ...", _whiteThrowIn);
        }

        [Test]
        public void ThenCaptureIsValid()
        {
            Assert.That(SmartBoard.IsValidPlacement(_blackKill));
        }
    }
}
