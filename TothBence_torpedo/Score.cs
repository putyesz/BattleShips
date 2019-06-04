using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TothBence_torpedo
{
    class Score
    {
        private int _score;
        private int _place;

        private string _name;

        private enum Win
        {
            Win, Lose
        };

        public  Score(int score, String name)
        {
            _score = score;
            _name = name;
        }


    }
}
