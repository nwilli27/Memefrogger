using System;
using Windows.UI.Xaml.Media;
using FroggerStarter.Constants;
using FroggerStarter.Enums;
using FroggerStarter.Factory;
using Point = Windows.Foundation.Point;

namespace FroggerStarter.Model
{
    /// <summary>
    ///     A Obstacle sprite object of type GameObject.
    /// </summary>
    /// <seealso cref="FroggerStarter.Model.GameObject" />
    internal class Obstacle : MovingObject
    {

        #region Data Members

        private readonly Direction direction;

        #endregion

        #region Constants

        private const int SpawnLocationOffset = 5;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets a value indicating whether the obstacle is active (moving).
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; } = false;

        #endregion

        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="Obstacle"/> class.
        ///     Creates a Game object sprite according to the type of obstacle passed in.
        ///     Precondition: none
        ///     Post-condition: none
        /// </summary>
        /// <param name="obstacleType">Type of obstacle to create.</param>
        /// <param name="direction">The direction the vehicle is facing.</param>
        public Obstacle(ObstacleType obstacleType, Direction direction)
        {
            Sprite = ObstacleFactory.CreateObstacleSprite(obstacleType);
            this.direction = direction;
            this.checkDirectionToFlipHorizontally();
            this.MoveToDefaultLocation();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Moves the obstacle forward depending on the given direction.
        ///     Precondition: none
        ///     Post-condition: @prev this.X +/- this.SpeedX
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">direction - null</exception>
        public void MoveForward()
        {
            switch (this.direction)
            {
                case Direction.Left:
                    this.moveObstacleToTheLeft();
                    break;

                case Direction.Right:
                    this.moveObstacleToTheRight(GameBoard.BackgroundWidth);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(this.direction), this.direction, null);
            }
        }

        /// <summary>
        ///     Determines whether [is out of bounds].
        ///     Precondition: none
        ///     Post-condition: none
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is off the end of the lane]; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public bool IsOutOfBounds()
        {
            switch (this.direction)
            {
                case Direction.Right:
                    return this.X + this.Width < 0;

                case Direction.Left:
                    return this.X > GameBoard.BackgroundWidth;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        ///     Adjusts the x location backwards by the width of the obstacle.
        ///     Precondition: none
        ///     Post-condition: this.X +/- this.Width
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void ShiftXBackwardsByWidth()
        {
            switch (this.direction)
            {
                case Direction.Left:
                    this.X += this.Width;
                    break;

                case Direction.Right:
                    this.X -= this.Width;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        ///     Shifts the x location forward by [amountToShift]
        ///     Precondition: none
        ///     Post-condition: this.X +/- [amountToShift]
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void ShiftXForward(double amountToShift)
        {
            switch (this.direction)
            {
                case Direction.Left:
                    this.X -= amountToShift;
                    break;

                case Direction.Right:
                    this.X += amountToShift;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        ///     Moves the X location to the default spawn location.
        ///     Precondition: none
        ///     Post-condition: this.X == SpawnLocation [based on direction]
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void MoveToDefaultLocation()
        {
            switch (this.direction)
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

        #region Private Helpers

        private void moveObstacleToTheRight(double horizontalLaneWidth)
        {
            if (this.hasObstacleMovedOffRightSide(horizontalLaneWidth))
            {
                this.X = -this.Width * 2;
            }
            this.MoveRight();
        }

        private void moveObstacleToTheLeft()
        {
            if (this.hasObstacleMovedOffLeftSide())
            {
                this.X = GameBoard.BackgroundWidth + this.Width;
            }
            this.MoveLeft();
        }

        private bool hasObstacleMovedOffRightSide(double horizontalLaneWidth)
        {
            return this.X + this.SpeedX > horizontalLaneWidth;
        }

        private bool hasObstacleMovedOffLeftSide()
        {
            return this.X + this.SpeedX < -this.Width;
        }

        private void checkDirectionToFlipHorizontally()
        {
            if (this.direction.Equals(Direction.Right))
            {
                this.Sprite.RenderTransformOrigin = new Point(0.5, 0.5);
                this.Sprite.RenderTransform = new ScaleTransform() { ScaleX = -1 };
            }
        }

        #endregion

    }
}
