using TicTacToe;

namespace TicTacToeTest
{
    [TestClass]
    public class BoardTest
    {
        // Initialize a board without arguments.
        [TestMethod]
        public void Constructor_noArguments_Initialize()
        {
            const string Expected = "[...]\n[...]\n[...]\n";

            Board board = new Board();

            string result = board.ToString();

            Assert.AreEqual(Expected, result);
        }

        // Initialize a board with given spaces in correct format.
        [TestMethod]
        public void Constructor_Spaces_Initialize()
        {
            const string Expected = "[XO.]\n[XXX]\n[XXX]\n";

            Space[] testSpaces = new Space[9]
            {
                new Space(new Position(0,0), Shape.X),
                new Space(new Position(0,1), Shape.O),
                new Space(new Position(0,2), Shape.None),
                new Space(new Position(1,0), Shape.X),
                new Space(new Position(1,1), Shape.X),
                new Space(new Position(1,2), Shape.X),
                new Space(new Position(2,0), Shape.X),
                new Space(new Position(2,1), Shape.X),
                new Space(new Position(2,2), Shape.X),
            };

            Board board = new Board(testSpaces);

            string result = board.ToString();

            Assert.AreEqual(Expected, result);
        }

        // Initializing a board while passing a space argument with less spaces than the board's height * width.
        [TestMethod]
        public void Constructor_LessSpaces_Initialize()
        {
            const string Expected = "[X..]\n[...]\n[...]\n";

            Space[] testSpaces = new Space[1]
            {
                new Space(new Position(0,0), Shape.X),
            };

            Board board = new Board(testSpaces);

            string result = board.ToString();

            Assert.AreEqual(Expected, result);
        }

        // Initializing a board while passing a space argument with more spaces than the board's height * width.
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_MoreSpaces_Initialize()
        {
            Space[] testSpaces = new Space[10]
            {
                new Space(new Position(0,0), Shape.X),
                new Space(new Position(0,1), Shape.O),
                new Space(new Position(0,2), Shape.None),
                new Space(new Position(1,0), Shape.X),
                new Space(new Position(1,1), Shape.X),
                new Space(new Position(1,2), Shape.X),
                new Space(new Position(2,0), Shape.X),
                new Space(new Position(2,1), Shape.X),
                new Space(new Position(2,2), Shape.X),
                new Space(new Position(2,2), Shape.X),
            };

            new Board(testSpaces);
        }

        // Initializing a board while passing a space argument with a space's position out of the board's range.
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_SpaceOutOfRange_Initialize()
        {
            Space[] testSpaces = new Space[1]
            {
                new Space(new Position(0, 9999)),
            };

            new Board(testSpaces);
        }

        // Initializing a board while passing a spaces argument with two spaces at the same position.
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_RepeatedSpace_Initialize()
        {
            Space[] testSpaces = new Space[2]
            {
                new Space(new Position(1,1), Shape.None),
                new Space(new Position(1,1), Shape.None),
            };

            Board board = new Board(testSpaces);
        }

        [TestMethod]
        public void GetEmptySpaces_OneEmptySpace_ListOfEmptySpaces()
        {
            Space emptySpace = new Space(new Position(0, 2), Shape.None);

            Space[] testSpaces = new Space[9]
            {
                new Space(new Position(0,0), Shape.X),
                new Space(new Position(0,1), Shape.O),
                emptySpace,
                new Space(new Position(1,0), Shape.X),
                new Space(new Position(1,1), Shape.X),
                new Space(new Position(1,2), Shape.X),
                new Space(new Position(2,0), Shape.X),
                new Space(new Position(2,1), Shape.X),
                new Space(new Position(2,2), Shape.X),
            };

            Board board = new Board(testSpaces);

            List<Space> emptySpaces = Board.GetEmptySpaces(board);

            // Test number of resulting empty spaces
            const int expectedLength = 1;
            Assert.AreEqual(expectedLength, emptySpaces.Count);

            // Compare values of resulting empty spaces
            Assert.AreEqual(emptySpace, emptySpaces[0]);
        }

        [TestMethod]
        public void GetEmptySpaces_AllSpacesEmpty_ListOfEmptySpaces()
        {
            Space[] testSpaces = new Space[9]
            {
                new Space(new Position(0,0), Shape.None),
                new Space(new Position(0,1), Shape.None),
                new Space(new Position(0,2), Shape.None),
                new Space(new Position(1,0), Shape.None),
                new Space(new Position(1,1), Shape.None),
                new Space(new Position(1,2), Shape.None),
                new Space(new Position(2,0), Shape.None),
                new Space(new Position(2,1), Shape.None),
                new Space(new Position(2,2), Shape.None),
            };

            Board board = new Board(testSpaces);

            List<Space> emptySpaces = Board.GetEmptySpaces(board);

            // Test number of resulting empty spaces
            const int expectedLength = 9;
            Assert.AreEqual(expectedLength, emptySpaces.Count);
        }

