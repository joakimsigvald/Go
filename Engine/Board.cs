using System;
using System.Collections.Generic;
using System.Linq;
using Go.Models;

namespace Go.Engine
{
    public class Board : IBoard
    {
        public event EventHandler BoardChanged;

        private readonly BoardState[,] _grid;
        private List<List<Position>> _rows;
        private List<Position> _positions;

        public Board(int size)
        {
            Size = size;
            _grid = new BoardState[Size + 2, Size + 2];
            InitializeBoard();
        }

        public int Size { get; private set; }

        public IEnumerable<Position> EmptyPoints
        {
            get { return _positions.Where(IsEmpty); }
        }

        public List<Position> Positions
        {
            get { return _positions ?? (_positions = GetPositions().ToList()); }
        }

        public List<List<Position>> Rows
        {
            get { return _rows ?? (_rows = GetRows().ToList()); }
        }

        public BoardState GetState(Position position)
        {
            return _grid[position.X, position.Y];
        }

        public void Remove(List<Position> positions)
        {
            if (!positions.Any()) return;
            positions.ForEach(pos => SetState(pos, BoardState.Empty));
            OnBoardChanged();
        }

        public IBoard Copy()
        {
            Board clone = new Board(Size);
            Positions.ForEach(p => clone.SetState(p, GetState(p)));
            return clone;
        }

        public IEnumerable<Position> GetLiberties(Position position)
        {
            return position.Neighbours.Where(IsEmpty);
        }

        public void PutStone(Position position, BoardState color)
        {
            SetState(position, color);
            OnBoardChanged();
        }

        public bool IsOccupied(Position position)
        {
            return !IsEmpty(position);
        }

        public bool IsEmpty(Position position)
        {
            return GetState(position) == BoardState.Empty;
        }

        public bool AllEmpty {
            get { return Positions.All(IsEmpty); }
        }

        private IEnumerable<List<Position>> GetRows()
        {
            for (byte y = 1; y <= Size; y++)
            {
                yield return GetRow(y);
            }
        }

        private List<Position> GetRow(byte y)
        {
            return Enumerable.Range(1, Size).
                Select(x => new Position((byte)x, y)).
                ToList();
        }

        private void InitializeBoard()
        {
            for (int i = 0; i <= Size + 1; i++)
            {
                _grid[0, i] = BoardState.Outside;
                _grid[Size + 1, i] = BoardState.Outside;
                _grid[i, 0] = BoardState.Outside;
                _grid[i, Size + 1] = BoardState.Outside;
            }
            Positions.ForEach(pos => SetState(pos, BoardState.Empty));
        }

        private void SetState(Position position, BoardState state)
        {
            _grid[position.X, position.Y] = state;
        }

        private IEnumerable<Position> GetPositions()
        {
            for (byte x = 1; x <= Size; x++)
            {
                for (byte y = 1; y <= Size; y++)
                {
                    yield return new Position(x, y);
                }
            }
        }

        private void OnBoardChanged()
        {
            if (BoardChanged != null) BoardChanged(this, EventArgs.Empty);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, Rows.Select(RowToString));
        }

        private string RowToString(List<Position> positions)
        {
            return new string(positions.Select(GetState).Select(StateToChar).ToArray());
        }

        private char StateToChar(BoardState state)
        {
            switch (state)
            {
                    case BoardState.Black:
                    return 'B';
                    case BoardState.White:
                    return 'W';
                default:
                    return '.';
            }
        }
    }
}
