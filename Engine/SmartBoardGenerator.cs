using System;
using System.Collections.Generic;
using System.Linq;
using Go.Models;
using Go.Utilities;

namespace Go.Engine
{
    public static class SmartBoardGenerator
    {
        public static SmartBoard SetUpBoard(string boardSetUp, params Move[] additionalMoves)
        {
            List<Move> moves = GetMoves(boardSetUp).ToList();
            var board = new Board(moves.Count);
            SmartBoard smartBoard = new SmartBoard(board);
            moves.Concat(additionalMoves).ForEach(smartBoard.MakeMove);
            return smartBoard;
        }

        private static IEnumerable<Move> GetMoves(string boardSetUp)
        {
            var rows = boardSetUp.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            return GetMoves(rows);
        }

        private static IEnumerable<Move> GetMoves(string[] rows)
        {
            Dictionary<int, Move> moves = new Dictionary<int, Move>();
            CreateMoves(rows, moves);
            return moves.OrderBy(p => p.Key).Select(p => p.Value);
        }

        private static void CreateMoves(string[] rows, Dictionary<int, Move> moves)
        {
            for (byte y = 1; y <= rows.Length; y++)
            {
                CreateMoves(moves, rows[y - 1].Trim(), y);
            }
        }

        private static void CreateMoves(Dictionary<int, Move> moves, string row, byte y)
        {
            for (byte x = 1; x <= row.Length; x++)
            {
                char symbol = row[x - 1];
                var state = GetBoardState(symbol);
                if (state.IsStoneColor())
                {
                    var move = new Move(state, new Position(x, y));
                    var order = GetMoveOrder(symbol);
                    moves[order] = move;
                }
            }
        }

        private static BoardState GetBoardState(char c)
        {
            if (char.IsLetter(c)) return BoardState.White;
            if (char.IsDigit(c)) return BoardState.Black;
            if (c == '.') return BoardState.Empty;
            throw new Exception("Unknown symbol: " + c);
        }

        private static int GetMoveOrder(char c)
        {
            if (char.IsLetter(c)) return 2*(char.ToUpper(c) - 'A') + 1;
            if (char.IsDigit(c)) return 2 * (c - '0');
            throw new Exception("Unknown symbol: " + c);            
        }
    }
}