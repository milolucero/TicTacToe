﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    /// <summary>
    /// Represents the tic-tac-toe board.
    /// </summary>
    internal class Board
    {
        private readonly int height;
        private readonly int width;
        private Space[] spaces;
        private List<Space> emptySpaces;
        private GameResult result;


        /// <summary>
        /// Initializes a new instance of the Board class, specifying the spaces that fill the board.
        /// </summary>
        /// <param name="spaces">The spaces that populate the board.</param>
        public Board(Space[] spaces)
        {
            height = Rule.GetBoardDimensions().height;
            width = Rule.GetBoardDimensions().width;
            this.spaces = spaces;
            SetEmptySpaces(GetEmptySpaces());
            SetResult(GameResult.Incomplete);
        }

        /// <summary>
        /// Initializes a new instance of the Board class without arguments, filling the board with empty (unassigned) spaces.
        /// </summary>
        public Board() : this(GetArrayOfEmptySpacesForBoard(Rule.GetBoardDimensions().height, Rule.GetBoardDimensions().width))
        {

        }

        /// <summary>
        /// Returns an array containing the board spaces.
        /// </summary>
        /// <returns>An array containing the board spaces.</returns>
        public Space[] GetSpaces()
        {
            return spaces;
        }

        /// <summary>
        /// Sets the spaces of the board to the specified array of spaces.
        /// </summary>
        /// <param name="spaces"></param>
        public void SetSpaces(Space[] spaces)
        {
            this.spaces = spaces;
        }

        /// <summary>
        /// Returns an array of empty spaces for the specified board height and width.
        /// </summary>
        /// <param name="boardHeight">The height of the board.</param>
        /// <param name="boardWidth">The width of the board.</param>
        /// <returns>An array of empty spaces for the specified board height and width.</returns>
        public static Space[] GetArrayOfEmptySpacesForBoard(int boardHeight, int boardWidth)
        {
            Space[] spaces = new Space[boardHeight * boardWidth];

            // Populate spaces array with new spaces at the specified coordinates
            int count = 0;
            for (int i = 0; i < boardHeight; i++)
            {
                for (int j = 0; j < boardWidth; j++)
                {
                    Position position = new Position(i, j);
                    spaces[count] = new Space(position);

                    count++;
                }
            }

            return spaces;
        }

        /// <summary>
        /// Returns a list containing the currently empty spaces of the board.
        /// </summary>
        /// <returns>A list containing the currently empty spaces of the board.</returns>
        public List<Space> GetEmptySpaces()
        {
            List<Space> emptySpaces = new List<Space>();

            foreach (Space space in spaces)
            {
                if (!space.IsOccupied())
                {
                    emptySpaces.Add(space);
                }
            }

            return emptySpaces;
        }

        /// <summary>
        /// Sets the empty spaces of the board to the specified list of empty spaces.
        /// </summary>
        /// <param name="emptySpaces">The list of empty spaces.</param>
        public void SetEmptySpaces(List<Space> emptySpaces)
        {
            this.emptySpaces = emptySpaces;
        }

        /// <summary>
        /// Occupies a specific space of a board by the shape that has the turn. If a player is specified, the player's occupied spaces are updated. Returns true if the space was taken succesfully, false if it was already occupied.
        /// </summary>
        /// <param name="board">The board.</param>
        /// <param name="space">The space to occupy.</param>
        /// <param name="player">The player whose state should be updated, or null.</param>
        /// <returns>True if the space was taken succesfully, false if it was already occupied.</returns>
        public static bool OccupySpace(Board board, Space space, Player? player)
        {
            Shape shapeToPlay = Board.GetShapeOfTurnFromBoard(board);
            bool spaceOccupiedSuccessfully = false;

            if (!space.IsOccupied())
            {
                space.SetOccupant(shapeToPlay);

                // If given a player as an argument, add the given space to the spaces that belong to this player.
                if (player is not null)
                {
                    player.AddToOccupiedSpaces(space);
                }

                List<Space> emptySpaces = board.GetEmptySpaces();
                emptySpaces.Remove(space);
                board.SetEmptySpaces(emptySpaces);

                spaceOccupiedSuccessfully = true;
            }
            else
            {
                Console.WriteLine("The space is already taken. Choose another one.");
            }

            return spaceOccupiedSuccessfully;
        }

        /// <summary>
        /// Occupies a specific space of a board by the shape that has the turn. If a player is specified, the player's occupied spaces are updated. Returns true if the space was taken succesfully, false if it was already occupied.
        /// </summary>
        /// <param name="board">The board.</param>
        /// <param name="space">The space to occupy.</param>
        /// <returns>True if the space was taken succesfully, false if it was already occupied.</returns>
        public static bool OccupySpace(Board board, Space space)
        {
            return OccupySpace(board, space, null);
        }

        /// <summary>
        /// Checks if there is a winner. Returns a tuple where the first value is true if a winner was found; otherwise, false. The second value is the winning player if there was one found; otherwise, null.
        /// </summary>
        /// <returns>A tuple where the first value is true if a winner was found; otherwise, false. The second value is the winning player if there was one found; otherwise, null.</returns>
        public (bool hasWinner, Shape? winnerShape) CheckWin()
        {
            bool hasWinner = false;
            Shape? winnerShape = null;

            // Idea: We only need to check the surroundings of the last placed shape.

            // Ways of winning for space[0]
            int[] wayOfWinning1 = { 0, 1, 2 };
            int[] wayOfWinning2 = { 0, 3, 6 };
            int[] wayOfWinning3 = { 0, 4, 8 };

            // Ways of winning for space[1]
            int[] wayOfWinning4 = { 1, 4, 7 };

            // Ways of winning for space[2]
            int[] wayOfWinning5 = { 2, 5, 8 };
            int[] wayOfWinning6 = { 2, 4, 6 };

            // Ways of winning for space[3]
            int[] wayOfWinning7 = { 3, 4, 5 };

            // Ways of winning for space[6]
            int[] wayOfWinning8 = { 6, 7, 8 };


            int[][] waysOfWinning = { wayOfWinning1, wayOfWinning2, wayOfWinning3, wayOfWinning4, wayOfWinning5, wayOfWinning6, wayOfWinning7, wayOfWinning8 };

            if (spaces[0].IsOccupied())
            {
                if ((spaces[wayOfWinning1[0]].GetOccupant() == spaces[wayOfWinning1[1]].GetOccupant() && spaces[wayOfWinning1[1]].GetOccupant() == spaces[wayOfWinning1[2]].GetOccupant()) ||
                    (spaces[wayOfWinning2[0]].GetOccupant() == spaces[wayOfWinning2[1]].GetOccupant() && spaces[wayOfWinning2[1]].GetOccupant() == spaces[wayOfWinning2[2]].GetOccupant()) ||
                    (spaces[wayOfWinning3[0]].GetOccupant() == spaces[wayOfWinning3[1]].GetOccupant() && spaces[wayOfWinning3[1]].GetOccupant() == spaces[wayOfWinning3[2]].GetOccupant()))
                {
                    hasWinner = true;
                    winnerShape = spaces[0].GetOccupant();
                }
            }

            if (spaces[1].IsOccupied())
            {
                if (spaces[wayOfWinning4[0]].GetOccupant() == spaces[wayOfWinning4[1]].GetOccupant() && spaces[wayOfWinning4[1]].GetOccupant() == spaces[wayOfWinning4[2]].GetOccupant())
                {
                    hasWinner = true;
                    winnerShape = spaces[1].GetOccupant();
                }
            }

            if (spaces[2].IsOccupied())
            {
                if ((spaces[wayOfWinning5[0]].GetOccupant() == spaces[wayOfWinning5[1]].GetOccupant() && spaces[wayOfWinning5[1]].GetOccupant() == spaces[wayOfWinning5[2]].GetOccupant()) ||
                    (spaces[wayOfWinning6[0]].GetOccupant() == spaces[wayOfWinning6[1]].GetOccupant() && spaces[wayOfWinning6[1]].GetOccupant() == spaces[wayOfWinning6[2]].GetOccupant()))
                {
                    hasWinner = true;
                    winnerShape = spaces[2].GetOccupant();
                }
            }

            if (spaces[3].IsOccupied())
            {
                if (spaces[wayOfWinning7[0]].GetOccupant() == spaces[wayOfWinning7[1]].GetOccupant() && spaces[wayOfWinning7[1]].GetOccupant() == spaces[wayOfWinning7[2]].GetOccupant())
                {
                    hasWinner = true;
                    winnerShape = spaces[3].GetOccupant();
                }
            }

            if (spaces[6].IsOccupied())
            {
                if (spaces[wayOfWinning8[0]].GetOccupant() == spaces[wayOfWinning8[1]].GetOccupant() && spaces[wayOfWinning8[1]].GetOccupant() == spaces[wayOfWinning8[2]].GetOccupant())
                {
                    hasWinner = true;
                    winnerShape = spaces[6].GetOccupant();
                }
            }

            return (hasWinner, winnerShape);
        }

        /// <summary>
        /// Checks if there is a tie.
        /// </summary>
        /// <returns>True if the board is tied (has no empty spaces left); otherwise, false.</returns>
        public bool CheckTie()
        {
            return emptySpaces.Count == 0;
        }

        /// <summary>
        /// Returns the space instance located at the specified position of the board.
        /// </summary>
        /// <param name="position">An instance of the Position class with specified (x, y) coordinate values.</param>
        /// <returns>The space instance located at the specified position of the board.</returns>
        /// <exception cref="Exception">If no result was found.</exception>
        public Space GetSpace(Position position)
        {
            foreach (Space space in spaces)
            {
                if (space.GetPosition().GetX() == position.GetX() &&
                    space.GetPosition().GetY() == position.GetY())
                {
                    return space;
                }
            }

            throw new Exception($"No space matched the position ({position.GetX()}, {position.GetY()}).");
        }

        /// <summary>
        /// Prints the board.
        /// </summary>
        public void PrintBoard()
        {
            string template = "";

            string[] shapes = new string[spaces.Length];

            for (int i = 0; i < spaces.Length; i++)
            {
                if (spaces[i].GetOccupant() is null)
                {
                    shapes[i] = " ";
                }
                else
                {
                    shapes[i] = spaces[i].GetOccupant().ToString();
                }
            }

            template += $"   |   |   \n";
            template += $" {shapes[6]} | {shapes[7]} | {shapes[8]} \n";
            template += $"___|___|___\n";
            template += $"   |   |   \n";
            template += $" {shapes[3]} | {shapes[4]} | {shapes[5]} \n";
            template += $"___|___|___\n";
            template += $"   |   |   \n";
            template += $" {shapes[0]} | {shapes[1]} | {shapes[2]} \n";
            template += $"   |   |   \n";

            Console.WriteLine(template);
        }

        /// <summary>
        /// Takes a number and returns the space of the board where the number would be located, starting from 1, going left-to-right, bottom-to-top.
        /// </summary>
        /// <param name="number">The number of the space in the board, starting from 1, going left-to-right, bottom-to-top.</param>
        /// <returns>The space located in the board position represented by the number.</returns>
        public Space GetBoardSpaceFromInt(int number)
        {
            return spaces[number - 1];
        }

        /// <summary>
        /// Returns the result of a given board.
        /// </summary>
        /// <param name="board">The board to examine for result.</param>
        /// <returns>A GameResult representing the result of the given board.</returns>
        public static GameResult GetResultFromBoard(Board board)
        {
            GameResult result;

            (bool hasWinner, Shape? winnerShape) = board.CheckWin();
            bool hasTie = board.CheckTie();

            if (hasWinner)
            {
                if (winnerShape == Shape.X)
                {
                    result = GameResult.WinnerX;
                } 
                else if (winnerShape == Shape.O)
                {
                    result = GameResult.WinnerO;
                }
                else
                {
                    throw new Exception($"No shape matched the winner player's shape ({winnerShape}).");
                }
            } 
            else if (hasTie)
            {
                result = GameResult.Tie;
            }
            else
            {
                result = GameResult.Incomplete;
            }

            return result;
        }

        /// <summary>
        /// Returns the result of the game.
        /// </summary>
        /// <returns>The result of the game.</returns>
        public GameResult GetResult()
        {
            return result;
        }

        /// <summary>
        /// Sets the result of the game.
        /// </summary>
        /// <param name="result">The result of the game.</param>
        public void SetResult(GameResult result)
        {
            this.result = result;
        }

        /// <summary>
        /// Returns the shape that should play next based on a given board. Assumes that X always starts.
        /// </summary>
        /// <param name="board">The board to check.</param>
        /// <returns>The shape that should play next</returns>
        public static Shape GetShapeOfTurnFromBoard(Board board)
        {
            int countOfX = 0;
            int countOfO = 0;

            foreach (Space space in board.GetSpaces())
            {
                if (space.GetOccupant() is not null)
                {
                    if (space.GetOccupant() == Shape.X)
                    {
                        countOfX++;
                    }
                    else if (space.GetOccupant() == Shape.O)
                    {
                        countOfO++;
                    }
                }
            }

            return countOfX == countOfO ? Shape.X : Shape.O;
        }

        /// <summary>
        /// Returns a string representation of the board and its current state.
        /// </summary>
        /// <returns>A string representation of the board and its current state.</returns>
        public override string ToString()
        {
            string template = "";

            int count = 0;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Space space = spaces[count];

                    // Get a string representation of the shape in the given space, or "." if null.
                    string shape = space.GetOccupant() is null ? "." : space.GetOccupant().ToString();

                    template += $"{shape}";
                    count++;
                }

                template += "\n";
            }

            return template;
        }
    }
}