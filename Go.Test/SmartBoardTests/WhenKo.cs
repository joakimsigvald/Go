using Go.Models;
using NUnit.Framework;

namespace Go.Engine.Test.SmartBoardTests
{
    [TestFixture]
    public class WhenKo : SmartBoardTestBase
    {
        readonly Position _takeKo = new Position(4, 3);

        [SetUp]
        public void SetUp()
        {
            Given(@"
            .....
            ..0A.
            .3D1B
            ..2C.
            .....");
        }

        [Test]
        public void ThenImmediateKoCaptureIsInvalid()
        {
            Assert.False(SmartBoard.IsValidPlacement(Move.Black(_takeKo)));
        }

        [Test]
        public void ThenKoCaptureIsValidAfterExchange()
        {
            var blackThreat = new Position(1, 1);
            var whiteResponce = new Position(1, 5);
            When(Move.Black(blackThreat), Move.White(whiteResponce));
            Assert.That(SmartBoard.IsValidPlacement(Move.Black(_takeKo)));
        }
    }
}
