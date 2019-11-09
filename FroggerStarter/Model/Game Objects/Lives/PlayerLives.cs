
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FroggerStarter.Constants;

namespace FroggerStarter.Model.Game_Objects.Lives
{
    /// <summary>
    ///     Holds the collection of Player lives (LifeHearts)
    /// </summary>
    internal class PlayerLives : IEnumerable<LifeHeart>
    {

        #region Data Members

        private readonly IList<LifeHeart> lives;
        private int numberOfHearts;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the number of hearts.
        /// </summary>
        /// <value>
        ///     The number of hearts.
        /// </value>
        public int NumberOfHearts
        {
            get => this.numberOfHearts;
            set
            {
                this.numberOfHearts = value;
                if (value < GameSettings.TotalNumberOfLives)
                {
                    this.makeRightMostHeartInvisible();
                }
            }
        }

        #endregion

        #region Constants

        private const int SpacingBetweenLifeHearts = 5;
        private const double LeftBorderOffset = 10;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PlayerLives"/> class.
        ///     Precondition: none
        ///     Post-condition: this.lives.Count() == 0
        /// </summary>
        public PlayerLives()
        {
            this.lives = new List<LifeHeart>();
            this.createAndLoadLives();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        ///     An enumerator that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<LifeHeart> GetEnumerator()
        {
            return this.lives.GetEnumerator();
        }

        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.lives.GetEnumerator();
        }

        #endregion

        #region Private Helpers

        private void createAndLoadLives()
        {
            var xLocation = LeftBorderOffset;

            for (var i = 0; i < GameSettings.TotalNumberOfLives; i++)
            {
                var newLife = new LifeHeart() {
                    X = xLocation
                };
                xLocation += newLife.Width + SpacingBetweenLifeHearts;
                newLife.SetCenteredYLocationOfArea(GameBoard.StatHudBottomYLocation, 0);
                this.lives.Add(newLife);
            }
        }

        private void makeRightMostHeartInvisible()
        {
            var rightMostHeart = this.lives.OrderByDescending(heart => heart.X).First();
            rightMostHeart.ChangeSpriteVisibility(false);
            rightMostHeart.HeartLostAnimation.SetFrameLocations(rightMostHeart.X, rightMostHeart.Y);
            rightMostHeart.HeartLostAnimation.Start();
            this.lives.Remove(rightMostHeart);
        }

        #endregion

    }
}
