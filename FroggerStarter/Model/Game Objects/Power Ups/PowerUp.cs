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

            this.X = randomXGridValue * GameBoard.PlayerJumpRange;
            this.Y = randomYGridValue * GameBoard.PlayerJumpRange;
        }

        /// <summary>
        ///     Abstract method holder for power up activation/ability
        /// </summary>
        public abstract void activate();

        #endregion

        #region Private Helpers

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
            return (int)GameBoard.HighRoadYLocation;
        }

        #endregion
    }
}
