using Go.Models;

namespace Go.Engine
{
    public interface IPlayer
    {
        BoardState Color { get; }
        Move GetMove(bool isRepeatedRequest = false);
    }
}
