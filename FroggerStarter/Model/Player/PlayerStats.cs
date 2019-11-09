using System;
using Windows.UI.Xaml;
using FroggerStarter.Model.Game_Objects.Lives;

namespace FroggerStarter.Model.Player
{
    /// <summary>
    ///     Class to hold statistics for the Player.
    /// </summary>
    internal class PlayerStats
    {
        /// <summary>
        ///     Gets the score.
        /// </summary>
        /// <value>
        ///     The score.
        /// </value>
        public int Score { get; set; }

        /// <summary>
        ///     Gets or sets the lives.
        /// </summary>
        /// <value>
        ///     The lives.
        /// </value>
        public PlayerLives Lives { get; }

        /// <summary>
        ///     Gets the total lives.
        /// </summary>
        /// <value>
        ///     The total lives.
        /// </value>
        public int TotalLives
        {
            get => this.Lives.NumberOfHearts;
            set => this.Lives.NumberOfHearts = value;
        }

        public PlayerStats()
        {
            this.Lives = new PlayerLives();
        }

    }

    /// <summary>
    ///     Holds the numbers of lives for the life update event.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class LivesUpdatedEventArgs : EventArgs
    {
        /// <summary>
        ///     Gets or sets the lives.
        /// </summary>
        /// <value>
        ///     The lives.
        /// </value>
        public int Lives { get; set; }

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
