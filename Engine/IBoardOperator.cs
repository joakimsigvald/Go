using System.Collections.Generic;
using Go.Models;

namespace Go.Engine
{
    public interface IBoardOperator
    {
        bool IsInside(Position position);
        void PutStone(Position position, BoardState color);
        void Remove(List<Position> capturedStones);
        IBoard Copy();
    }
}