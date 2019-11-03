using System;
using FroggerStarter.Constants;
using FroggerStarter.Utility;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model.Game_Objects.Power_Ups
{
    /// <summary>
    ///     Holds the implementation for a generic power up
    /// </summary>
    internal abstract class PowerUp : GameObject
    {
        #region Data Members

        private BaseSprite sprite;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the sprite associated with the game object.
        /// </summary>
        /// <value>
        ///     The sprite.
        /// </value>
        public override BaseSprite Sprite
        {
            get => this.sprite;
            protected set
            {
                this.sprite = value;
                this.ChangeSpriteVisibility(false);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Sets a random X and Y location based on a grid layout
        ///     (RandomLaneNumber = Y, RandomXJumpNumber = X)
        ///     Precondition: none
        ///     Post-condition: this.X = randomX
        ///                     this.Y = randomY
        /// </summary>
        public void setRandomLocationOnBoard()
        {
            var randomXGridValue = Randomizer.getRandomValueInRange(0, getTotalXJumpSpaces());
            var randomYGridValue = Randomizer.getRandomValueInRange(0, getTotalYJumpSpaces());

            var yLocation = (randomYGridValue * GameBoard.PlayerJumpRange) + getTopStartingYPoint();
            var xLocation = randomXGridValue * GameBoard.PlayerJumpRange;

            this.X = this.getCenterXLocation(xLocation); 
            this.Y = this.getCenterYLocation(yLocation);
        }

        /// <summary>
        ///     Abstract method holder for power up activation/ability
        /// </summary>
        public abstract void activate();

        #endregion

        #region Private Helpers

        private double getCenterXLocation(double xLocation)
        {
            return ((GameBoard.PlayerJumpRange - this.Width) / 2) + xLocation;
        }

        private double getCenterYLocation(double yLocation)
        {
            return ((GameBoard.PlayerJumpRange - this.Height) / 2) + yLocation;
        }

        private static int getTotalYJumpSpaces()
        {
            //TODO do something with this 50
            return ((getLowerEndingYPoint() + (int) GameBoard.RoadShoulderOffset) - getTopStartingYPoint()) / GameBoard.PlayerJumpRange;
        }

        private static int getTotalXJumpSpaces()
        {
            //TODO do something with 50
            return (int)GameBoard.BackgroundWidth / GameBoard.PlayerJumpRange;
        }

        private static int getLowerEndingYPoint()
        {
            //TODO this will change once water is introduced
            return (int)GameBoard.BottomRoadYLocation;
        }

        private static int getTopStartingYPoint()
        {
            //TODO this will change once water is introduced
            return (int)GameBoard.HighRoadYLocation + (int) GameBoard.RoadShoulderOffset;
        }

        #endregion
    }

    /// <summary>
    ///     Holds the event for finished animation.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class PowerUpTimeAssigned : EventArgs
    {
        /// <summary>
        ///     Gets or sets the power up time.
        /// </summary>
        /// <value>
        ///     The power up time.
        /// </value>
        public int PowerUpTime { get; set; }
    }
}
