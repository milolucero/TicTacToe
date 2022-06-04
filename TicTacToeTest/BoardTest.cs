using TicTacToe;

namespace TicTacToeTest
{
    [TestClass]
    public class BoardTest
    {
        [TestMethod]
        public void Constructor_noArguments_Initialize()
        {
            const string Expected = "[...]\n[...]\n[...]\n";

            Board board = new Board();

            string result = board.ToString();

            Assert.AreEqual(Expected, result);
        }

        [TestMethod]
        public void Constructor_Spaces_Initialize()
        {
            const string Expected = "[XO.]\n[XXX]\n[XXX]\n";

            Space[] testSpaces = new Space[8]
            {
                new Space(new Position(0,0), Shape.X),
                new Space(new Position(0,1), Shape.O),
                new Space(new Position(0,2), Shape.None),
                new Space(new Position(1,0), Shape.X),
                new Space(new Position(1,1), Shape.X),
                new Space(new Position(1,2), Shape.X),
                new Space(new Position(2,0), Shape.X),
                new Space(new Position(2,1), Shape.X),
                //new Space(new Position(2,2), Shape.X),
            };

            Board board = new Board(testSpaces);

            string result = board.ToString();

            Assert.AreEqual(Expected, result);
        }
    }
}