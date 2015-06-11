using System.Linq;
using Go.Engine;
using Go.Models;

namespace Go.Gaming
{
    public class ComputerPlayer : Player
    {
        private readonly ISmartBoard _smartBoard;

        public ComputerPlayer(ISmartBoard smartBoard, BoardState color)
            : base(color)
        {
            _smartBoard = smartBoard;
        }

        protected override Position GetPosition(bool isRepeatedRequest)
        {
            return isRepeatedRequest ? Position.Pass : GetFirstAvailableSpace();
        }

        private Position GetFirstAvailableSpace()
        {
            return _smartBoard.BoardView.Positions.FirstOrDefault(pos => _smartBoard.IsValidPlacement(new Move(Color, pos))) 
                ?? Position.Pass;
        }
    }
}
