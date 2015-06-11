using System.Collections.Generic;
using System.Linq;

namespace Go.Models
{
    public class Group
    {
        private static int _nextId = 1;

        public static readonly Group NoGroup = new Group(BoardState.Empty, Position.Pass); 

        public readonly BoardState Color;
        private readonly int _id;
        private readonly List<Position> _stones;

        public Group(BoardState color, Position firstStone)
        {
            Color = color;
            _id = _nextId++;
            _stones = new List<Position> { firstStone };
        }

        private Group(Group original)
        {
            Color = original.Color;
            _id = original._id;
            _stones = original._stones.ToList();
        }

        public IEnumerable<Position> Neighbours
        {
            get
            {
                return _stones.SelectMany(s => s.Neighbours).
                    Distinct().
                    Except(_stones);
            }
        }

        public Position FirstStone
        {
            get { return _stones[0]; }
        }

        public List<Position> Stones
        {
            get { return _stones.ToList(); }
        }

        public int Size
        {
            get { return _stones.Count; }
        }

        public void AddStones(IEnumerable<Position> stones)
        {
            _stones.AddRange(stones);
        }

        public override string ToString()
        {
            return string.Format("{{{0}_{1}}}", Color, _id);
        }

        public Group Copy()
        {
            return new Group(this);
        }
    }
}