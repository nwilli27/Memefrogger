
using FroggerStarter.Constants;
using FroggerStarter.Enums;
using FroggerStarter.Utility;

namespace FroggerStarter.Model.Game_Objects.Power_Ups
{
    /// <summary>
    ///     Holds the implementation for a generic power up
    /// </summary>
    public abstract class PowerUp : GameObject
    {

        #region Properties

        /// <summary>
        ///     The type of power up
        /// </summary>
        public PowerUpType Type { get; protected set; }

        /// <summary>
        ///     Gets or sets the number of occurrences.
        /// </summary>
        /// <value>
        ///     The number of occurrences.
        /// </value>
        public int NumberOfTimesActivated { get; set; }

        /// <summary>
        ///     Gets a value indicating whether this instance has been activated maximum number times.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has been activated maximum number times; otherwise, <c>false</c>.
        /// </value>
        public virtual bool HasBeenActivatedMaxNumberTimes => false;

        #endregion

        #region Constants

        private static readonly int TopStartingYPoint = (int)GameBoard.HighRoadYLocation + (int)GameBoard.RoadShoulderOffset;
        private static readonly int LowerEndingYPoint = (int)GameBoard.BottomRoadYLocation;
        private static readonly int TotalXGridSpaces = (int)GameBoard.BackgroundWidth / GameBoard.IndividualGridSize;
        private static readonly int TotalYGridSpaces = (LowerEndingYPoint + (int)GameBoard.RoadShoulderOffset - TopStartingYPoint) / GameBoard.IndividualGridSize;

        #endregion

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
        }

        /// <summary>
        ///     Moves the off board and makes the sprite invisible.
        ///     Precondition: none
        ///     Post-condition: this.X = -this.Width
        ///                     Sprite.Visibility = Collapsed
        /// </summary>
        public void MoveOffBoardAndMakeInvisible()
        {
            this.X = -this.Width;
            this.ChangeSpriteVisibility(false);
        }

        /// <summary>
        ///     Activates the power up ability.
        /// </summary>
        public virtual void Activate()
        {
            this.NumberOfTimesActivated++;
        }

        #endregion

        #region Private Helpers

        private void setRandomLocationOnBoard()
        {
            var randomXGridValue = Randomizer.GetRandomValueInRange(0, TotalXGridSpaces - 1);
            var randomYGridValue = Randomizer.GetRandomValueInRange(0, TotalYGridSpaces - 1);

            var yLocation = (randomYGridValue * GameBoard.IndividualGridSize) + TopStartingYPoint;
            var xLocation = randomXGridValue * GameBoard.IndividualGridSize;

            this.X = this.getCenterXLocation(xLocation);
            this.Y = this.getCenterYLocation(yLocation);
        }

        private double getCenterXLocation(double xLocation)
        {
            return ((GameBoard.IndividualGridSize - this.Width) / 2) + xLocation;
        }

        private double getCenterYLocation(double yLocation)
        {
            return ((GameBoard.IndividualGridSize - this.Height) / 2) + yLocation;
        }

        #endregion
    }

}
