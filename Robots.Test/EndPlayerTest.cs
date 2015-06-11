using Go.Engine;
using Go.Models;
using Go.Robots;
using NUnit.Framework;

namespace Robots.Test
{
    [TestFixture]
    public class EndPlayerTest
    {
        [Test]
        public void When3X3BoardIsEmpty_ThenPasses()
        {
            var smartBoard = new SmartBoard(new Board(3));
            var endPlayer = new EndPlayer(smartBoard) {Color = BoardState.Black};
            Assert.That(endPlayer.GetMove().IsPass);
        }
    }
}
