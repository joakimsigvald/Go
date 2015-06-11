using Go.Engine;
using Go.Models;
using Go.Robots;
using NUnit.Framework;

namespace Robots.Test
{
    [TestFixture]
    public class SimplePlayerTest
    {
        [Test]
        public void When3X3BoardIsEmpty_ThenPlaysInMiddle()
        {
            var smartBoard = new SmartBoard(new Board(3));
            var simplePlayer = new SimplePlayer(smartBoard) {Color = BoardState.Black};
            Assert.That(simplePlayer.GetMove().Position, Is.EqualTo(new Position(2, 2)));
        }
    }
}
