using System;
using Go.Engine;
using Go.Gaming;
using Go.Models;

namespace Go.ConsoleApp
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var size = GetSize(args);
            Board board = new Board(size);
            ISmartBoard smartBoard = new SmartBoard(board);
            ICommunicator communicator = new ConsoleCommunicator(board);
            Player blackPlayer = new HumanPlayer(communicator, BoardState.Black);
            Player whitePlayer = new ComputerPlayer(smartBoard, BoardState.White);
            Game game = new Game(smartBoard, communicator, blackPlayer, whitePlayer);
            game.Play();
            GameResult result = game.ComputeResult();
            communicator.ShowGameFinished(result);
        }

        private static byte GetSize(string[] args)
        {
            if (args.Length != 1) throw new ArgumentException("Provide board size as an argument");
            byte size;
            if (!Byte.TryParse(args[0], out size)) throw new ArgumentException("Provide board size as an integer");
            return size;
        }
    }
}
