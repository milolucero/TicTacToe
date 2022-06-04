using System;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("TicTacToeTest")]

namespace TicTacToe
{
    /// <summary>
    /// A simple Tic-tac-toe console game using object oriented programming principles.
    /// </summary>
    class Program
    {        
        static void Main(string[] args)
        {
            PrintProgramInformation("Tic-Tac-Toe", "1.0.0", "Camilo Lucero", "cluceroespitia@rrc.ca");

            Game game = new Game();
            game.NewGame();

            // Test.RunTests();
        }

        /// <summary>
        /// Displays a message with information of the program and author.
        /// </summary>
        /// <param name="programName">The name of the program.</param>
        /// <param name="programVersion">The version of the program.</param>
        /// <param name="authorName">The name of the author.</param>
        /// <param name="authorEmail">The email of the author.</param>
        public static void PrintProgramInformation(string programName, string programVersion, string authorName, string authorEmail)
        {
            string message = "";
            message += $"{programName} v{programVersion}\n";
            message += $"Author: {authorName}\n";
            message += $"Email: {authorEmail}\n";

            Console.WriteLine(message);
        }
    }
}