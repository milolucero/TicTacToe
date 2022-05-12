using System;

namespace TicTacToe
{
    /// <summary>
    /// A simple Tic-tac-toe console game using object oriented programming principles.
    /// </summary>
    class Program
    {        
        static void Main(string[] args)
        {
            PrintWelcomeMessage();

            Game game = new Game();
            game.NewGame();

            // Test.RunTests();
        }

        public static void PrintWelcomeMessage()
        {
            string programName = "Tic-tac-toe";
            string programVersion = "1.0";
            string authorName = "Camilo Lucero";
            string authorEmail = "cluceroespitia@rrc.ca";

            string message = "";
            message += $"{programName} v{programVersion}\n";
            message += $"Author: {authorName}\n";
            message += $"Email: {authorEmail}\n";

            Console.WriteLine(message);
        }
    }
}