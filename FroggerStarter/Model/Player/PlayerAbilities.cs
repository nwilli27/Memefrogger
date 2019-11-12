
using FroggerStarter.Constants;
using FroggerStarter.Model.Score;

namespace FroggerStarter.Model.Player
{
    /// <summary>
    ///     Class to hold the player's current abilities as constants
    /// </summary>
    internal static class PlayerAbilities
    {

        #region Properties

        /// <summary>
        ///     Gets or sets a value indicating whether this instance has only one revive.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has only one revive; otherwise, <c>false</c>.
        /// </value>
        public static bool HasQuickRevive { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance has double points.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has double points; otherwise, <c>false</c>.
        /// </value>
        public static bool HasDoublePoints => ScoreTimer.ScoreTick > GameSettings.ScoreTime;

        #endregion
    }
}
