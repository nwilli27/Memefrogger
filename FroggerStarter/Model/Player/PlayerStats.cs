using System;
using FroggerStarter.Constants;
using FroggerStarter.Model.Game_Objects.Lives;

namespace FroggerStarter.Model.Player
{
    /// <summary>
    ///     Class to hold statistics for the Player.
    /// </summary>
    internal static class PlayerStats
    {
        /// <summary>
        ///     Gets the score.
        /// </summary>
        /// <value>
        ///     The score.
        /// </value>
        public static int Score { get; set; }

        /// <summary>
        ///     Gets or sets the lives.
        /// </summary>
        /// <value>
        ///     The lives.
        /// </value>
        public static PlayerLives Lives { get; private set; }

        /// <summary>
        ///     Gets a value indicating whether the player has lives left.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has lives left; otherwise, <c>false</c>.
        /// </value>
        public static bool HasLivesLeft => TotalLives > 0;

        /// <summary>
        ///     Gets the total lives.
        /// </summary>
        /// <value>
        ///     The total lives.
        /// </value>
        public static int TotalLives
        {
            get => Lives.NumberOfHearts;
            set => Lives.NumberOfHearts = value;
        }

        /// <summary>
        ///     Setups the player stats.
        /// </summary>
        public static void SetupPlayerStats()
        {
            Lives = new PlayerLives();
            TotalLives = GameSettings.TotalNumberOfLives;
            Score = 0;
        }
    }

    /// <summary>
    ///     Holds the score for the score update event.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class ScoreUpdatedEventArgs : EventArgs
    {
        /// <summary>
        ///     Gets or sets the score.
        /// </summary>
        /// <value>
        ///     The score.
        /// </value>
        public int Score { get; set; }
    }

}
