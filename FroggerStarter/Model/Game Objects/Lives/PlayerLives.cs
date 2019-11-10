﻿
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FroggerStarter.Constants;
using FroggerStarter.Model.Player;

namespace FroggerStarter.Model.Game_Objects.Lives
{
    /// <summary>
    ///     Holds the collection of Player hearts (LifeHearts)
    /// </summary>
    internal class PlayerLives : IEnumerable<LifeHeart>
    {

        #region Data Members

        private readonly IList<LifeHeart> hearts;
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
            get
            {
                if (PlayerAbilities.HasQuickRevive)
                {
                    return this.numberOfHearts + 1;
                }
                return this.numberOfHearts;
            }
            set
            {
                if (value < GameSettings.TotalNumberOfLives || PlayerAbilities.HasQuickRevive)
                {
                    this.makeRightMostHeartInvisible();
                }
                this.numberOfHearts = value;
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
        ///     Post-condition: this.hearts.Count() == 0
        /// </summary>
        public PlayerLives()
        {
            this.hearts = new List<LifeHeart>();
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
            return this.hearts.GetEnumerator();
        }

        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.hearts.GetEnumerator();
        }

        public void MoveQuickReviveHeart()
        {
            var quickReviveHeart = this.hearts.First(heart => heart is QuickReviveHeart);
            var rightMostHeartXLocation = this.hearts.OrderByDescending(heart => heart.X).First(heart => heart is SingleLifeHeart).X;
            quickReviveHeart.X = rightMostHeartXLocation + quickReviveHeart.Width + SpacingBetweenLifeHearts;
        }

        #endregion

        #region Private Helpers

        private void createAndLoadLives()
        {
            var xLocation = LeftBorderOffset;

            for (var i = 0; i < GameSettings.TotalNumberOfLives; i++)
            {
                var newLife = new SingleLifeHeart() {
                    X = xLocation
                };
                xLocation += newLife.Width + SpacingBetweenLifeHearts;
                newLife.SetCenteredYLocationOfArea(GameBoard.StatHudBottomYLocation, 0);
                this.hearts.Add(newLife);
            }

            this.addQuickReviveHeartToEnd();
        }

        private void addQuickReviveHeartToEnd()
        {
            var quickReviveHeart = new QuickReviveHeart();
            var rightMostHeartXLocation = this.hearts.OrderByDescending(heart => heart.X).First().X;
            quickReviveHeart.X = rightMostHeartXLocation + quickReviveHeart.Width + SpacingBetweenLifeHearts;
            quickReviveHeart.SetCenteredYLocationOfArea(GameBoard.StatHudBottomYLocation, 0);
            quickReviveHeart.ChangeSpriteVisibility(false);
            this.hearts.Add(quickReviveHeart);
        }

        private void makeRightMostHeartInvisible()
        {
            var rightMostHeart = this.hearts.OrderByDescending(heart => heart.X).First(heart => heart is SingleLifeHeart);

            if (PlayerAbilities.HasQuickRevive)
            {
                rightMostHeart = this.hearts.First(heart => heart is QuickReviveHeart);
                rightMostHeart.ChangeSpriteVisibility(false);
            }
            else
            {
                rightMostHeart.ChangeSpriteVisibility(false);
                this.hearts.Remove(rightMostHeart);
            }

            rightMostHeart.HeartLostAnimation.SetFrameLocations(rightMostHeart.X, rightMostHeart.Y);
            rightMostHeart.HeartLostAnimation.Start();
        }

        #endregion

    }
}
