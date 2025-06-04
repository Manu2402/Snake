using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Threading;
using Aiv.Draw;

namespace Snake
{
    class Program
    {
        public static void Main(string[] args)
        {
            Game.Init(); // Executed once each execution.
            Game.Play(); // Executed once each frame.
        }

    }

}
