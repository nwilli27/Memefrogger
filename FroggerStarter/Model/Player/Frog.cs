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

        private bool canMove = true;
        private bool isDead;

        #endregion

        #region Constants

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
        public Animation.Animation DeathAnimation { get; }

        /// <summary>
        ///     Gets the frog leap animation.
        /// </summary>
        /// <value>
        ///     The frog leap animation.
        /// </value>
        public Animation.Animation FrogLeapAnimation { get; }

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
                this.canMove = !value;
                this.isDead = value;
            }
        }

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

            this.DeathAnimation = new Animation.Animation(AnimationType.PlayerDeath) {
                AnimationInterval = 500
            };
            this.FrogLeapAnimation = new Animation.Animation(AnimationType.FrogLeap) {
                AnimationInterval = 100
            };
            this.FrogLeapAnimation.AnimationFinished += this.onLeapFinished;
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
            var leftBoundary = 0;

            if (this.X - this.SpeedX >= leftBoundary && this.canMove)
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
            var rightBoundary = GameBoard.BackgroundWidth;

            if (this.X + this.SpeedX < rightBoundary && this.canMove)
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
            var topBoundary = GameBoard.HighRoadYLocation;

            if (this.Y - this.SpeedY >= topBoundary && this.canMove)
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
            var bottomBoundary = GameBoard.BottomRoadYLocation + GameBoard.RoadShoulderOffset;

            if (this.Y + this.SpeedY < bottomBoundary && this.canMove)
            {
                this.SpeedX = SpeedXDirection;
                this.MoveDown();
                this.Rotate(this.Direction = Direction.Down);
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
            this.ChangeSpriteVisibility(false);
            this.StopMovement();
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
        public void ResetAfterDeath()
        {
            this.ChangeSpriteVisibility(true);
            this.startMovement();
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
                    break;
            }
        }

        #endregion

        #region Private Helpers

        private void startMovement()
        {
            this.SpeedX = SpeedXDirection;
            this.SpeedY = SpeedYDirection;
        }

        private void playLeapAnimation()
        {
            this.Sprite.Visibility = Visibility.Collapsed;
            this.StopMovement();
            this.canMove = false;
            this.FrogLeapAnimation.RotateFrames(this.Direction);
            this.FrogLeapAnimation.SetFrameLocations(this.X, this.Y);
            this.FrogLeapAnimation.Start();
        }

        private void onLeapFinished(object sender, AnimationIsFinishedEventArgs e)
        {
            if (e.FrogLeapIsOver && !this.IsDead)
            {
                this.canMove = true;
                this.ChangeSpriteVisibility(true);
                this.startMovement();
            }
        }

        private void moveLeftPreventBoundary()
        {
            var leftBoundary = 0;

            if (this.X - this.SpeedX >= leftBoundary)
            {
                this.MoveLeft();
            }
        }

        private void moveRightPreventBoundary()
        {
            var rightBoundary = GameBoard.BackgroundWidth;

            if (this.X + this.Width + this.SpeedX < rightBoundary)
            {
                this.MoveRight();
            }
        }

        #endregion

    }

}