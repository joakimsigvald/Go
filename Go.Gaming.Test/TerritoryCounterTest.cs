using Go.Engine;
using Go.Models;
using NUnit.Framework;

namespace Go.Gaming.Test
{
    [TestFixture]
    public class TerritoryCounterTest
    {
        private readonly BoardGenerator _testBoardGenerator = new BoardGenerator();

        [Test]
        public void BoardIsEmpty()
        {
            Test(@"
            ...
            ...
            ...");
        }

        [Test]
        public void BoardIsFull()
        {
            Test(@"
            BBB
            BBB
            BBB");
        }

        [Test]
        public void BoardIsFullOfBlackExceptOne()
        {
            Test(@"
            .BB
            BBB
            BBB", black: 1);
        }

        [Test]
        public void BlackSurroundsLargeArea()
        {
            Test(@"
            ...B
            ...B
            ...B
            BBBB", black: 9);
        }

        [Test]
        public void WhiteSurroundsUpperLeftAndBlackLowerRight()
        {
            Test(@"
            W.WWB
            .WWBB
            WWBB.
            WBBB.
            BBB.B", black: 3, white: 2);
        }

        [Test]
        public void ThereIsOneDame()
        {
            Test(@"
            .WW
            W.B
            BB.", 1, 1);
        }

        [Test]
        public void WhiteSurroundsLargeArea()
        {
            Test(@"
            .......
            .......
            ....W..
            WWWW...
            ....W..
            ....W..
            WWWWWWW", white: 35);
        }

        [Test]
        public void WhiteSurroundsLargeAreaWithBlackPrisoner()
        {
            Test(@"
            .......
            .......
            ....W..
            WWWW...
            ....W..
            .B..W..
            WWWWWWW", white: 36);
        }

        private void Test(string boardSetUp, int black = 0, int white = 0)
        {
            var board = _testBoardGenerator.CreateBoard(boardSetUp);
            var result = new TerritoryCounter().Count(new SmartBoard(board));
            Assert.That(result.Score[BoardState.Black], Is.EqualTo(black), "Black score error");
            Assert.That(result.Score[BoardState.White], Is.EqualTo(white), "White score error");
        }
    }
}