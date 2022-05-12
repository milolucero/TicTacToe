﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    /// <summary>
    /// Represents the decision-making process that a bot player can use to make a move in the game.
    /// </summary>
    internal abstract class BotAI
    {
        /// <summary>
        /// Returns a random empty space.
        /// </summary>
        /// <param name="board">The board from which to choose an empty space.</param>
        /// <returns>A random empty space.</returns>
        public static Space GetRandomMove(Board board)
        {
            // If middle space is empty, choose it.
            bool middleSpaceIsEmpty = board.GetBoardSpaceFromInt(5).GetOccupant() == null;
            if (middleSpaceIsEmpty)
            {
                return board.GetBoardSpaceFromInt(5);
            }

            Random random = new Random();
            int maxRandom = board.GetEmptySpaces().Count;
            int randomEmptySpaceInt = random.Next(0, maxRandom);
            Space randomEmptySpace = board.GetEmptySpaces()[randomEmptySpaceInt];

            return Space.GetSpaceClone(randomEmptySpace);
        }

        /// <summary>
        /// Returns the optimal move for the player with the current turn on a given board move based on the minimax algorithm.
        /// </summary>
        /// <param name="board">A Board instance.</param>
        /// <returns>The optimal move for the player with the current turn on a given board move based on the minimax algorithm.</returns>
        public static Space GetMinimaxMove(Board board)
        {
            (_, Space moveChoice) = Minimax(board, false);
            return moveChoice;
        }

        /// <summary>
        /// Uses the minimax algorithm to get the score of the best possible move and the space that represents that move.
        /// </summary>
        /// <param name="board">A Board instance.</param>
        /// <param name="isMaximizing">True if we are maximizing for the player with the current turn; otherwise, false.</param>
        /// <returns>A tuple containing the score of the best possible move and the space that represents that move.</returns>
        public static (int, Space) Minimax(Board board, bool isMaximizing)
        {
            // If terminal state
            if (Board.GetResultFromBoard(board) != GameResult.Incomplete)
            {
                int resultFromBoard = GetScore(board);
                // If it's the maximizing player turn and we got here, means that it's a terminal state and the opponent just made his move, which also means that the maximizing player either lost or tied. To reflect this result, we negate its value.
                if (isMaximizing)
                {
                    resultFromBoard = -resultFromBoard;
                }
                return (resultFromBoard, new Space(new Position(-1, -1)));
            }

            List<int> scores = new List<int>();
            List<Space> moves = new List<Space>();
            List<Space> emptySpaces = board.GetEmptySpaces();

            foreach (Space emptySpace in emptySpaces)
            {
                Space move = Space.GetSpaceClone(emptySpace);
                Board nextBoard = Board.GetBoardClone(board);
                Board.OccupySpace(nextBoard, move);

                (int score, _) = Minimax(nextBoard, !isMaximizing);

                scores.Add(score);
                moves.Add(move);
            }

            if (isMaximizing)
            {
                int maxScore = scores.Max();
                int maxScoreIndex = scores.IndexOf(maxScore);
                Space moveChoice = moves[maxScoreIndex];
                return (scores[maxScoreIndex], moveChoice);
            }
            else // isMinimizing
            {
                int minScore = scores.Min();
                int minScoreIndex = scores.IndexOf(minScore);
                Space moveChoice = moves[minScoreIndex];
                return (scores[minScoreIndex], moveChoice);
            }
        }

        /// <summary>
        /// Returns the score of a board in terminal state (there is a winner or a draw) based on the result of the game. 
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static int GetScore(Board board)
        {
            int score;
            GameResult gameResult = Board.GetResultFromBoard(board);

            if (gameResult == GameResult.WinnerX || gameResult == GameResult.WinnerO)
            {
                score = 1;
            }
            else if (gameResult == GameResult.Tie)
            {
                score = 0;
            }
            else
            {
                throw new Exception("Unable to get a score from an unfinished game.");
            }

            return score;
        }
    }
}
