using System.Collections.Generic;
using System.Linq;
using Go.Models;

namespace Go.Engine
{
    //TODO: Move to gaming, together with player
    public class TerritoryCounter
    {
        private ISmartBoard _smartBoard;
        private IBoardView _originalBoard;

        public GameResult Count(ISmartBoard smartBoard)
        {
            _originalBoard = smartBoard.BoardView;
            _smartBoard = smartBoard.Copy();
            GameResult result = FillIngPoints();
            result.Score[BoardState.Black] += CountEmpty(BoardState.Black);
            result.Score[BoardState.White] += CountEmpty(BoardState.White);
            AddTerritoryFillIns(result);
            AddDeadStones(result);
            return result;
        }

        private GameResult FillIngPoints()
        {
            GameResult result = new GameResult(0, 0);
            EndPlayer blackScorePlayer = new EndPlayer(BoardState.Black, _smartBoard);
            EndPlayer whiteScorePlayer = new EndPlayer(BoardState.White, _smartBoard);
            bool isPass = false, wasPass = false;
            IPlayer nextPlayer = blackScorePlayer;
            while (!isPass || !wasPass)
            {
                Move move = nextPlayer.GetMove();
                wasPass = isPass;
                isPass = move.IsPass;
                if (!isPass)
                {
                    _smartBoard.MakeMove(move);
                }
                nextPlayer = (nextPlayer == blackScorePlayer ? whiteScorePlayer : blackScorePlayer);
            }
            return result;
        }

        private void AddTerritoryFillIns(GameResult result)
        {
            Stack<Position> unchartedTerritory = new Stack<Position>(_smartBoard.BoardView.EmptyPoints);
            while (unchartedTerritory.Any())
            {
                var empty = unchartedTerritory.Pop();
                var neighbours = empty.Neighbours.ToList();
                RemoveAndCountFillIns(result, neighbours, BoardState.Black).ForEach(unchartedTerritory.Push);
                RemoveAndCountFillIns(result, neighbours, BoardState.White).ForEach(unchartedTerritory.Push);
            }
        }

        private List<Position> RemoveAndCountFillIns(GameResult result, IEnumerable<Position> points, BoardState color)
        {
            var fillIns = points.Where(p => _originalBoard.GetState(p) != color && _smartBoard.BoardView.GetState(p) == color).ToList();
            result.Score[color] += fillIns.Count;
            _smartBoard.Remove(fillIns);
            return fillIns;
        }

        private void AddDeadStones(GameResult result)
        {
            CountDeadStones(result, BoardState.Black);
        }

        private void CountDeadStones(GameResult result, BoardState color)
        {
            result.Score[color.GetOpposite()] +=
                _originalBoard.Positions.Count(
                    p => _originalBoard.GetState(p) == color && _smartBoard.BoardView.GetState(p) != color);
        }

        private int CountEmpty(BoardState color)
        {
            return _smartBoard.BoardView.EmptyPoints.Count(p => IsOwner(p, color));
        }

        private bool IsOwner(Position position, BoardState color)
        {
            return position.Neighbours.Any(np => _smartBoard.BoardView.GetState(np) == color) &&
                position.Neighbours.All(np => _smartBoard.BoardView.GetState(np) != color.GetOpposite());
        }
    }
}