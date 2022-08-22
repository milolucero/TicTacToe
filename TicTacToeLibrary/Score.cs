using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeLibrary
{
    public class Score
    {
        int scoreUser;
        int scoreBot;
        int scoreTie;

        public Score()
        {
            scoreUser = 0;
            scoreBot = 0;
            scoreTie = 0;
        }

        public void ResetScores()
        {
            scoreUser = 0;
            scoreBot = 0;
            scoreTie = 0;
        }
    }
}
