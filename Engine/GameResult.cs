using System.Collections.Generic;
using System.Linq;
using Go.Models;

namespace Go.Engine
{
    public struct GameResult
    {
        public readonly IDictionary<BoardState, int> Score;

        public GameResult(int blackScore, int whiteScore)
        {
            Score = new Dictionary<BoardState, int>
            {
                {BoardState.Black, blackScore}
                , {BoardState.White, whiteScore}        
            };
        }

        public int BlackScore
        {
            get { return Score[BoardState.Black]; }
            //set { Score[BoardState.Black] = value; }
        }

        public int WhiteScore
        {
            get { return Score[BoardState.White]; }
            //set { Score[BoardState.White] = value; }
        }

        public BoardState Winner
        {
            get
            {
                return Score.
                    OrderByDescending(x => x.Value).
                    Select(x => x.Key).
                    First();
            }
        }
    }
}