        [TestMethod]
        public void GetEmptySpaces_NoEmptySpaces_ListOfEmptySpaces()
        {
            Space[] testSpaces = new Space[9]
            {
                new Space(new Position(0,0), Shape.X),
                new Space(new Position(0,1), Shape.O),
                new Space(new Position(0,2), Shape.O),
                new Space(new Position(1,0), Shape.X),
                new Space(new Position(1,1), Shape.X),
                new Space(new Position(1,2), Shape.X),
                new Space(new Position(2,0), Shape.X),
                new Space(new Position(2,1), Shape.X),
                new Space(new Position(2,2), Shape.X),
            };

            Board board = new Board(testSpaces);

            List<Space> emptySpaces = Board.GetEmptySpaces(board);

            // Test number of resulting empty spaces
            const int expectedLength = 0;
            Assert.AreEqual(expectedLength, emptySpaces.Count);
        }

        // Resulting empty array must be:
        // 1. The same length as the board's height * width.
        // 2. All spaces must be empty (space.Occupant is Shape.None)
        // 3. Positions must start at (0,0) and increment according to the height and width.
        [TestMethod]
        public void GetArrayOfEmptySpacesForBoard_ArrayOfEmptySpaces()
        {
            Space[] arrayOfEmptySpaces = Board.GetArrayOfEmptySpacesForBoard();
            int boardHeight = Rule.GetBoardDimensions().height;
            int boardWidth = Rule.GetBoardDimensions().width;

            // 1. Test length of result
            int expectedLength = boardHeight * boardWidth;
            Assert.AreEqual(expectedLength, arrayOfEmptySpaces.Length);

            // 2. Test content of resulting spaces (empty => Shape.None)
            foreach (Space space in arrayOfEmptySpaces)
            {
                if (space.Occupant is not Shape.None)
                    Assert.Fail($"space in position (X: {space.Position.X}, Y: {space.Position.Y}) is not empty (occupant: {space.Occupant}).");
            }

            // 3.Positions must start at(0, 0) and increment according to the height and width.
            for (int i = 0; i < boardHeight; i++)
            {
                for (int j = 0; j < boardWidth; j++)
                {
                    int currentArrayPosition = (i * boardWidth) + j;
                    if (arrayOfEmptySpaces[currentArrayPosition].Position.X != i || arrayOfEmptySpaces[currentArrayPosition].Position.Y != j)
                        Assert.Fail($"The positions of the spaces are not in the correct order. Board expected position (X: {i}, Y: {j}), but received space with position (X: {arrayOfEmptySpaces[currentArrayPosition].Position.X}, Y: {arrayOfEmptySpaces[currentArrayPosition].Position.Y})");
                }
            }
        }

