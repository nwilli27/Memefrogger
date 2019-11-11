using System;
using FroggerStarter.Constants;
using FroggerStarter.Enums;

namespace FroggerStarter.Model.Game_Objects.Moving_Object
{

    /// <summary>
    ///     An obstacle sprite object of MovingObject.
    /// </summary>
    /// <seealso cref="MovingObject" />
    public abstract class Obstacle : MovingObject
    {
        #region Data Members

        private Direction direction;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the direction.
        ///     Flips the obstacle sprite according
        ///     to the direction.
        /// </summary>
        /// <value>
        ///     The direction.
        /// </value>
        public override Direction Direction
        {
            get => this.direction;
            set
            {
                this.direction = value;
                this.FlipSpriteHorizontally();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Flips the sprite horizontally.
        /// </summary>
        public override void FlipSpriteHorizontally()
        {
            if (this.Direction.Equals(Direction.Right))
            {
                base.FlipSpriteHorizontally();
            }
        }

        /// <summary>
        ///     Moves the obstacle forward depending on the given direction.
        ///     Precondition: none
        ///     Post-condition: @prev this.X +/- this.SpeedX
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">direction - null</exception>
        public virtual void MoveForward()
        {
            switch (this.Direction)
            {
                case Direction.Left:
                    this.moveObstacleToTheLeft();
                    break;

                case Direction.Right:
                    this.moveObstacleToTheRight();
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(this.Direction), this.Direction, null);
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
            switch (this.Direction)
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
            switch (this.Direction)
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
            switch (this.Direction)
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
        ///     Moves the obstacle to the right.
        /// </summary>
        protected abstract void moveObstacleToTheRight();

        /// <summary>
        ///     Moves the obstacle to the left.
        /// </summary>
        protected abstract void moveObstacleToTheLeft();

        /// <summary>
        ///     Determines whether [has obstacle moved off right side].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [has obstacle moved off right side]; otherwise, <c>false</c>.
        /// </returns>
        protected virtual bool hasObstacleMovedOffRightSide()
        {
            return this.X + this.SpeedX > GameBoard.BackgroundWidth;
        }

        /// <summary>
        ///     Determines whether [has obstacle moved off left side].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [has obstacle moved off left side]; otherwise, <c>false</c>.
        /// </returns>
        protected virtual bool hasObstacleMovedOffLeftSide()
        {
            return this.X + this.SpeedX < -this.Width;
        }

        #endregion

    }
}
