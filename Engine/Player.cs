using System;
using Go.Models;

namespace Go.Engine
{
    public abstract class Player : IPlayer
    {
        private BoardState _color;

        public BoardState Color
        {
            get { return _color; }
            set
            {
                if (_color != default(BoardState)) throw new InvalidOperationException("Cannot change color of player");
                _color = value;
                if (!_color.IsStoneColor()) throw new InvalidOperationException("Color must be set to a stone color");
            }
        }

        public Move GetMove(bool isRepeatedRequest = false)
        {
            Position position = GetPosition(isRepeatedRequest);
            return new Move(Color, position);
        }

        protected abstract Position GetPosition(bool isRepeatedRequest);
    }
}
