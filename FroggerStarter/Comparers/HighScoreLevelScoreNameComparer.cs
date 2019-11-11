using System;
using System.Collections.Generic;
using FroggerStarter.Model.Score;

namespace FroggerStarter.Comparers
{
    /// <summary>
    ///     Comparer that checks HighScore for Level, then Score, then Name.
    /// </summary>
    public class HighScoreLevelScoreNameComparer : IComparer<HighScore>
    {
        #region Methods

        /// <summary>
        ///     Compares the specified high score1 with the high score2.
        /// </summary>
        /// <param name="highScore1">The high score1.</param>
        /// <param name="highScore2">The high score2.</param>
        /// <returns>Positive value if highScore1 is greater, 0 if they're equal, and a negative value if highScore2 is greater.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public int Compare(HighScore highScore1, HighScore highScore2)
        {
            if (highScore1 == null || highScore2 == null)
            {
                throw new ArgumentNullException();
            }

            if (ReferenceEquals(highScore1, highScore2))
            {
                return 0;
            }

            var levelComparison = highScore2.Level.CompareTo(highScore1.Level);
            if (levelComparison != 0)
            {
                return levelComparison;
            }

            var scoreComparison = highScore2.Score.CompareTo(highScore1.Score);
            return scoreComparison != 0
                ? scoreComparison
                : string.Compare(highScore1.PlayerName, highScore2.PlayerName, StringComparison.Ordinal);
        }

        #endregion
    }
}