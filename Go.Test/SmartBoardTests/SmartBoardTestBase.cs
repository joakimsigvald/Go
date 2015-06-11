using Go.Models;

namespace Go.Engine.Test.SmartBoardTests
{
    public abstract class SmartBoardTestBase
    {
        protected SmartBoard SmartBoard;

        protected void Given(string boardSetUp, params Move[] moves)
        {
            SmartBoard = SmartBoardGenerator.SetUpBoard(boardSetUp, moves);
        }

        protected void When(params Move[] moves)
        {
            foreach (var move in moves)
            {
                SmartBoard.MakeMove(move);
            }
        }
    }
}
