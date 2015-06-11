using Go.Engine;
using Go.Models;

namespace Go.Gaming
{
    public interface ICommunicator
    {
        Position RequestPosition();
        void ShowCurrentBoard();
        void ShowGameFinished(GameResult result);
        void Say(string message);
    }
}
