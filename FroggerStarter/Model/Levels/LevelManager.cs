using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using FroggerStarter.Model.Animation;
using FroggerStarter.Model.Levels.Levels;

namespace FroggerStarter.Model.Levels
{
    /// <summary>
    ///     Class responsible for managing multiple levels
    /// </summary>
    internal class LevelManager
    {
        #region Data Members

        //TODO could maybe use indexer here
        private readonly IList<Level> levels;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the current level.
        /// </summary>
        /// <value>
        ///     The current level.
        /// </value>
        public int CurrentLevel { get; set; } = 1;

        /// <summary>
        ///     Gets a value indicating whether this instance is at maximum level.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is at maximum level; otherwise, <c>false</c>.
        /// </value>
        public bool IsAtMaxLevel => this.CurrentLevel == this.levels.Count;

        #endregion

        #region

        private const int NextLevelTimerInterval = 5;

        #endregion

        #region Constructors

        public LevelManager()
        {
            this.levels = new List<Level>() {
                new LevelOne(),
                new LevelTwo(),
                new LevelThree()
            };

        }

        #endregion

        /// <summary>
        ///     Gets the current level.
        ///     Precondition: none
        ///     Post-condition: none
        /// </summary>
        /// <returns></returns>
        public Level GetCurrentLevel()
        {
            return this.levels[this.CurrentLevel - 1];
        }
    }
}
