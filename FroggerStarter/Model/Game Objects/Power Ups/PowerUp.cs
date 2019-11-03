
using FroggerStarter.Constants;
using FroggerStarter.Utility;

namespace FroggerStarter.Model.Game_Objects.Power_Ups
{
    /// <summary>
    ///     Holds the implementation for a generic power up
    /// </summary>
    internal abstract class PowerUp : GameObject
    {

        #region Methods

        /// <summary>
        ///     Sets the location and makes the sprite visible
        ///     Also setups the correlating ability.
        ///     Precondition: none
        ///     Post-condition: Sprite.Visibility = Visible
        ///                     this.X = random location
        ///                     this.Y = random location
        /// </summary>
        public void SetLocationAndMakeVisible()
        {
            this.ChangeSpriteVisibility(true);
            this.setRandomLocationOnBoard();
            this.SetupAbility();
        }

        /// <summary>
        ///     Moves the power up off the board and
        ///     makes the sprite invisible
        ///     Precondition: none
        ///     Post-condition: this.X = -this.Width
        ///                     Sprite.Visibility = Collapsed
        ///     
        /// </summary>
        public void MoveOffBoardAndMakeInvisible()
        {
            this.X = -this.Width;
            this.ChangeSpriteVisibility(false);
        }

        /// <summary>
        ///     Abstract method that allows subclasses to have different
        ///     implementation of setup 
        /// </summary>
        public abstract void SetupAbility();

        /// <summary>
        ///     Abstract method for power up activation/ability.
        /// </summary>
        public abstract void Activate();

        #endregion

        #region Private Helpers

        private void setRandomLocationOnBoard()
        {
            var randomXGridValue = Randomizer.GetRandomValueInRange(0, getTotalXJumpSpaces() - 1);
            var randomYGridValue = Randomizer.GetRandomValueInRange(0, getTotalYJumpSpaces() - 1);

            var yLocation = (randomYGridValue * GameBoard.PlayerJumpRange) + getTopStartingYPoint();
            var xLocation = randomXGridValue * GameBoard.PlayerJumpRange;

            this.X = this.getCenterXLocation(xLocation);
            this.Y = this.getCenterYLocation(yLocation);
        }

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
