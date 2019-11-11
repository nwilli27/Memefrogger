using System;
using Windows.UI.Xaml;
using FroggerStarter.Enums;
using FroggerStarter.Model.Animation;
using FroggerStarter.Model.Game_Objects;
using FroggerStarter.Model.Game_Objects.Moving_Object;
using FroggerStarter.View.Sprites;
using FroggerStarter.Constants;

namespace FroggerStarter.Model.Player
{

    /// <summary>
    ///     Defines the frog model
    /// </summary>
    /// <seealso cref="GameObject" />
    public sealed class Frog : MovingObject
    {
        #region Data Members

        private bool isDead;

        #endregion

        #region Constants

        private const int SpeedXDirection = 50;
        private const int SpeedYDirection = 50;

        private static readonly double TopBoundary = GameBoard.HighRoadYLocation;
        private static readonly double BottomBoundary = GameBoard.BottomRoadYLocation + GameBoard.RoadShoulderOffset;
        private static readonly double RightBoundary = GameBoard.BackgroundWidth;
        private const double LeftBoundary = 0;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the death animation.
        /// </summary>
        /// <value>
        ///     The death animation.
        /// </value>
        public Animation.Animation DeathAnimation { get; private set; }

        /// <summary>
        ///     Gets the frog leap animation.
        /// </summary>
        /// <value>
        ///     The frog leap animation.
        /// </value>
        public Animation.Animation FrogLeapAnimation { get; private set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance has collided.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has collided; otherwise, <c>false</c>.
        /// </value>
        public bool IsDead
        {
            get => this.isDead;
            set
            {
                this.CanMove = !value;
                this.isDead = value;
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance can move.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can move; otherwise, <c>false</c>.
        /// </value>
        public bool CanMove { get; set; } = true;

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

            this.setupAnimations();
            this.Direction = Direction.Up;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Move to the left with boundary check of left side
        ///     Precondition: none
        ///     Post-condition: this.X =- this.SpeedX
        /// </summary>
        public void MoveLeftWithBoundaryCheck()
        {
            if (this.X - this.SpeedX >= LeftBoundary && this.CanMove && !GameSettings.PauseGame)
            {
                this.SpeedX = SpeedXDirection;
                this.MoveLeft();
                this.Rotate(this.Direction = Direction.Left);
                this.playLeapAnimation();
            }
        }

        /// <summary>
        ///     Moves to the right with boundary check of right side
        ///     Precondition: none
        ///     Post-condition: this.X += this.SpeedX
        /// </summary>
        public void MoveRightWithBoundaryCheck()
        {
            if (this.X + this.SpeedX < RightBoundary && this.CanMove && !GameSettings.PauseGame)
            {
                this.SpeedX = SpeedXDirection;
                this.MoveRight();
                this.Rotate(this.Direction = Direction.Right);
                this.playLeapAnimation();
            }
        }

        /// <summary>
        ///     Moves up with boundary check of top side
        ///     Precondition: none
        ///     Post-condition: this.Y =- this.SpeedY
        /// </summary>
        public void MoveUpWithBoundaryCheck()
        {
            if (this.Y - this.SpeedY >= TopBoundary && this.CanMove && !GameSettings.PauseGame)
            {
                this.SpeedX = SpeedXDirection;
                this.MoveUp();
                this.Rotate(this.Direction = Direction.Up);
                this.playLeapAnimation();
            }
        }

        /// <summary>
        ///     Moves down with boundary check of bottom side
        ///     Precondition: none
        ///     Post-condition: this.Y += this.SpeedY
        /// </summary>
        public void MoveDownWithBoundaryCheck()
        {
            if (this.Y + this.SpeedY < BottomBoundary && this.CanMove && !GameSettings.PauseGame)
            {
                this.SpeedX = SpeedXDirection;
                this.MoveDown();
                this.Rotate(this.Direction = Direction.Down);
                this.playLeapAnimation();
            }
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
            this.ChangeSpriteVisibility(false);
            this.CanMove = false;
            this.IsDead = true;

            this.DeathAnimation.RotateFrames(this.Direction);
            this.DeathAnimation.SetFrameLocations(this.X, this.Y);
            this.DeathAnimation.Start();
        }

        /// <summary>
        ///     Resets the player after the death animation is finished.
        ///     Precondition: none
        ///     Post-condition: Sprite.Visibility = Visible
        ///                     Direction = Up
        ///                     this.IsDead = false
        /// </summary>
        public void ResetVisibilityAndMovement()
        {
            this.ChangeSpriteVisibility(true);
            this.CanMove = true;
            this.Direction = Direction.Up;
            this.IsDead = false;
        }

        /// <summary>
        ///     Moves the player with given game object
        ///     speedX and the correlating direction.
        /// </summary>
        /// <param name="obstacle">The obstacle.</param>
        public void MovePlayerWithObstacle(Obstacle obstacle)
        {
            this.SpeedX = obstacle.SpeedX;

            switch (obstacle.Direction)
            {
                case Direction.Left:
                    this.moveLeftPreventBoundary();
                    break;

                case Direction.Right:
                    this.moveRightPreventBoundary();
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion

        #region Private Helpers

        private void setupAnimations()
        {
            this.DeathAnimation = new Animation.Animation(AnimationType.PlayerDeath)
            {
                AnimationInterval = 500
            };
            this.FrogLeapAnimation = new Animation.Animation(AnimationType.FrogLeap)
            {
                AnimationInterval = 100
            };
            this.FrogLeapAnimation.AnimationFinished += this.onLeapFinished;
        }

        private void playLeapAnimation()
        {
            this.Sprite.Visibility = Visibility.Collapsed;
            this.CanMove = false;
           
            this.FrogLeapAnimation.RotateFrames(this.Direction);
            this.FrogLeapAnimation.SetFrameLocations(this.X, this.Y);
            this.FrogLeapAnimation.Start();
        }

        private void onLeapFinished(object sender, AnimationIsFinishedEventArgs e)
        {
            if (e.FrogLeapIsOver && !this.IsDead)
            {
                this.ChangeSpriteVisibility(true);
                this.CanMove = true;
            }
        }

        private void moveLeftPreventBoundary()
        {
            if (this.X - this.SpeedX >= LeftBoundary)
            {
                this.MoveLeft();
            }
        }

        private void moveRightPreventBoundary()
        {
            if (this.X + this.Width + this.SpeedX < RightBoundary)
            {
                this.MoveRight();
            }
        }

        #endregion

    }

}