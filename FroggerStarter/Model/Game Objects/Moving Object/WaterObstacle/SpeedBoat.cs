
using FroggerStarter.Constants;
using FroggerStarter.Enums;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model.Game_Objects.Moving_Object.WaterObstacle
{
    /// <summary>
    ///     A speed boat object of type Obstacle
    /// </summary>
    /// <seealso cref="WaterObstacle" />
    internal sealed class SpeedBoat : WaterObstacle
    {
        #region MyRegion

        /// <summary>
        ///     The splash animation
        /// </summary>
        public Animation.Animation SplashAnimation { get; set; }

        #endregion

        #region Constants

        private const int SplashAnimationBaseSpeed = 300;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="SpeedBoat"/> class.
        ///     Precondition: none
        ///     Post-condition: none
        /// </summary>
        /// <param name="direction">The direction the boat is facing.</param>
        public SpeedBoat(Direction direction) : base(direction)
        {
            this.Sprite = new SpeedBoatSprite();
            this.SplashAnimation = new Animation.Animation(AnimationType.SpeedBoatSplash);
            this.ObstacleType = ObstacleType.SpeedBoat;
        }

        /// <summary>
        ///     Moves the obstacle forward depending on the given direction.
        ///     Precondition: none
        ///     Post-condition: @prev this.X +/- this.SpeedX
        /// </summary>
        public override void MoveForward()
        {
            base.MoveForward();
            this.SplashAnimation.SetFrameLocations(this.X - (this.Width / 2), this.Y);
        }

        /// <summary>
        ///     Starts the speed boat water animation.
        ///     Precondition: none
        ///     Post-condition: SplashAnimation.Interval = 300 / SpeedX
        /// </summary>
        public void StartSpeedBoatWaterAnimation()
        {
            this.SplashAnimation.AnimationInterval = SplashAnimationBaseSpeed / (int) this.SpeedX;
            this.SplashAnimation.StartEndlessLoopKeepBaseFrame();
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
