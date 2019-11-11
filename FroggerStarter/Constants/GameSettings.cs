using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using FroggerStarter.Enums;

namespace FroggerStarter.Constants
{
    /// <summary>
    ///     Class holds unique data needed for game operation and setup.
    /// </summary>
    public class GameSettings
    {
        private static bool pauseGame;

        #region Constants

        /// <summary>
        ///     The total number of lives
        /// </summary>
        public const int TotalNumberOfLives = 4;

        /// <summary>
        ///     The total score time
        /// </summary>
        public const double ScoreTime = 20.0;

        /// <summary>
        ///     The pause timer
        /// </summary>
        public static DispatcherTimer PauseTimer;

        /// <summary>
        ///     Gets or sets a value indicating whether [pause game].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [pause game]; otherwise, <c>false</c>.
        /// </value>
        public static bool PauseGame {
            get => pauseGame;
            set
            {
                if (value)
                {
                    PauseTimer = new DispatcherTimer
                    {
                        Interval = new TimeSpan(0, 0, 0, 5, 0)
                    };
                }
                pauseGame = value;
            }
        }



        #endregion
    }
}
