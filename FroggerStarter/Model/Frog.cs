﻿using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{
    /// <summary>
    ///     Defines the frog model
    /// </summary>
    /// <seealso cref="FroggerStarter.Model.GameObject" />
    public class Frog : GameObject
    {
        #region Data members

        private const int SpeedXDirection = 50;
        private const int SpeedYDirection = 50;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Frog" /> class.
        /// </summary>
        public Frog()
        {
            Sprite = new FrogSprite();
            SetSpeed(SpeedXDirection, SpeedYDirection);
        }

        public void MoveLeftWithBoundaryCheck(double leftBoundary)
        {
            if (this.X - this.SpeedX >= leftBoundary)
            {
                this.MoveLeft();
            }
        }

        public void MoveRightWithBoundaryCheck(double rightBoundary)
        {
            if (this.X + this.SpeedX < rightBoundary)
            {
                this.MoveRight();
            }
        }

        public void MoveUpWithBoundaryCheck(double topBoundary)
        {
            if (this.Y - this.SpeedY >= topBoundary)
            {
                this.MoveUp();
            }
        }

        public void MoveDownWithBoundaryCheck(double bottomBoundary)
        {
            if (this.Y + this.SpeedY < bottomBoundary)
            {
                this.MoveDown();
            }
        }

        public void stopFrogMovement()
        {
            this.SpeedX = 0;
            this.SpeedY = 0;
        }
        
        #endregion
    }
}