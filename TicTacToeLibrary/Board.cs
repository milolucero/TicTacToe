using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeLibrary
{
    /// <summary>
    /// Represents the tic-tac-toe board.
    /// </summary>
    public class Board
    {
        private readonly int height;
        private readonly int width;

        /// <summary>
        /// Property representing an array of the board's spaces.
        /// </summary>
        public Space[] Spaces
        {
            get; set;
        }

        /// <summary>
        /// Property representing a list of the empty spaces of the board.
        /// </summary>
        public List<Space> EmptySpaces
        {
            get
            {
                return GetEmptySpaces();
            }

            set
            {

            }
        }

        public GameResult Result
        {
            get; set;
        }

        /// <summary>
        /// Initializes a new instance of the Board class, specifying the spaces that fill the board.
        /// If the given spaces don't fully fill the board, an unoccupied space is placed in those positions.
        /// </summary>
        /// <param name="spaces">The spaces that populate the board.</param>
        /// <exception cref="ArgumentOutOfRangeException">If any of the given spaces is our of the board's range or if the given spaces have more items than the board's range.</exception>
        public Board(Space[] spaces)
        {
            // Guard clause
            Validate.BoardConstructorSpacesArgument(spaces);

            height = Rule.GetBoardDimensions().height;
            width = Rule.GetBoardDimensions().width;

            // Populate board with empty spaces
            this.Spaces = GetArrayOfEmptySpacesForBoard();

            // Place the given spaces in their positions
            foreach (Space space in spaces)
            {
                // Guard clause if given space is out of board's range 
                if (space.Position.X >= this.height || space.Position.Y >= this.width)
                    throw new ArgumentOutOfRangeException(nameof(spaces), $"Space in position ({space.Position.X}, {space.Position.Y}) is out of the board's height and width.");

                // Locate correct position and place space
                for (int i = 0; i < Spaces.Length; i++)
                {
                    if (space.Position.X == Spaces[i].Position.X && space.Position.Y == Spaces[i].Position.Y)
                    {
                        Spaces[i] = space;
                    }
                }
            }

            this.Result = GetResultFromBoard(this);
        }

        /// <summary>
        /// Initializes a new instance of the Board class without arguments, filling the board with empty (unassigned) spaces.
        /// </summary>
        public Board() : this(GetArrayOfEmptySpacesForBoard())
        {

        }

        /// <summary>
        /// Returns a list containing the currently empty spaces of the given board.
        /// </summary>
        /// <param name="board">The board object.</param>
        /// <returns>A list containing the currently empty spaces of the board.</returns>
        public static List<Space> GetEmptySpaces(Board board)
        {
            List<Space> emptySpaces = new List<Space>();

            foreach (Space space in board.Spaces)
            {
                if (!space.IsOccupied())
                {
                    emptySpaces.Add(space);
                }
            }

            return emptySpaces;
        }

        /// <summary>
        /// Returns a list containing the currently empty spaces of the current board.
        /// </summary>
        /// <returns>A list containing the currently empty spaces of the current board.</returns>
        public List<Space> GetEmptySpaces()
        {
            return GetEmptySpaces(this);
        }

        /// <summary>
        /// Returns an array of empty spaces for the specified board height and width.
        /// </summary>
        /// <param name="boardHeight">The height of the board.</param>
        /// <param name="boardWidth">The width of the board.</param>
        /// <returns>An array of empty spaces for the specified board height and width.</returns>
        public static Space[] GetArrayOfEmptySpacesForBoard()
        {
            int boardHeight = Rule.GetBoardDimensions().height;
            int boardWidth = Rule.GetBoardDimensions().width;

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
        /// Occupies a specific space of a board by the shape that has the turn. If a player is specified, the player's occupied spaces are updated.
        /// </summary>
        /// <param name="board">The board.</param>
        /// <param name="space">The space to occupy.</param>
        /// <param name="player">The player whose state should be updated, or null (default).</param>
        /// <exception cref="ArgumentException">If the given space is already occupied.</exception>
        public static void OccupySpace(Board board, Space space, Player player = null)
        {
            // Guard clause
            if (space.IsOccupied())
                throw new ArgumentException($"Cannot occupy space in position (X: {space.Position.X}, Y: {space.Position.Y}) by player {player.Name}, since it is not empty (already taken by {space.Occupant}).");

            Shape shapeToPlay = GetShapeOfTurnFromBoard(board);

            // Make a clone of the space, set the occupant, and assign it to the same space position in the given board.
            // This will avoid mutating the given space, allowing for simulating moves without affecting the original argument's state.
            Space spaceClone = Space.GetSpaceClone(space);
            spaceClone.Occupant = shapeToPlay;
            board.SetSpace(spaceClone);

            // If given a player as an argument, add the given space to the spaces that belong to this player.
            if (!(player is null))
                player.AddToOccupiedSpaces(space);

            List<Space> emptySpaces = board.EmptySpaces;
            emptySpaces.Remove(space);
            board.EmptySpaces = emptySpaces;
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

            if (Spaces[0].IsOccupied())
            {
                if ((Spaces[wayOfWinning1[0]].Occupant == Spaces[wayOfWinning1[1]].Occupant && Spaces[wayOfWinning1[1]].Occupant == Spaces[wayOfWinning1[2]].Occupant) ||
                    (Spaces[wayOfWinning2[0]].Occupant == Spaces[wayOfWinning2[1]].Occupant && Spaces[wayOfWinning2[1]].Occupant == Spaces[wayOfWinning2[2]].Occupant) ||
                    (Spaces[wayOfWinning3[0]].Occupant == Spaces[wayOfWinning3[1]].Occupant && Spaces[wayOfWinning3[1]].Occupant == Spaces[wayOfWinning3[2]].Occupant))
                {
                    hasWinner = true;
                    winnerShape = Spaces[0].Occupant;
                }
            }

            if (Spaces[1].IsOccupied())
            {
                if (Spaces[wayOfWinning4[0]].Occupant == Spaces[wayOfWinning4[1]].Occupant && Spaces[wayOfWinning4[1]].Occupant == Spaces[wayOfWinning4[2]].Occupant)
                {
                    hasWinner = true;
                    winnerShape = Spaces[1].Occupant;
                }
            }

            if (Spaces[2].IsOccupied())
            {
                if ((Spaces[wayOfWinning5[0]].Occupant == Spaces[wayOfWinning5[1]].Occupant && Spaces[wayOfWinning5[1]].Occupant == Spaces[wayOfWinning5[2]].Occupant) ||
                    (Spaces[wayOfWinning6[0]].Occupant == Spaces[wayOfWinning6[1]].Occupant && Spaces[wayOfWinning6[1]].Occupant == Spaces[wayOfWinning6[2]].Occupant))
                {
                    hasWinner = true;
                    winnerShape = Spaces[2].Occupant;
                }
            }

            if (Spaces[3].IsOccupied())
            {
                if (Spaces[wayOfWinning7[0]].Occupant == Spaces[wayOfWinning7[1]].Occupant && Spaces[wayOfWinning7[1]].Occupant == Spaces[wayOfWinning7[2]].Occupant)
                {
                    hasWinner = true;
                    winnerShape = Spaces[3].Occupant;
                }
            }

            if (Spaces[6].IsOccupied())
            {
                if (Spaces[wayOfWinning8[0]].Occupant == Spaces[wayOfWinning8[1]].Occupant && Spaces[wayOfWinning8[1]].Occupant == Spaces[wayOfWinning8[2]].Occupant)
                {
                    hasWinner = true;
                    winnerShape = Spaces[6].Occupant;
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
            return EmptySpaces.Count == 0;
        }

        /// <summary>
        /// Returns the space instance located at the specified position of the board.
        /// </summary>
        /// <param name="position">An instance of the Position class with specified (x, y) coordinate values.</param>
        /// <returns>The space instance located at the specified position of the board.</returns>
        /// <exception cref="Exception">If no result was found.</exception>
        public Space GetSpace(Position position)
        {
            foreach (Space space in Spaces)
            {
                if (space.Position.X == position.X &&
                    space.Position.Y == position.Y)
                {
                    return space;
                }
            }

            throw new Exception($"No space matched the position ({position.X}, {position.Y}).");
        }

        /// <summary>
        /// Replaces the space in the board by the given space object. The space replaced corresponds to the same board position as the space given.
        /// </summary>
        /// <param name="space">The space that will be placed on the board.</param>
        public void SetSpace(Space space)
        {
            // To get a 0-8 position from a (x, y) coordinate, we add x to y times 3.
            // BUG: Positions are inverted, a space(2, 0) should be in position x = 2, y = 0, but is instead in x = 0, y = 2. Inverting the variables here is a hotfix, but needs to be fixed before it cascades into other bugs.
            int y = space.Position.Y;
            int x = space.Position.X;
            int arrayPosition = y + (x * 3); // The correct formula should be x + (y * 3)
            Spaces[arrayPosition] = space;
        }

        /// <summary>
        /// Returns a clone of a given board, with equal field values, but different references. This allows to perform simulations on board clones, without affecting the game's state.
        /// </summary>
        /// <param name="board">The board to clone.</param>
        /// <returns>A clone of the board.</returns>
        public static Board GetBoardClone(Board board)
        {
            Board clone = new Board();

            // Clone spaces of board
            for (int i = 0; i < board.Spaces.Length; i++)
            {
                clone.Spaces[i] = Space.GetSpaceClone(board.Spaces[i]);
            }

            // Clone array of empty spaces
            for (int i = 0; i < board.EmptySpaces.Count; i++)
            {
                clone.EmptySpaces[i] = Space.GetSpaceClone(board.EmptySpaces[i]);
            }

            // Set result
            // clone.Result = GetResultFromBoard(clone);
            clone.Result = board.Result;

            return clone;
        }

        /// <summary>
        /// Prints the board.
        /// </summary>
        public void PrintBoard()
        {
            string template = "";

            string[] shapes = new string[Spaces.Length];

            for (int i = 0; i < Spaces.Length; i++)
            {
                if (Spaces[i].Occupant is Shape.None)
                {
                    shapes[i] = " ";
                }
                else
                {
                    shapes[i] = Spaces[i].Occupant.ToString();
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
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the integer position is out of range for the board's spaces.</exception>
        public Space GetBoardSpaceFromInt(int number)
        {
            // Validate that the number is within the board's range.
            (int height, int width) = Rule.GetBoardDimensions();

            int min = 1;
            int max = height * width;

            if (number >= min && number <= max)
            {
                return Spaces[number - 1];
            }

            throw new ArgumentOutOfRangeException($"Space out of bounds. No space of the board matches the integer position \"{number}\".");
        }

        /// <summary>
        /// Takes an (x, y) coordinate and returns the space of the board located in that position.
        /// </summary>
        /// <param name="x">The horizontal coordinate.</param>
        /// <param name="y">The vertical coordinate.</param>
        /// <returns>The space located at the specified coordinate.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the coordinates are out of range for the board's spaces.</exception>
        public Space GetBoardSpaceFromCoordinates(int x, int y)
        {
            // To get a 0-8 position from an (x, y) coordinate, we add x to y times the board's width.
            int positionOnArray = x + (y * 3);

            // Check if space is in range
            if (positionOnArray < Spaces.Length)
            {
                return Spaces[(x + 1) + (y * 3)];
            }

            throw new ArgumentOutOfRangeException($"Space out of bounds. Trying to get space ({x}, {y}), equivalent to position [{positionOnArray}] of an array of {Spaces.Length} elements.");
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
        /// Returns the shape that should play next based on a given board. Assumes that X always starts.
        /// </summary>
        /// <param name="board">The board to check.</param>
        /// <returns>The shape that should play next</returns>
        public static Shape GetShapeOfTurnFromBoard(Board board)
        {
            int countOfX = 0;
            int countOfO = 0;

            foreach (Space space in board.Spaces)
            {
                if (!(space.Occupant is Shape.None))
                {
                    if (space.Occupant == Shape.X)
                    {
                        countOfX++;
                    }
                    else if (space.Occupant == Shape.O)
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
                template += "[";

                for (int j = 0; j < width; j++)
                {
                    Space space = Spaces[count];

                    // Get a string representation of the shape in the given space, or "." if null.
                    string shape = space.Occupant is Shape.None ? "." : space.Occupant.ToString();

                    template += $"{shape}";
                    count++;
                }

                template += "]\n";
            }

            return template;
        }
    }
}