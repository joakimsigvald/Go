using System;
using System.Collections.Generic;
using Go.Models;

namespace Go.Engine
{
    public interface IBoardView
    {
        event EventHandler BoardChanged;

        int Size { get; }
        List<List<Position>> Rows { get; }
        IEnumerable<Position> EmptyPoints { get; }
        List<Position> Positions { get; }
        BoardState GetState(Position position);
        IEnumerable<Position> GetLiberties(Position position);
        bool IsEmpty(Position position);
        bool IsOccupied(Position position);
        bool AllEmpty { get; }
    }
}