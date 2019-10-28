using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using FroggerStarter.Controller;

namespace FroggerStarter.Model
{
    class ScoreTimer
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
        public static bool IsTimeUp => ScoreTick <= 0.0;

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
