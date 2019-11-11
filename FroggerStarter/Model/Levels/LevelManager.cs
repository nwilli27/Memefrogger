
using System.Collections.Generic;
using FroggerStarter.Model.Levels.Levels;

namespace FroggerStarter.Model.Levels
{
    /// <summary>
    ///     Class responsible for managing multiple levels
    /// </summary>
    internal static class LevelManager
    {
        #region Data Members

        private static IList<Level> levels;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the current level.
        /// </summary>
        /// <value>
        ///     The current level.
        /// </value>
        public static int CurrentLevel { get; set; } = 1;

        /// <summary>
        ///     Gets a value indicating whether this instance is at maximum level.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is at maximum level; otherwise, <c>false</c>.
        /// </value>
        public static bool IsAtMaxLevel => CurrentLevel == levels.Count;

        #endregion

        #region Methods

        /// <summary>
        ///     Creates the list of levels
        ///     Precondition: none
        ///     Post-condition: levels.Count == 3
        /// </summary>
        public static void CreateLevels()
        {
            levels = new List<Level>() {
                new LevelOne(),
                new LevelTwo(),
                new LevelThree()
            };
        }

        /// <summary>
        ///     Gets the current level.
        ///     Precondition: none
        ///     Post-condition: none
        /// </summary>
        /// <returns></returns>
        public static Level GetCurrentLevel()
        {
            return levels[CurrentLevel - 1];
        }

        #endregion
    }
}
