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
            var randomXGridValue = Randomizer.getRandomValueInRange(0, getTotalXJumpSpaces() - 1);
            var randomYGridValue = Randomizer.getRandomValueInRange(0, getTotalYJumpSpaces() - 1);

            var yLocation = (randomYGridValue * GameBoard.PlayerJumpRange) + getTopStartingYPoint();
            var xLocation = randomXGridValue * GameBoard.PlayerJumpRange;

            this.X = this.getCenterXLocation(xLocation); 
            this.Y = this.getCenterYLocation(yLocation);
        }

        /// <summary>
        ///     Moves the power up off the board and
        ///     makes the sprite invisible
        ///     Precondition: none
        ///     Post-condition: this.X = -this.Width
        ///                     Sprite.Visibility == Visibility.Collapsed
        ///     
        /// </summary>
        public void MoveOffBoardAndMakeInvisible()
        {
            this.X = -this.Width;
            this.ChangeSpriteVisibility(false);
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
            return ((getLowerEndingYPoint() + (int) GameBoard.RoadShoulderOffset) - getTopStartingYPoint()) / GameBoard.PlayerJumpRange;
        }

        private static int getTotalXJumpSpaces()
        {
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

}
