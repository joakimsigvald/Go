using Go.Models;

namespace Go.Engine
{
    public abstract class Player : IPlayer
    {
        protected Player(BoardState color)
        {
            Color = color;
        }

        public BoardState Color { get; private set; }

        public Move GetMove(bool isRepeatedRequest = false)
        {
            Position position = GetPosition(isRepeatedRequest);
            return new Move(Color, position);
        }

        protected abstract Position GetPosition(bool isRepeatedRequest);
    }
}
