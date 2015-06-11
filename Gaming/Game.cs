using Go.Engine;
using Go.Models;

namespace Go.Gaming
{
    public class Game
    {
        private readonly ISmartBoard _smartBoard;
        private readonly ICommunicator _communicator;
        private readonly IPlayer _blackPlayer;
        private readonly IPlayer _whitePlayer;
        private IPlayer _nextPlayer;
        private Move _lastMove = Move.NoMove;

        public Game(ISmartBoard smartBoard, ICommunicator communicator, IPlayer blackPlayer, IPlayer whitePlayer)
        {
            blackPlayer.Color = BoardState.Black;
            whitePlayer.Color = BoardState.White;
            _smartBoard = smartBoard;
            _communicator = communicator;
            _blackPlayer = blackPlayer;
            _whitePlayer = whitePlayer;
        }

        public void Play()
        {
            _communicator.ShowCurrentBoard();
            _nextPlayer = _blackPlayer;
            var gameIsEnding = false;
            while (!gameIsEnding)
            {
                gameIsEnding = MakeTurn();
            }
        }

        public GameResult ComputeResult()
        {
            GameResult result = new TerritoryCounter().Count(_smartBoard);
            foreach (var color in new[] { BoardState.Black, BoardState.White })
                result.Score[color] += _smartBoard.GetPrisoners(color);
            if (_lastMove.IsResign)
            {
                result.Score[_lastMove.Color] = -1;
            }
            return result;
        }

        private bool MakeTurn()
        {
            Move move = Move.NoMove;
            try
            {
                move = GetNextMove();
                _smartBoard.MakeMove(move);
                return move.IsResign || IsSecondPass(move);
            }
            finally
            {
                _lastMove = move;
                _nextPlayer = GetOppositePlayer(_nextPlayer);
            }
        }

        private Move GetNextMove()
        {
            Move move = _nextPlayer.GetMove();
            while (!_smartBoard.IsValid(move))
            {
                move = _nextPlayer.GetMove(true);
            }
            return move;
        }

        private bool IsSecondPass(Move move)
        {
            return move.IsPass && _lastMove.IsPass;
        }

        private IPlayer GetOppositePlayer(IPlayer player)
        {
            return player == _whitePlayer ? _blackPlayer : _whitePlayer;
        }
    }
}