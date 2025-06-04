using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    static class Score
    {
        public static int ScorePT;

        static Score()
        {
            ScorePT = 0;
        }

        public static void AddPT()
        {
            ScorePT++;
        }

    }
}
