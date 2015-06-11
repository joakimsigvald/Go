using System;
using Go.Engine;
using Go.Models;
using NSubstitute;
using NUnit.Framework;

namespace Go.Gaming.Test.GameTests
{
    [TestFixture]
    public class WhenGameStarts
    {
        private Game _game;
        private IPlayer _blackPlayer;
        private IPlayer _whitePlayer;

        [SetUp]
        public void SetUp()
        {
            var rules = MockRules();
            _blackPlayer = MockPlayer(BoardState.Black);
            _whitePlayer = MockPlayer(BoardState.White);
            var communicator = Substitute.For<ICommunicator>();
            _game = new Game(rules, communicator, _blackPlayer, _whitePlayer);
            _game.Play();
        }

        private static ISmartBoard MockRules()
        {
            var rules = Substitute.For<ISmartBoard>();
            rules.IsValid(Arg.Any<Move>()).Returns(true);
            return rules;
        }

        private static IPlayer MockPlayer(BoardState color)
        {
            var player = Substitute.For<IPlayer>();
            player.Color.Returns(color);
            player.GetMove().Returns(new Move(color, Position.Resign));
            player.When(p => p.GetMove(true)).Throw(new InvalidOperationException());
            return player;
        }

        [Test]
        public void ThenBlackPlaysFirst()
        {
            _blackPlayer.Received().GetMove();
            _whitePlayer.DidNotReceive().GetMove();
        }
    }
}
