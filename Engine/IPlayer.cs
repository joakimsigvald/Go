using Go.Models;

namespace Go.Engine
{
    public interface IPlayer
    {
        BoardState Color { get; set; }
        Move GetMove(bool isRepeatedRequest = false);
    }
}
