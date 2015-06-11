using System.Linq;
using Go.Models;
using NUnit.Framework;

namespace Go.Engine.Test.SmartBoardTests
{
    [TestFixture]
    public class WhenSuperKo : SmartBoardTestBase
    {
        readonly Move[] _blackRepeatedMoves = new[]{
                new Position(4, 2)
                , new Position(4, 6)
                , new Position(4, 4)
                }.Select(Move.Black).ToArray();

        readonly Move[] _whiteRepeatedMoves = new[]{
                new Position(3, 4)
                , new Position(3, 2) //<-- capturing sequence starts here
                , new Position(3, 6)
                }.Select(Move.White).ToArray();

        [SetUp]
        public void SetUp()
        {
            Given(@"
                ..0A...
                .1I7B..
                ..2C..
                .3H9D..
                ..4E...
                .5J8F..
                ..6G..."
                , _blackRepeatedMoves[0]
                , _whiteRepeatedMoves[0]
                , _blackRepeatedMoves[1]
                , _whiteRepeatedMoves[1]
                , _blackRepeatedMoves[2]);
        }

        [Test]
        public void ThenLastMoveOfFirstRepititionIsInvalidIsInvalid()
        {
            Assert.IsFalse(SmartBoard.IsValidPlacement(_whiteRepeatedMoves[2]));
        }
    }
}
