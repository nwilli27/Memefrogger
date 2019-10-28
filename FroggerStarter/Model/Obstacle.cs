using System;
using System.Drawing;
using Windows.UI.Xaml.Media;
using FroggerStarter.Controller;
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

        /// <summary>
        ///     Gets or sets a value indicating whether the obstacle is active (moving).
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; } = false;

        #region Data Members

        private readonly Direction direction;
        
        #endregion

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
        }

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
                    this.moveObstacleToTheLeft(GameBoard.BackgroundWidth);
                    break;

                case Direction.Right:
                    this.moveObstacleToTheRight(GameBoard.BackgroundWidth);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(this.direction), this.direction, null);
            }
        }

        /// <summary>
        ///     Determines whether [is within x range on inverted lane side] from the otherGameObject.
        /// </summary>
        /// <param name="otherGameObject">The other game object.</param>
        /// <param name="xRange">The range on each side of the obstacle</param>
        /// <returns>
        ///   <c>true</c> if [is within x range on inverted lane side] [the specified other game object]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsWithinXRangeOnInvertedLaneSide(Obstacle otherGameObject, int xRange)
        {
            var otherObjectBoundary = new Rectangle(
                (int)otherGameObject.getInvertedXLocationOnLane(GameBoard.BackgroundWidth),
                (int)otherGameObject.Y,
                (int)otherGameObject.Width,
                (int)otherGameObject.Height
            );

            var thisObjectBoundaryWithRange = new Rectangle(
                addRangeToLocation((int) this.X, xRange),
                (int)this.Y,
                addRangeToLocation((int) this.Width, xRange),
                (int)this.Height
            );

            return thisObjectBoundaryWithRange.IntersectsWith(otherObjectBoundary);
        }

        /// <summary>
        ///     Adjusts the x location for obstacle by [amountOfSpace]
        /// </summary>
        /// <param name="amountOfSpace">The amount of space.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void AdjustXSpacingForObstacle(double amountOfSpace)
        {
            switch (this.direction)
            {
                case Direction.Left:
                    this.X += amountOfSpace;
                    break;

                case Direction.Right:
                    this.X -= amountOfSpace;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #region Private Helpers

        private static int addRangeToLocation(int location, int range)
        {
            return location < 0 ? location - range : location + range;
        }

        private double getInvertedXLocationOnLane(double horizontalLaneWidth)
        {
            switch (this.direction)
            {
                case Direction.Left:

                    if (this.isOffRightSideLaneBoundary(horizontalLaneWidth))
                    {
                        return this.X;
                    }
                    else if (this.isOffLeftSideLaneBoundary())
                    {
                        return horizontalLaneWidth - this.X;
                    }
                    else
                    {
                        return this.X + horizontalLaneWidth;
                    }

                case Direction.Right:

                    if (this.isOffLeftSideLaneBoundary())
                    {
                        return this.X;
                    }
                    else
                    {
                        return this.X - horizontalLaneWidth;
                    }

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private bool isOffLeftSideLaneBoundary()
        {
            return this.X <= 0;
        }

        private bool isOffRightSideLaneBoundary(double horizontalLaneWidth)
        {
            return this.X + this.Width >= horizontalLaneWidth;
        }

        private void moveObstacleToTheRight(double horizontalLaneWidth)
        {
            if (this.hasObstacleMovedOffRightSide(horizontalLaneWidth))
            {
                this.X = -this.Width;
            }
            this.MoveRight();
        }

        private void moveObstacleToTheLeft(double horizontalLaneWidth)
        {
            if (this.hasObstacleMovedOffLeftSide())
            {
                this.X = horizontalLaneWidth;
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
