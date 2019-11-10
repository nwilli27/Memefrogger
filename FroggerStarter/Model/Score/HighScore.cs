using System;

namespace FroggerStarter.Model.Score
{
    /// <summary>
    ///     Defines the object for a HighScore.
    /// </summary>
    public class HighScore : IComparable<HighScore>
    {
        #region Properties

        /// <summary>
        ///     Gets the score.
        /// </summary>
        /// <value>
        ///     The score.
        /// </value>
        public int Score { get; set; }

        /// <summary>
        ///     Gets the name of the player.
        /// </summary>
        /// <value>
        ///     The name of the player.
        /// </value>
        public string PlayerName { get; set; }

        /// <summary>
        ///     Gets the level.
        /// </summary>
        /// <value>
        ///     The level.
        /// </value>
        public int Level { get; set; }

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
            this.Score = score;
            this.PlayerName = playerName;
            this.Level = level;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="HighScore" /> class.
        ///     Precondition: none
        ///     Post-condition: Score == 0; PlayerName == ""; Level == 0
        /// </summary>
        public HighScore() : this(0, "", 0)
        {
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

            var scoreComparison = this.Score.CompareTo(other.Score);
            if (scoreComparison != 0)
            {
                return scoreComparison;
            }

            var playerNameComparison = string.Compare(this.PlayerName, other.PlayerName, StringComparison.Ordinal);
            return playerNameComparison != 0 ? playerNameComparison : this.Level.CompareTo(other.Level);
        }

        #endregion
    }
}