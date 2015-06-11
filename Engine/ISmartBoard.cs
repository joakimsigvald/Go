using System.Collections.Generic;
using Go.Models;

namespace Go.Engine
{
    public interface ISmartBoard
    {
        IBoardView BoardView { get; }
        bool IsValid(Move move);
        void MakeMove(Move move);
        bool IsValidPlacement(Move move);
        int GetPrisoners(BoardState color);
        ISmartBoard Copy();
        void Remove(List<Position> stones);
    }
}
