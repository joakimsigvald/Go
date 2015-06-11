using System;
using System.Collections.Generic;
using System.Linq;
using Go.Engine;
using Go.Gaming;
using Go.Models;

namespace Go.ConsoleApp
{
    public class ConsoleCommunicator : Communicator
    {
        public ConsoleCommunicator(IBoardView board)
            : base(board)
        {
        }

        public override Position RequestPosition()
        {
            Console.Write("> ");
            var input = Console.ReadLine();
            Position position;
            while (!TryParse(input, out position))
            {
                Console.Write("Select move: [X, Y]> ");
                input = Console.ReadLine();
            }
            return position;
        }

        public override void ShowCurrentBoard()
        {
            Console.WriteLine();
            Console.WriteLine(new String('-', Board.Size * 2));
            Console.WriteLine();
            Board.Rows.ToList().ForEach(PrintRow);
        }

        public override void ShowGameFinished(GameResult result)
        {
            Console.WriteLine("Game over");
            Console.WriteLine("Winner is: " + result.Winner);
            Console.WriteLine("Black score: {0}, white score: {1}", result.BlackScore, result.WhiteScore);
            Console.Read();
        }

        public override void Say(string message)
        {
            Console.WriteLine(message);
        }

        private bool TryParse(string input, out Position position)
        {
            position = Position.Pass;
            if (string.IsNullOrEmpty(input)) return true;
            position = Position.Resign;
            if (string.IsNullOrWhiteSpace(input)) return true;
            var parts = input.Split(',');
            if (parts.Length != 2) return false;
            byte x;
            byte y;
            if (Byte.TryParse(parts[0].Trim(), out x) && Byte.TryParse(parts[1].Trim(), out y))
            {
                position = new Position(x, y);
                return true;
            }
            return false;
        }

        private void PrintRow(List<Position> row)
        {
            row.ForEach(PrintIntersection);
            Console.WriteLine();
        }

        private void PrintIntersection(Position pos)
        {
            var state = Board.GetState(pos);
            Console.Write(GetIntersectionSymbol(state));
        }

        private string GetIntersectionSymbol(BoardState state)
        {
            switch (state)
            {
                case BoardState.Empty: return ".";
                case BoardState.White: return "@";
                case BoardState.Black: return "O";
                default: throw new ArgumentException("Unknown state: " + state);
            }
        }
    }
}
