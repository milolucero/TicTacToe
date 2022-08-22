using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeLibrary;

namespace TicTacToeConsoleUI
{
    public class Program
    {
        static void Main(string[] args)
        {
            PrintProgramInformation(
                programName: "Tic-Tac-Toe",
                programVersion: "2.0.0",
                authorName: "Camilo Lucero",
                authorEmail: "cluceroespitia@rrc.ca");

            bool playAgain = false;

            do
            {
                Shape userShapeChoice = PromptUserShapeChoice();

                DifficultyLevel difficultyLevel = PromptLevelOfDifficulty();

                Game game = new Game(userShapeChoice, difficultyLevel);

                Console.WriteLine($"\nUser shape: {userShapeChoice}\nDifficulty level: {difficultyLevel}");

                // TODO: Display the game

                playAgain = PromptPlayAgain();

                Console.ReadLine();
            } while (playAgain);

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
        
        /// <summary>
        /// Prompts the user to pick a side (either "X" or "O"). Keeps prompting until the input is valid. Returns the Shape chosen. 
        /// </summary>
        /// <returns>The Shape chosen by the user.</returns>
        public static Shape PromptUserShapeChoice()
        {
            Shape userShapeChoice = Shape.None;
            while (userShapeChoice == Shape.None)
            {
                Console.Write("\nChoose a side. Enter \"X\" or \"O\": ");
                string userInput = Console.ReadLine().ToUpper();

                // Check if the given user input matches a Shape.
                if (Enum.IsDefined(typeof(Shape), userInput))
                {
                    // Convert the userInput string into its equivalent Shape.
                    userShapeChoice = (Shape)Enum.Parse(typeof(Shape), userInput);
                }
                else
                {
                    Console.WriteLine("Invalid side. Please try again.\n");
                }
            }

            return userShapeChoice;
        }

        /// <summary>
        /// Prompts the user to pick a level of difficulty. Keeps prompting until the input is valid. Returns the DifficultyLevel chosen.
        /// </summary>
        /// <returns>The DifficultyLevel chosen.</returns>
        private static DifficultyLevel PromptLevelOfDifficulty()
        {
            string userInput;
            int userNumber;
            bool userInputIsInt;
            bool userInputIsValidDifficultyLevel = false;

            do
            {
                // Prompt for user input
                Console.Write("\nChoose a number to pick the level of difficulty:\n" +
                    "1 - Easy\n" +
                    "2 - Medium\n" +
                    "3 - Hard\n\n");

                userInput = Console.ReadLine();

                // Validate input
                userInputIsInt = int.TryParse(userInput, out userNumber);

                if (userInputIsInt)
                {
                    userInputIsValidDifficultyLevel = Enum.IsDefined(typeof(DifficultyLevel), userNumber);

                    if (!userInputIsValidDifficultyLevel)
                    {
                        Console.WriteLine($"\"{userInput}\" is not a valid level of difficulty. Please write a number from 1 to 3.");
                    }
                }
                else
                {
                    Console.WriteLine($"\"{userInput}\" is not a number. Please write a number from 1 to 3.");
                }

            } while(!userInputIsValidDifficultyLevel);

            return (DifficultyLevel)userNumber;
        }

        /// <summary>
        /// Prompts the user to play again. Keeps prompting until the input is valid.
        /// </summary>
        /// <returns>True if the user wants to play again; otherwise, false.</returns>
        private static bool PromptPlayAgain()
        {
            throw new NotImplementedException();
        }
    }
}
