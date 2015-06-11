using Go.Engine;
using Go.Models;

namespace Go.Gaming
{
    public abstract class Communicator : ICommunicator
    {
        protected readonly IBoardView Board;

        protected Communicator(IBoardView board)
        {
            Board = board;
            Board.BoardChanged += Board_BoardChanged;
        }

        void Board_BoardChanged(object sender, System.EventArgs e)
        {
            ShowCurrentBoard();
        }

        public abstract Position RequestPosition();
        public abstract void ShowCurrentBoard();
        public abstract void ShowGameFinished(GameResult result);
        public abstract void Say(string message);
    }
}
