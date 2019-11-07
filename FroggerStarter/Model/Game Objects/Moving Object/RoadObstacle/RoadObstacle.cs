
using FroggerStarter.Constants;
using FroggerStarter.Enums;
using System;

namespace FroggerStarter.Model.Game_Objects.Moving_Object
{
    /// <summary>
    ///     An obstacle that is a RoadObstacle
    /// </summary>
    /// <seealso cref="WaterObstacle" />
    public abstract class RoadObstacle : Obstacle
    {
        #region Properties

        /// <summary>
        ///     Gets or sets a value indicating whether the obstacle is active (moving).
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }

        #endregion

        #region Constants

        private const int SpawnLocationOffset = 5;

        #endregion

        #region Constructor 

        /// <summary>
        ///     Initializes a new instance of the <see cref="RoadObstacle"/> class.
        /// </summary>
        /// <param name="direction">The direction the vehicle is facing.</param>
        public RoadObstacle(Direction direction) : base(direction) {}

        #endregion

        #region Methods

        /// <summary>
        ///     Moves the X location to the default spawn location.
        ///     Precondition: none
        ///     Post-condition: this.X == SpawnLocation [based on direction]
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void MoveToDefaultLocation()
        {
            switch (this.Direction)
            {
                case Direction.Left:
                    this.X = GameBoard.BackgroundWidth + SpawnLocationOffset;
                    break;

                case Direction.Right:
                    this.X = -this.Width - SpawnLocationOffset;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion
    }
}
