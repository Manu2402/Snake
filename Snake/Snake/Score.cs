using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    static class Score
    {
        public static int ScorePT; //Punteggio

        static Score()
        {
            ScorePT = 0; //Inizia da zero
        }

        public static void AddPT()
        {
            ScorePT++; //Aumenta ad ogni collisione con la mela
        }

    }
}
