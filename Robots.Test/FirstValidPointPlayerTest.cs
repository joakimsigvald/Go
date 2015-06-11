using Go.Engine;
using Go.Models;
using Go.Robots;
using NUnit.Framework;

namespace Robots.Test
{
    [TestFixture]
    public class FirstValidPointPlayerTest
    {
        [Test]
        public void WhenBoardIsEmpty_ThenPlaysInUpperLeft()
        {
            var smartBoard = new SmartBoard(new Board(3));
            var fvpPlayer = new FirstValidPointPlayer(smartBoard);
            Assert.That(fvpPlayer.GetMove().Position, Is.EqualTo(new Position(1, 1)));
        }
    }
}