        // 1. The occupied space is applied to the given board.
        // 2. The occupied space is removed from the board's empty spaces.
        // 3. The occupied space is in the correct position.
        // 4. The space is occupied by the given player.
        // 5. The space is occupied by null if no player parameter is given.
        // 6. If the given space is already occupied, ArgumentException is thrown.
        // 7. If a player is given, it space is added to the player.OccupiedSpaces.
        [TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        public void OccupySpace_()
        {
            // Create a test game, user is "X"
            //Game game = new Game();
            //game.SetUserPlayer(Shape.X);

            // Create a test game, the next turn corresponds to O.
            //Game game = SimulateGameFromBoard(GetTestBoard(2));
            /*
             * Board 2 - Expected
             * X..
             * .X.
             * ..O
             */

            /* Actual
             * O..
             * .X.
             * ..X
             */ 
            Board board = GetTestBoard(2);

            Space choiceOfSpaceToOccupy;
            int amountOfSpacesOnBoard = Rule.GetBoardDimensions().height * Rule.GetBoardDimensions().width;

            // Check that the amount of empty spaces on the board are correct.
            //Assert.AreEqual(board.GetEmptySpaces().Count, amountOfSpacesOnBoard);

            // Turn (O): Occupy bottom center space.
            //choiceOfSpaceToOccupy = board.GetBoardSpaceFromInt(2);
            //Board.OccupySpace(board, choiceOfSpaceToOccupy);

            // Check if O is now on the space.
            Assert.IsTrue(VerifyOccupant(board, positionX: 0, positionY: 2, Shape.O));

            // Turn (X): Tries to occupy the space that is occupied by X.
            //choiceOfSpaceToOccupy = board.GetBoardSpaceFromInt(2);
            //Board.OccupySpace(board, choiceOfSpaceToOccupy);
            // No spaces occupied here, an exception is expected for trying to occupy the occupied space.
        }

        /// <summary>
        /// Returns true if the space on the specified position of the given board matches the specified occupant; otherwise, false. 
        /// </summary>
        /// <param name="board">The board to check.</param>
        /// <param name="positionX">The coordinate X to check.</param>
        /// <param name="positionY">The coordinate Y to check.</param>
        /// <param name="occupant">The occupant to compare against the current space on the board in the specified coordinates.</param>
        /// <returns>True if the space on the specified position of the given board matches the specified occupant; otherwise, false.</returns>
        /// <exception cref="ArgumentException">If there is no space in the board that matches the specified coordinates.</exception>
        private bool VerifyOccupant(Board board, int positionX, int positionY, Shape occupant)
        {
            foreach (Space space in board.Spaces)
            {
                if (space.Position.X == positionX && space.Position.Y == positionY)
                    return space.Occupant == occupant;
            }

            throw new ArgumentException($"Occupant cannot be validated. No space on the board matched position (X: {positionX}, Y: {positionY}).");
        }

        /// <summary>
        /// Simulates a new game with the specified board.
        /// </summary>
        /// <param name="board">The board from which the game should continue.</param>
        private static void SimulateGameFromBoard(Board board)
        {
            Game game = new Game(board);
            game.NewGame();
        }

        /// <summary>
        /// Returns a board filled with specific occupied spaces designed for testing purposes.
        /// </summary>
        /// <param name="boardNumber">The number of the board to get returned (see inline comments to see the board design).</param>
        /// <returns></returns>
        private static Board GetTestBoard(int boardNumber)
        {
            Board board = new Board();

            /*
             * Board 1
             * O.X
             * X.X
             * .OO
             */
            if (boardNumber == 1)
            {
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(9));
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(7));
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(6));
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(3));
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(4));
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(2));
            }

            /*
             * Board 2
             * X..
             * .X.
             * ..O
             */
            if (boardNumber == 2)
            {
                Board.OccupySpace(board, board.GetSpace(new Position(0, 2))); // X
                Board.OccupySpace(board, board.GetSpace(new Position(2, 0))); // O
                Board.OccupySpace(board, board.GetSpace(new Position(1, 1))); // X
            }

            /*
             * Board 3
             * OXX
             * OOX
             * ...
             */
            if (boardNumber == 3)
            {
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(9));
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(7));
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(8));
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(5));
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(6));
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(4));
            }

            /*
             * Board 4 - O Plays. Two options: One move leads to draw, the other leads to O losing.
             * OXX
             * XXO
             * O..
             */
            if (boardNumber == 4)
            {
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(9)); // X
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(7)); // O
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(8)); // X
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(6)); // O
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(5)); // X
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(1)); // O
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(4)); // X
            }

            /*
             * Board 5 - X plays. The best move is on space 4, since it guarantees a win in X's next turn, regardless of O's move.
             * Use this to test the bot's algorithm. Playing as X, the bot should always pick space 4 in its first turn.
             * XO.
             * ..X
             * ..O
             */
            if (boardNumber == 5)
            {
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(7)); // X
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(8)); // O
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(6)); // X
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(3)); // O
            }

            /*
             * Board 6 - O plays. Both alternatives lead to O's losing. Check how the bot behaves when playing as O.
             * XXO
             * OXX
             * O..
             */
            if (boardNumber == 6)
            {
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(8)); // X
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(9)); // O
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(7)); // X
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(4)); // O
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(6)); // X
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(1)); // O
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(5)); // X
            }

            /*
             * Board 7 - O plays. X is about to win by choosing 6. O must decide to choose 6 to keep alive.
             * O.X
             * XX.
             * O..
             */
            if (boardNumber == 7)
            {
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(9)); // X
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(7)); // O
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(5)); // X
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(1)); // O
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(4)); // X
            }

            /*
             * Board 8 - X plays. 3 options, one move leads to a win, the others lead to a lose in the next turn. X must choose 7 to win.
             * .OO
             * X..
             * XXO
             */
            if (boardNumber == 8)
            {
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(1)); // X
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(9)); // O
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(2)); // X
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(3)); // O
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(4)); // X
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(8)); // O
            }

            return board;
        }
    }
}