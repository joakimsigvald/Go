using System.Collections.Generic;
using System.Linq;
using Go.Models;

namespace Go.Engine
{
    public class Groups
    {
        private readonly IDictionary<Position, Group> _groups = new Dictionary<Position, Group>();

        public Groups()
        {
        }

        private Groups(Groups original)
        {
            _groups = original._groups.ToDictionary(p => p.Key, p => p.Value.Copy());
        }

        public Group this[Position pos]
        {
            get
            {
                Group group;
                return _groups.TryGetValue(pos, out group) ? group : Group.NoGroup;
            }
        }

        public IEnumerable<Group> GetFriendlyNeighbours(Position position, BoardState color)
        {
            return position.Neighbours.
                Where(p => IsSameColor(p, color)).
                Select(p => _groups[p]).
                Distinct();
        }

        public IEnumerable<Group> GetEnemyNeighbours(Position position, BoardState color)
        {
            return position.Neighbours.
                Where(p => IsOppositeColor(p, color)).
                Select(p => _groups[p]).Distinct();
        }

        public Group Add(Position position, BoardState color)
        {
            var group = new Group(color, position);
            MergeWithNeighbouringGroups(@group);
            Add(@group);
            return @group;
        }

        public IEnumerable<Position> Remove(Group group)
        {
            group.Stones.ForEach(stone => _groups.Remove(stone));
            return group.Stones;
        }

        public Groups Copy()
        {
            return new Groups(this);
        }

        private void Add(Group group)
        {
            group.Stones.ForEach(s => _groups[s] = group);
        }

        private void MergeWithNeighbouringGroups(Group newGroup)
        {
            var neighbours = GetFriendlyNeighbours(newGroup.FirstStone, newGroup.Color).ToList();
            var stones = neighbours.SelectMany(g => g.Stones).ToList();
            newGroup.AddStones(stones);
        }

        private bool IsSameColor(Position position, BoardState color)
        {
            return this[position].Color == color;
        }

        private bool IsOppositeColor(Position position, BoardState color)
        {
            return (int)this[position].Color == (BoardState.Outside - color);
        }
    }
}