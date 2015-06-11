using System.Collections.Generic;
using System.Linq;
using Go.Models;

namespace Go.Engine
{
    public class EndPlayer : Player
    {
        private readonly ISmartBoard _smartBoard;

        public EndPlayer(BoardState color, ISmartBoard smartBoard)
            : base(color)
        {
            _smartBoard = smartBoard;
        }

        protected override Position GetPosition(bool isRepeatedRequest)
        {
            return GetCandidates().FirstOrDefault() ?? Position.Pass;
        }

        private IEnumerable<Position> GetCandidates()
        {
            var emptyPoints = _smartBoard.BoardView.EmptyPoints.ToList();
            return emptyPoints.Where(p => OnlySurroundedBy(p, 0))
                .Concat(emptyPoints.Where(p => OnlySurroundedBy(p, 1)))
                .Concat(emptyPoints.Where(p => OnlySurroundedBy(p, 2)))
                .Concat(emptyPoints.Where(p => OnlySurroundedBy(p, 3)))
                .Concat(emptyPoints.Where(IsDame));
        }

        private bool IsDame(Position p)
        {
            return _smartBoard.IsValidPlacement(new Move(Color, p)) &&
                p.Neighbours.Any(np => _smartBoard.BoardView.GetState(np) == Color.GetOpposite())
                   && p.Neighbours.Any(np => _smartBoard.BoardView.GetState(np) == Color);
        }

        private bool OnlySurroundedBy(Position p, int n)
        {
            return _smartBoard.BoardView.GetLiberties(p).Count() == 4 - n
                   && p.Neighbours.All(np => _smartBoard.BoardView.GetState(np) != Color.GetOpposite())
                   && p.Neighbours.Any(np => _smartBoard.BoardView.GetState(np) == Color);
        }
    }
}