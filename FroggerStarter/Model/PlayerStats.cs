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
        public int Score { get; private set; }

        /// <summary>
        ///     Gets the lives.
        /// </summary>
        /// <value>
        ///     The lives.
        /// </value>
        public int Lives { get; private set; } = 3;

        /// <summary>
        ///     Decrements the lives by one.
        ///     Precondition: none
        ///     Post-condition: this.Lives--
        /// </summary>
        public void decrementLivesByOne()
        {
            this.Lives--;
        }

        /// <summary>
        ///     Increments the score by one
        ///     Precondition: none
        ///     Post-condition: this.Score++
        /// </summary>
        public void incrementScoreByOne()
        {
            this.Score++;
        }
    }

    /// <summary>
    ///     Holds the numbers of lives for the life update event.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class LiveLossEventArgs : EventArgs
    {
        public int Lives { get; set; }
    }

    /// <summary>
    ///     Holds the score for the score update event.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class ScoreUpdatedEventArgs : EventArgs
    {
        public int Score { get; set; }
    }

}
