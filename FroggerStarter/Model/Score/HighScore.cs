using System;

namespace FroggerStarter.Model.Score
{
    /// <summary>
    ///     Defines the object for a HighScore.
    /// </summary>
    public class HighScore : IComparable<HighScore>
    {
        #region Data members

        private readonly int score;
        private readonly string playerName;
        private readonly int level;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="HighScore" /> class.
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

        #endregion

        #region Methods

        /// <summary>
        ///     Natural sort order for HighScores.  Player Score, then Name, then level.
        ///     Precondition: none
        ///     Post-condition: none
        /// </summary>
        /// <param name="other">The other HighScore.</param>
        /// <returns>
        ///     Value greater than 0 if this HighScore is greater than the other, 0 if this HighScore equals the other
        ///     HighScore, and a value less than 0 if this HighScore is less than the other.
        /// </returns>
        public int CompareTo(HighScore other)
        {
            if (ReferenceEquals(this, other))
            {
                return 0;
            }

            if (ReferenceEquals(null, other))
            {
                return 1;
            }

            var scoreComparison = this.score.CompareTo(other.score);
            if (scoreComparison != 0)
            {
                return scoreComparison;
            }

            var playerNameComparison = string.Compare(this.playerName, other.playerName, StringComparison.Ordinal);
            if (playerNameComparison != 0)
            {
                return playerNameComparison;
            }

            return this.level.CompareTo(other.level);
        }

        #endregion
    }
}