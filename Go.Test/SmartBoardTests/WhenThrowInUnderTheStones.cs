using Go.Models;
using NUnit.Framework;

namespace Go.Engine.Test.SmartBoardTests
{
    [TestFixture]
    public class WhenThrowInUnderTheStones
    {
        readonly Position _whiteThrowIn = new Position(1, 1);
        readonly Position _blackKill = new Position(2, 1);

        private SmartBoard _smartBoard;

        [SetUp]
        public void SetUp()
        {
            _smartBoard = SmartBoardGenerator.CreateBoard(@"
            AB2
            01.
            ...");
            _smartBoard.MakeMove(Move.White(_whiteThrowIn));
        }

        [Test]
        public void ThenCaptureIsValid()
        {
            Assert.That(_smartBoard.IsValidPlacement(Move.Black(_blackKill)));
        }
    }
}
