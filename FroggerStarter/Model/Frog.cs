
using Windows.UI.Xaml;
using FroggerStarter.Enums;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{

    /// <summary>
    ///     Defines the frog model
    /// </summary>
    /// <seealso cref="FroggerStarter.Model.GameObject" />
    public sealed class Frog : MovingObject
    {
        #region Data Members

        public bool CanMove { get; set; } = true;

        public bool HasBeenCollidedWith { get; set; }

        #endregion

        #region Constants

        private const int SpeedXDirection = 50;
        private const int SpeedYDirection = 50;
        private const int DeathAnimationInterval = 500;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the death animation.
        /// </summary>
        /// <value>
        ///     The death animation.
        /// </value>
        public Animation DeathAnimation { get; }

        /// <summary>
        ///     Gets the frog leap animation.
        /// </summary>
        /// <value>
        ///     The frog leap animation.
        /// </value>
        public Animation FrogLeapAnimation { get; }

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

            this.DeathAnimation = new Animation(AnimationType.PlayerDeath, DeathAnimationInterval);
            this.FrogLeapAnimation = new Animation(AnimationType.FrogLeap, 100);
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
            if (this.X - this.SpeedX >= leftBoundary && this.CanMove)
            {
                this.MoveLeft();
                this.Direction = Direction.Left;
                this.Rotate(this.Direction);
                this.playLeapAnimation();
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
            if (this.X + this.SpeedX < rightBoundary && this.CanMove)
            {
                this.MoveRight();
                this.Direction = Direction.Right;
                this.Rotate(this.Direction);
                this.playLeapAnimation();
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
            if (this.Y - this.SpeedY >= topBoundary && this.CanMove)
            {
                this.MoveUp();
                this.Direction = Direction.Up;
                this.Rotate(this.Direction);
                this.playLeapAnimation();
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
            if (this.Y + this.SpeedY < bottomBoundary && this.CanMove)
            {
                this.MoveDown();
                this.Direction = Direction.Down;
                this.Rotate(this.Direction);
                this.playLeapAnimation();
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
            this.DeathAnimation.RotateFrames(this.Direction);
            this.DeathAnimation.SetFrameLocations(this.X, this.Y);
            this.DeathAnimation.Start();
        }

        /// <summary>
        ///     Starts the movement
        ///     Precondition: none
        ///     Post-condition: this.SpeedX = SpeedXDirection
        ///                     this.SpeedY = SpeedYDirection
        ///                     
        /// </summary>
        public void startMovement()
        {
            this.SpeedX = SpeedXDirection;
            this.SpeedY = SpeedYDirection;
        }

        #endregion

        #region Private Helpers

        private void playLeapAnimation()
        {
            this.Sprite.Visibility = Visibility.Collapsed;
            this.StopMovement();
            this.CanMove = false;
            this.FrogLeapAnimation.RotateFrames(this.Direction);
            this.FrogLeapAnimation.SetFrameLocations(this.X, this.Y);
            this.FrogLeapAnimation.Start();
        }

        #endregion

    }

}