using System;
using System.Collections.Generic;
using System.Linq;
using Go.Models;

namespace Go.Engine
{
    public class MoveStack
    {
        private readonly List<Move> _moves;

        public MoveStack()
        {
            _moves = new List<Move>();
        }

        private MoveStack(MoveStack original)
        {
            _moves = original._moves.ToList();
        }

        public void Push(Move move)
        {
            _moves.Insert(0, move);
        }

        public Move Last
        {
            get { return _moves.FirstOrDefault() ?? Move.NoMove; }
        }

        public bool GivesSuperKo(Move nextMove)
        {
            return GetAllCurrentSequencePairsSeparatedByOne().
                Where(SequencesAreEqual).
                Select(GetMoveBetween).
                Any(move => Equals(move, nextMove));
        }

        public MoveStack Copy()
        {
            return new MoveStack(this);
        }

        private static bool SequencesAreEqual(Tuple<List<Move>, List<Move>> pair)
        {
            return pair.Item1.SequenceEqual(pair.Item2);
        }

        private Move GetMoveBetween(Tuple<List<Move>, List<Move>> pair)
        {
            return GetPrevious(pair.Item2.Count);
        }

        private Move GetPrevious(int step)
        {
            if (step >= _moves.Count) throw new ArgumentOutOfRangeException("step");
            return _moves.Skip(step).First();
        }

        private IEnumerable<Tuple<List<Move>, List<Move>>> GetAllCurrentSequencePairsSeparatedByOne()
        {
            for (int seqLen = 2; seqLen < (_moves.Count + 1) / 2; seqLen++)
            {
                var currentSequence = _moves.Take(seqLen).ToList();
                var oldSequence = _moves.Skip(seqLen + 1).Take(seqLen).ToList();
                yield return new Tuple<List<Move>, List<Move>>(oldSequence, currentSequence);
            }
        }
    }
}
