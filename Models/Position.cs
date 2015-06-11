using System;
using System.Collections.Generic;
using System.Linq;

namespace Go.Models
{
    public class Position : IEquatable<Position>
    {
        public static readonly Position Pass = new Position(byte.MinValue, byte.MinValue);
        public static readonly Position Resign = new Position(byte.MaxValue, byte.MaxValue);

        public readonly byte X;
        public readonly byte Y;

        private List<Position> _neighbours;

        public Position(byte x, byte y)
        {
            X = x;
            Y = y;
        }

        public IEnumerable<Position> Neighbours
        {
            get
            {
                return _neighbours ?? (_neighbours = GetNeighbours().ToList());
            }
        }

        public bool Equals(Position other)
        {
            return other != null && other.X == X && other.Y == Y;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Position);
        }

        public override int GetHashCode()
        {
            return X + Y << 8;
        }

        public override string ToString()
        {
            return string.Format("[{0}, {1}]", X, Y);
        }

        private IEnumerable<Position> GetNeighbours()
        {
            yield return new Position((byte)(X - 1), Y);
            yield return new Position((byte)(X + 1), Y);
            yield return new Position(X, (byte)(Y - 1));
            yield return new Position(X, (byte)(Y + 1));
        }
    }
}
