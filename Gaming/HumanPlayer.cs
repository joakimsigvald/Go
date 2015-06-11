using Go.Engine;
using Go.Models;

namespace Go.Gaming
{
    public class HumanPlayer : Player
    {
        private readonly ICommunicator _communicator;

        public HumanPlayer(ICommunicator communicator, BoardState color)
            : base(color)
        {
            _communicator = communicator;
        }

        protected override Position GetPosition(bool isRepeatedRequest)
        {
            if (isRepeatedRequest)
            {
                _communicator.Say("Choose a valid move");
            }
            return _communicator.RequestPosition();
        }
    }
}
