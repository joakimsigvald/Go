using System;
using System.Collections.Generic;
using System.Linq;
using Go.Models;

namespace Go.Engine
{
    public class SmartBoard : ISmartBoard
    {
        private readonly IBoard _board;
        private readonly Groups _groups;
        private readonly MoveStack _moves;
        private readonly IDictionary<BoardState, int> _prisoners = new Dictionary<BoardState, int>
        {
            {BoardState.Black, 0}
            , {BoardState.White, 0}
        };
        private List<Position> _hasBeenOccupied;

        public SmartBoard(IBoard board)
        {
            _board = board;
            _groups = new Groups();
            _moves = new MoveStack();
            _hasBeenOccupied = new List<Position>();
            if (!board.AllEmpty) 
                PreparateSmartBoard();
        }

        private SmartBoard(SmartBoard original)
        {
            _board = original._board.Copy();
            _groups = original._groups.Copy();
            _moves = original._moves.Copy();
            _hasBeenOccupied = original._hasBeenOccupied.ToList();
            _prisoners = original._prisoners.ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        public IBoardView BoardView
        {
            get { return _board; }
        }

        public bool IsValid(Move move)
        {
            return move.IsPass || move.IsResign || IsValidPlacement(move);
        }

        public void MakeMove(Move move)
        {
            if (move.IsPlacement)
            {
                PlaceStone(move);
            }
            _moves.Push(move);
        }

        public bool IsValidPlacement(Move move)
        {
            var stone = move.Position;
            var color = move.Color;
            return IsEmpty(stone) && !IsSuicide(stone, color) &&
                   (IsPristine(stone) || !(IsKo(stone, color) || _moves.GivesSuperKo(move)));
        }

        public int GetPrisoners(BoardState color)
        {
            return _prisoners[color];
        }

        public ISmartBoard Copy()
        {
            return new SmartBoard(this);
        }

        public void Remove(List<Position> stones)
        {
            _board.Remove(stones);
        }

        public bool IsPristine(Position position)
        {
            return !_hasBeenOccupied.Contains(position);
        }

        public bool IsEmpty(Position position)
        {
            return _board.IsEmpty(position);
        }

        private void PreparateSmartBoard()
        {
            _hasBeenOccupied = _board.Positions.Where(_board.IsOccupied).ToList();
            _hasBeenOccupied.ForEach(p => _groups.Add(p, _board.GetState(p)));
        }

        private void PlaceStone(Move move)
        {
            if (!IsValidPlacement(move)) throw new ArgumentException(CreateErrorMessage(move));
            _board.PutStone(move.Position, move.Color);
            _hasBeenOccupied.Add(move.Position);
            var capturedStones = AddStone(move.Position, move.Color);
            _board.Remove(capturedStones);
            _prisoners[move.Color] += capturedStones.Count;
        }

        private bool IsKo(Position stone, BoardState color)
        {
            if (HasEmptyNeighbour(stone) || HasFriendlyNeighbour(stone, color)) return false;
            var killableGroups = GetKillableGroups(stone, color).ToList();
            if (killableGroups.Count() != 1) return false;
            var singleKillableGroup = killableGroups.Single();
            return singleKillableGroup.Size == 1 && Equals(singleKillableGroup.FirstStone, _moves.Last.Position);
        }

        private List<Position> AddStone(Position position, BoardState color)
        {
            _groups.Add(position, color);
            return RemoveCapturedGroups(position, color);
        }

        private string CreateErrorMessage(Move move)
        {
            if (!IsEmpty(move.Position))
            {
                return "Occupied";
            }
            if (IsSuicide(move.Position, move.Color))
            {
                return "Suicide";
            }
            if (IsKo(move.Position, move.Color))
            {
                return "Ko";
            }
            if (_moves.GivesSuperKo(move))
            {
                return "Super ko";
            }
            return "Error";
        }

        private bool IsSuicide(Position position, BoardState color)
        {
            return !HasEmptyNeighbour(position)
                   && !GetKillableGroups(position, color).Any()
                   && _groups.GetFriendlyNeighbours(position, color).All(IsAtari);
        }

        private IEnumerable<Group> GetKillableGroups(Position stone, BoardState color)
        {
            return _groups.GetEnemyNeighbours(stone, color).Where(IsAtari);
        }

        private bool HasEmptyNeighbour(Position position)
        {
            return position.Neighbours.Any(IsEmpty);
        }

        private bool HasFriendlyNeighbour(Position position, BoardState color)
        {
            return position.Neighbours.Any(pos => _groups[pos].Color == color);
        }

        private List<Position> RemoveCapturedGroups(Position position, BoardState color)
        {
            return GetCapturedGroups(position, color).
                SelectMany(_groups.Remove).
                ToList();
        }

        private IEnumerable<Group> GetCapturedGroups(Position position, BoardState color)
        {
            return _groups.GetEnemyNeighbours(position, color).Where(IsCaptured);
        }

        private bool IsAtari(Group group)
        {
            return group.Neighbours.Count(IsEmpty) == 1;
        }

        private bool IsCaptured(Group group)
        {
            return !group.Neighbours.Any(IsEmpty);
        }
    }
}
