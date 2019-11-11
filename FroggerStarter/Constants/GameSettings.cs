using System;
using Windows.UI.Xaml;

namespace FroggerStarter.Constants
{
    /// <summary>
    ///     Class holds unique data needed for game operation and setup.
    /// </summary>
    public class GameSettings
    {

        #region Data Members

        private static bool pauseGame;

        #endregion

        #region Constants

        private const int PauseTimeInterval = 5;

        /// <summary>
        ///     The total number of lives
        /// </summary>
        public const int TotalNumberOfLives = 4;

        /// <summary>
        ///     The total score time
        /// </summary>
        public const double ScoreTime = 20.0;

        #endregion

        #region Properties

        /// <summary>
        ///     The pause timer
        /// </summary>
        public static DispatcherTimer PauseTimer { get; private set; }

        /// <summary>
        ///     Gets or sets a value indicating whether [pause game].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [pause game]; otherwise, <c>false</c>.
        /// </value>
        public static bool PauseGame
        {
            get => pauseGame;
            set
            {
                if (value)
                {
                    PauseTimer = new DispatcherTimer
                    {
                        Interval = new TimeSpan(0, 0, 0, PauseTimeInterval, 0)
                    };
                }
                else
                {
                    PauseTimer.Stop();
                }
                pauseGame = value;
            }
        }

        #endregion
    }

    /// <summary>
    ///     Holds the event for a pause finishing
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class PauseIsFinishedEventArgs : EventArgs
    {

        /// <summary>
        ///     Gets or sets a value indicating whether [pause is finished].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [pause is finished]; otherwise, <c>false</c>.
        /// </value>
        public bool PauseIsFinished { get; set; }
    }
}
