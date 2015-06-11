using System;

namespace Go.Models
{
    public class Move : IEquatable<Move>
    {
        public readonly static Move NoMove = new Move(BoardState.Empty, Position.Pass);

        public readonly BoardState Color;
        public readonly Position Position;

        public static Move Black(Position position)
        {
            return new Move(BoardState.Black, position);
        }

        public static Move White(Position position)
        {
            return new Move(BoardState.White, position);
        }

        public Move(BoardState color, Position position)
        {
            Color = color;
            Position = position;
        }

        public bool IsPass
        {
            get { return !Equals(NoMove) && Equals(Position, Position.Pass); }
        }

        public bool IsResign
        {
            get { return !Equals(NoMove) && Equals(Position, Position.Resign); }
        }

        public bool IsPlacement 
        {
            get { return !Equals(Position, Position.Pass) && !Equals(Position, Position.Resign); }
        }

        public bool Equals(Move other)
        {
            return other != null && Equals(Position, other.Position) && Color == other.Color;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Move);
        }

        public override int GetHashCode()
        {
            return Position.GetHashCode() << 4 + Color.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("[{0}: {1}]", Color, Position);
        }
    }
}
