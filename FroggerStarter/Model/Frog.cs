﻿
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using FroggerStarter.Enums;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{

    /// <summary>
    ///     Defines the frog model
    /// </summary>
    /// <seealso cref="FroggerStarter.Model.GameObject" />
    public class Frog : MovingObject
    {
        #region Data members

        private const int SpeedXDirection = 50;
        private const int SpeedYDirection = 50;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the death animation.
        /// </summary>
        /// <value>
        ///     The death animation.
        /// </value>
        public Animation DeathAnimation { get; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Frog" /> class.
        ///     Precondition: none
        ///     Post-condition: frog.SpeedX = SpeedXDirection
        ///                     frog.SpeedY = SpeedYDirection
        ///                     
        /// </summary>
        public Frog()
        {
            Sprite = new FrogSprite();

            this.SpeedX = SpeedXDirection;
            this.SpeedY = SpeedYDirection;

            this.DeathAnimation = new Animation(AnimationType.PlayerDeath);
            this.DeathAnimation.AnimationFinished += this.onDeathAnimationDone;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Move to the left with boundary check of left side
        ///     Precondition: none
        ///     Post-condition: this.X =- this.SpeedX
        /// </summary>
        /// <param name="leftBoundary">The left boundary.</param>
        public void MoveLeftWithBoundaryCheck(double leftBoundary)
        {
            if (this.X - this.SpeedX >= leftBoundary)
            {
                this.MoveLeft();
            }
        }

        /// <summary>
        ///     Moves to the right with boundary check of right side
        ///     Precondition: none
        ///     Post-condition: this.X += this.SpeedX
        /// </summary>
        /// <param name="rightBoundary">The right boundary.</param>
        public void MoveRightWithBoundaryCheck(double rightBoundary)
        {
            if (this.X + this.SpeedX < rightBoundary)
            {
                this.MoveRight();
            }
        }

        /// <summary>
        ///     Moves up with boundary check of top side
        ///     Precondition: none
        ///     Post-condition: this.Y =- this.SpeedY
        /// </summary>
        /// <param name="topBoundary">The top boundary.</param>
        public void MoveUpWithBoundaryCheck(double topBoundary)
        {
            if (this.Y - this.SpeedY >= topBoundary)
            {
                this.MoveUp();
            }
        }

        /// <summary>
        ///     Moves down with boundary check of bottom side
        ///     Precondition: none
        ///     Post-condition: this.Y += this.SpeedY
        /// </summary>
        /// <param name="bottomBoundary">The bottom boundary.</param>
        public void MoveDownWithBoundaryCheck(double bottomBoundary)
        {
            if (this.Y + this.SpeedY < bottomBoundary)
            {
                this.MoveDown();
            }
        }

        /// <summary>
        ///     Stops the movement of the game object by setting its speed to 0.
        ///     Precondition: none
        ///     Post-condition: SpeedX == 0
        ///                     SpeedY == 0
        /// </summary>
        public void StopMovement()
        {
            this.SpeedX = 0;
            this.SpeedY = 0;
        }

        /// <summary>
        ///     Plays the death animation.
        ///     Precondition: none
        ///     Post-condition: this.SpeedX = 0
        ///                     this.SpeedY = 0
        ///                     Sprite.Visibility = Collapsed
        ///                     DeathAnimation.Start()
        /// </summary>
        public void PlayDeathAnimation()
        {
            this.Sprite.Visibility = Visibility.Collapsed;
            this.StopMovement();
            this.DeathAnimation.SetFrameLocations(this.X, this.Y);
            this.DeathAnimation.Start();
        }

        #endregion

        #region Private Helpers

        private void startMovement()
        {
            this.SpeedX = SpeedXDirection;
            this.SpeedY = SpeedYDirection;
        }

        private void onDeathAnimationDone(object sender, AnimationIsFinishedEventArgs e)
        {
            if (e.AnimationIsOver)
            {
                this.Sprite.Visibility = Visibility.Visible;
                this.startMovement();
            }
        }

        #endregion
    }

}