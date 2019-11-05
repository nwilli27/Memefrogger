using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FroggerStarter.Model.Score
{
    /// <summary>
    ///     Defines the object for a HighScore.
    /// </summary>
    public class HighScore : IComparable
    {
        private int score;
        private string playerName;
        private int level;

        /// <summary>
        ///     Initializes a new instance of the <see cref="HighScore"/> class.
        ///     Precondition: none
        ///     Post-condition: none
        /// </summary>
        /// <param name="score">The score.</param>
        /// <param name="playerName">Name of the player.</param>
        /// <param name="level">The level.</param>
        public HighScore(int score, string playerName, int level)
        {
            this.score = score;
            this.playerName = playerName;
            this.level = level;
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
            //TODO: I'll do this or figure it out.
        }
    }
}
