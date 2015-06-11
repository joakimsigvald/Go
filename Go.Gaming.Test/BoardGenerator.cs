using System;
using System.Collections.Generic;
using System.Linq;
using Go.Engine;
using Go.Models;

namespace Go.Gaming.Test
{
    public class BoardGenerator
    {
        public Board CreateBoard(string boardSetUp)
        {
            var boardStates = GetBoardStates(boardSetUp).ToArray();
            var board = new Board(boardStates.Length);
            for (byte x = 1; x <= board.Size; x++)
            {
                for (byte y = 1; y <= board.Size; y++)
                {
                    board.PutStone(new Position(x, y), boardStates[x - 1][y - 1]);
                }
            }
            return board;
        }

        private IEnumerable<BoardState[]> GetBoardStates(string boardSetUp)
        {
            var rows = boardSetUp.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            for (byte i = 0; i < rows.Length; i++)
            {
                yield return GetRow(rows[i].Trim()).ToArray();
            }
        }

        private IEnumerable<BoardState> GetRow(string row)
        {
            for (byte i = 0; i < row.Length; i++)
            {
                yield return GetBoardState(row[i]);
            }
        }

        private BoardState GetBoardState(char c)
        {
            switch (c)
            {
                case '.': return BoardState.Empty;
                case 'W': return BoardState.White;
                case 'B': return BoardState.Black;
                default: throw new Exception("Unknown symbol: " + c);
            }
        }
    }
}