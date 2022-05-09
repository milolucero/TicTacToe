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
        }

        /// <summary>
        /// Initializes a new instance of the Board class without arguments, filling the board with empty (unassigned) spaces.
        /// </summary>
        public Board() : this(GetArrayOfEmptySpacesForBoard(Rule.GetBoardDimensions().height, Rule.GetBoardDimensions().width))
        {

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
        /// Checks if there is a winner. Returns a tuple where the first value is true if a winner was found; otherwise, false. The second value is the winning player if there was one found; otherwise, null.
        /// </summary>
        /// <returns>A tuple where the first value is true if a winner was found; otherwise, false. The second value is the winning player if there was one found; otherwise, null.</returns>
        public (bool hasWinner, Player? winner) CheckWin()
        {
            bool hasWinner = false;
            Player? winner = null;

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
                    winner = spaces[0].GetOccupant();
                }
            }

            if (spaces[1].IsOccupied())
            {
                if (spaces[wayOfWinning4[0]].GetOccupant() == spaces[wayOfWinning4[1]].GetOccupant() && spaces[wayOfWinning4[1]].GetOccupant() == spaces[wayOfWinning4[2]].GetOccupant())
                {
                    hasWinner = true;
                    winner = spaces[1].GetOccupant();
                }
            }

            if (spaces[2].IsOccupied())
            {
                if ((spaces[wayOfWinning5[0]].GetOccupant() == spaces[wayOfWinning5[1]].GetOccupant() && spaces[wayOfWinning5[1]].GetOccupant() == spaces[wayOfWinning5[2]].GetOccupant()) ||
                    (spaces[wayOfWinning6[0]].GetOccupant() == spaces[wayOfWinning6[1]].GetOccupant() && spaces[wayOfWinning6[1]].GetOccupant() == spaces[wayOfWinning6[2]].GetOccupant()))
                {
                    hasWinner = true;
                    winner = spaces[2].GetOccupant();
                }
            }

            if (spaces[3].IsOccupied())
            {
                if (spaces[wayOfWinning7[0]].GetOccupant() == spaces[wayOfWinning7[1]].GetOccupant() && spaces[wayOfWinning7[1]].GetOccupant() == spaces[wayOfWinning7[2]].GetOccupant())
                {
                    hasWinner = true;
                    winner = spaces[3].GetOccupant();
                }
            }

            if (spaces[6].IsOccupied())
            {
                if (spaces[wayOfWinning8[0]].GetOccupant() == spaces[wayOfWinning8[1]].GetOccupant() && spaces[wayOfWinning8[1]].GetOccupant() == spaces[wayOfWinning8[2]].GetOccupant())
                {
                    hasWinner = true;
                    winner = spaces[6].GetOccupant();
                }
            }

            return (hasWinner, winner);
        }

        /// <summary>
        /// Checks if there is a draw.
        /// </summary>
        public bool CheckDraw()
        {
            bool isDraw = false;

            if (emptySpaces.Count() == 0)
            {
                isDraw = true;
            }

            return isDraw;
        }

        /// <summary>
        /// Returns the space instance located at the specified position of the board.
        /// </summary>
        /// <param name="position">An instance of the Position class with specified (x, y) coordinate values.</param>
        /// <returns>The space instance located at the specified position of the board.</returns>
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

            // If no result was found
            throw new Exception($"Error: No space matched the position ({position.GetX()}, {position.GetY()}).");
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
                    shapes[i] = spaces[i].GetOccupant().GetShape().ToString();
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
        /// <param name="number"></param>
        /// <returns>The space located in the board position represented by the number.</returns>
        public Space GetBoardSpaceFromInt(int number)
        {
            return spaces[number - 1];
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
                    string shape = space.GetOccupant() is null ? "." : space.GetOccupant().GetShape().ToString();

                    template += $"{shape}";
                    count++;
                }

                template += "\n";
            }

            return template;
        }
    }
}