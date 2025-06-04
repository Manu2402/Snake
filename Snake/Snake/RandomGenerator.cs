using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    static class RandomGenerator
    {
        public static Random random;

        static RandomGenerator()
        {
            random = new Random();
        }

        public static float GetRandomInt()
        {
            return random.Next(1, 100);
        }

        public static float GetRandomFloat()
        {
            return (float)((random.NextDouble() + 50) * GetRandomInt()); // [50f, 5000f]
        }
    }
}
