using System.Linq;
using Go.Models;
using NUnit.Framework;

namespace Go.Engine.Test.SmartBoardTests
{
    [TestFixture]
    public class WhenSuperKo
    {
        readonly Position[] _blackRepeatedMoves = {
                new Position(4, 2)
                , new Position(4, 6)
                , new Position(4, 4)
                };

        readonly Position[] _whiteRepeatedMoves = {
                new Position(3, 4)
                , new Position(3, 2) //<-- capturing sequence starts here
                , new Position(3, 6)
                };

        private SmartBoard _smartBoard;

        [SetUp]
        public void SetUp()
        {
            _smartBoard = SmartBoardGenerator.CreateBoard(@"
                ..0A...
                .1I7B..
                ..2C..
                .3H9D..
                ..4E...
                .5J8F..
                ..6G...");
            for (int i = 0; i < _blackRepeatedMoves.Length - 1; i++)
            {
                _smartBoard.MakeMove(Move.Black(_blackRepeatedMoves[i]));
                _smartBoard.MakeMove(Move.White(_whiteRepeatedMoves[i]));
            }
            _smartBoard.MakeMove(Move.Black(_blackRepeatedMoves.Last()));
        }

        [Test]
        public void ThenLastMoveOfFirstRepititionIsInvalidIsInvalid()
        {
            Assert.False(_smartBoard.IsValidPlacement(Move.White(_whiteRepeatedMoves.Last())));
        }
    }
}
