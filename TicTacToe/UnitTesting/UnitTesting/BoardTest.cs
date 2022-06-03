using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TicTacToe;

namespace UnitTesting
{
    [TestClass]
    public class BoardTest
    {
        [TestMethod]
        public void Constructor_noArguments_Initialize()
        {
            Board board = new BoardTest();
            PrivateObject obj = new PrivateObject(board);
        }
    }
}
