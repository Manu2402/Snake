using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    enum Colors
    {
        Green, DarkGreen, Black, LightBlue, LAST //Enum di colori, usati per scopo preciso
    }

    static class ColorsFactory
    {
        public static Color color;

        public static Color GetColor(Colors col) //Passo un colore come parametro e in base al suo valore RGB lo associo alla Enum e lo ritorno
        {
            switch (col)
            {
                case Colors.Green:
                    color.R = 0;
                    color.G = 255;
                    color.B = 0;
                    break;
                case Colors.DarkGreen:
                    color.R = 34;
                    color.G = 139;
                    color.B = 34;
                    break;
                case Colors.Black:
                    color.R = 0;
                    color.G = 0;
                    color.B = 0;
                    break;
                case Colors.LightBlue:
                    color.R = 0;
                    color.G = 127;
                    color.B = 255;
                    break;
                default:
                    break;
            }
            return color;
        }


    }
}
