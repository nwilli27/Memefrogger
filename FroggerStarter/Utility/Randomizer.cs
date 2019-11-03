using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FroggerStarter.Utility
{
    /// <summary>
    ///     A class that holds basic generation of random values.
    /// </summary>
    internal class Randomizer
    {

        /// <summary>
        ///     Gets the random value in range.
        ///     Precondition: none
        ///     Post-condition: none
        /// </summary>
        /// <param name="lowerBound">The lower bound.</param>
        /// <param name="upperBound">The upper bound.</param>
        /// <returns>
        ///     A random value in range of lowerBound and upperBound
        /// </returns>
        public static int getRandomValueInRange(int lowerBound, int upperBound)
        {
            var random = new Random();
            return random.Next(lowerBound, upperBound);
        }

    }
}
