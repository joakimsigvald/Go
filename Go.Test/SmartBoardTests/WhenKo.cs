using Go.Models;
using NUnit.Framework;

namespace Go.Engine.Test.SmartBoardTests
{
    [TestFixture]
    public class WhenKo
    {
        readonly Position _blackCapture = new Position(4, 3);
        readonly Position _blackThreat = new Position(1, 1);
        readonly Position _whiteResponce = new Position(1, 5);

        private SmartBoard _smartBoard;

        [SetUp]
        public void SetUp()
        {
            _smartBoard = SmartBoardGenerator.CreateBoard(@"
            .....
            ..0A.
            .3D1B
            ..2C.
            .....");
        }

        [Test]
        public void ThenImmediateKoCaptureIsInvalid()
        {
            Assert.False(_smartBoard.IsValidPlacement(Move.Black(_blackCapture)));
        }

        [Test]
        public void ThenKoCaptureIsValidAfterExchange()
        {
            _smartBoard.MakeMove(Move.Black(_blackThreat));
            _smartBoard.MakeMove(Move.White(_whiteResponce));
            Assert.That(_smartBoard.IsValidPlacement(Move.Black(_blackCapture)));
        }
    }
}
