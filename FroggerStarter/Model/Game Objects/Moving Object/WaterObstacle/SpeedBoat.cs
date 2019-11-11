
using FroggerStarter.Constants;
using FroggerStarter.Enums;
using FroggerStarter.View.Sprites.WaterSprites;

namespace FroggerStarter.Model.Game_Objects.Moving_Object.WaterObstacle
{
    /// <summary>
    ///     A speed boat object of type Obstacle
    /// </summary>
    /// <seealso cref="WaterObstacle" />
    internal sealed class SpeedBoat : WaterObstacle
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the splash animation.
        /// </summary>
        /// <value>
        ///     The splash animation.
        /// </value>
        public Animation.Animation SplashAnimation { get; }

        #endregion

        #region Constants

        private const int SplashAnimationBaseSpeed = 300;
        private const int SplashAnimationSlowDownIncrement = 150;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="SpeedBoat"/> class.
        ///     Precondition: none
        ///     Post-condition: none
        /// </summary>
        public SpeedBoat()
        {
            this.Sprite = new SpeedBoatSprite();
            this.SplashAnimation = new Animation.Animation(AnimationType.SpeedBoatSplash);
        }

        /// <summary>
        ///     Moves the obstacle forward depending on the given direction.
        ///     Sets the animation for the splash animation each move forward.
        ///     Precondition: none
        ///     Post-condition: @prev this.X +/- this.SpeedX
        /// </summary>
        public override void MoveForward()
        {
            base.MoveForward();
            this.SplashAnimation.SetFrameLocations(this.X - (this.Width / 2), this.Y);
        }

        /// <summary>
        ///     Starts the speed boat water animation on endless loop.
        ///     Precondition: none
        ///     Post-condition: SplashAnimation.Interval = 300 / SpeedX
        /// </summary>
        public void StartSpeedBoatWaterAnimation()
        {
            this.SplashAnimation.AnimationInterval = SplashAnimationBaseSpeed / (int) this.SpeedX;
            this.SplashAnimation.StartEndlessLoopKeepBaseFrame();
        }

        /// <summary>
        ///     Stops the and slow down speed boat splash animation.
        ///     Precondition: none
        ///     Post-condition: SplashAnimation.AnimationInterval += SplashAnimationSlowDownIncrement
        /// </summary>
        public void SlowDownSpeedBoatSplashAnimation()
        {
            this.StopSpeedBoatAnimations();
            this.SplashAnimation.AnimationInterval += SplashAnimationSlowDownIncrement;
            this.SplashAnimation.StartEndlessLoopKeepBaseFrame();
        }

        /// <summary>
        /// Stops the speed boat animations timer.
        /// </summary>
        public void StopSpeedBoatAnimations()
        {
            this.SplashAnimation.Stop();
        }

        /// <summary>
        ///     Determines whether [has obstacle moved off right side].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [has obstacle moved off right side]; otherwise, <c>false</c>.
        /// </returns>
        protected override bool hasObstacleMovedOffRightSide()
        {
            return this.X + this.SpeedX > GameBoard.BackgroundWidth + this.Width / 2;
        }

        /// <summary>
        ///     Determines whether [has obstacle moved off left side].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [has obstacle moved off left side]; otherwise, <c>false</c>.
        /// </returns>
        protected override bool hasObstacleMovedOffLeftSide()
        {
            return this.X + this.SpeedX < -this.Width - this.Width / 2;
        }

        #endregion
    }
}
