using System.Linq;
using Go.Models;
using NUnit.Framework;

namespace Go.Engine.Test.MoveStackTests
{
    [TestFixture]
    public class WhenSequenceIsRepeated
    {
        private MoveStack _moveStack;

        private readonly Move[] _moveSequence =
        {
            Move.Black(new Position(1, 1))
            , Move.Black(new Position(2, 2))
            , Move.Black(new Position(3, 3))
        };

        [SetUp]
        public void SetUp()
        {
            _moveStack = new MoveStack();
            _moveSequence.
                Concat(_moveSequence).
                Take(_moveSequence.Count() * 2 - 1).
                ToList().
                ForEach(_moveStack.Push);
        }

        [Test]
        public void ThenCompletingSecondRepititonGivesSuperKo()
        {
            Assert.That(_moveStack.GivesSuperKo(_moveSequence.Last()));
        }
    }
}
