﻿using System.Collections.Generic;
using System.Linq;
using Go.Engine;
using Go.Models;

namespace Go.Robots
{
    public class EndPlayer : Player
    {
        private readonly ISmartBoard _smartBoard;

        public EndPlayer(ISmartBoard smartBoard)
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
            return emptyPoints.Where(p => OnlyFriendlyAttachments(p, 1))
                .Concat(emptyPoints.Where(p => OnlyFriendlyAttachments(p, 2)))
                .Concat(emptyPoints.Where(p => OnlyFriendlyAttachments(p, 3)))
                .Concat(emptyPoints.Where(IsDame));
        }

        private bool IsDame(Position p)
        {
            return _smartBoard.IsValidPlacement(new Move(Color, p)) &&
                p.Neighbours.Any(np => _smartBoard.BoardView.GetState(np) == Color.GetOpposite())
                   && p.Neighbours.Any(np => _smartBoard.BoardView.GetState(np) == Color);
        }

        private bool OnlyFriendlyAttachments(Position p, int n)
        {
            return _smartBoard.BoardView.GetLiberties(p).Count() == 4 - n
                   && p.Neighbours.All(np => _smartBoard.BoardView.GetState(np) != Color.GetOpposite())
                   && p.Neighbours.Any(np => _smartBoard.BoardView.GetState(np) == Color);
        }
    }
}