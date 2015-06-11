using Go.Models;
using NUnit.Framework;

namespace Go.Engine.Test.SmartBoardTests
{
    [TestFixture]
    public class Test3X3
    {
        private SmartBoard _smartBoard;

        [SetUp]
        public void SetUp()
        {
            _smartBoard = SmartBoardGenerator.CreateBoard(@"
            AC3
            10.
            B2.");
        }

        [Test]
        public void TestCaptureOneStoneAfterCapturesNoLiberties()
        {
            Move whiteSacrifice = Move.White(new Position(1, 1));
            Move blackKill = Move.Black(new Position(2, 1));
            _smartBoard.MakeMove(whiteSacrifice);
            _smartBoard.MakeMove(blackKill);
            Assert.That(_smartBoard.IsEmpty(whiteSacrifice.Position));
            Assert.IsFalse(_smartBoard.IsPristine(whiteSacrifice.Position));
        }
    }
}
