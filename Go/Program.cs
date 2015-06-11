using System;
using Go.Engine;
using Go.Gaming;
using Go.Robots;

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
            Player computer = new SimplePlayer(smartBoard);
            Player human = new HumanPlayer(communicator);
            Game game = new Game(smartBoard, communicator, computer, human);
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
