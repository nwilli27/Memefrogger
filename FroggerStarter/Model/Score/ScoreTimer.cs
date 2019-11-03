using System;
using FroggerStarter.Constants;

namespace FroggerStarter.Model.Score
{
    /// <summary>
    ///     Class that holds properties in regards to the score Tick and timer.
    /// </summary>
    internal class ScoreTimer
    {

        /// <summary>
        ///     Gets or sets the score tick.
        /// </summary>
        /// <value>
        ///     The score tick.
        /// </value>
        public static double ScoreTick { get; set; } = GameSettings.ScoreTime;

        /// <summary>
        ///     Checks the ScoreTick to see if its reached 0.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the ScoreTick reached 0; otherwise, <c>false</c>.
        /// </value>
        public static bool IsTimeUp => ScoreTick <= 0;

        /// <summary>
        ///     Resets the score tick back to the specified amount
        ///     of time in 'GameSettings.ScoreTime'
        /// </summary>
        public static void ResetScoreTick()
        {
            ScoreTick = GameSettings.ScoreTime;
        }
    }

    /// <summary>
    ///     Holds the numbers of lives for the life update event.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class ScoreTimerTickEventArgs : EventArgs
    {
        /// <summary>
        ///     Gets or sets the lives.
        /// </summary>
        /// <value>
        ///     The lives.
        /// </value>
        public double ScoreTick { get; set; }

    }
}
