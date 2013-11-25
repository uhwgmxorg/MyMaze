using System;
using System.Linq;

namespace Tools
{
    public class StaticTools
    {
        static private Random Random = new Random();

        /// <summary>
        /// RandomDouble
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        static public double RandomDouble(double min, double max)
        {
            double d;
            d = (double)Random.NextDouble() * (max - min) + min;
            return d;
        }
    }
}
