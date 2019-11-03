using System;

namespace FroggerStarter.Model
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
        ///     Gets the lives.
        /// </summary>
        /// <value>
        ///     The lives.
        /// </value>
        public int Lives { get; set; }

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
