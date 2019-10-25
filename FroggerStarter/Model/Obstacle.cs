using System;
using Windows.Foundation;
using Windows.UI.Xaml.Media;
using FroggerStarter.Factory;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{
    /// <summary>
    ///     A Obstacle sprite object of type GameObject.
    /// </summary>
    /// <seealso cref="FroggerStarter.Model.GameObject" />
    internal class Obstacle : GameObject
    {
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
        public void MoveForward(double horizontalLaneWidth)
        {
            switch (this.direction)
            {
                case Direction.Left:
                    this.moveObstacleToTheLeft(horizontalLaneWidth);
                    break;

                case Direction.Right:
                    this.moveObstacleToTheRight(horizontalLaneWidth);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(this.direction), this.direction, null);
            }
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
    }
}
