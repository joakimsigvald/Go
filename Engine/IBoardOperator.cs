using System.Collections.Generic;
using Go.Models;

namespace Go.Engine
{
    public interface IBoardOperator
    {
        void PutStone(Position position, BoardState color);
        void Remove(List<Position> capturedStones);
        IBoard Copy();
    }
}