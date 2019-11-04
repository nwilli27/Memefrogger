using FroggerStarter.Enums;

namespace FroggerStarter.Model.Game_Objects.Moving_Object
{
    /// <summary>
    ///     Type of GameObject that allows movement.
    /// </summary>
    /// <seealso cref="GameObject" />
    public abstract class MovingObject : GameObject
    {

        #region Properties

        /// <summary>
        ///     Gets the x speed of the game object.
        /// </summary>
        /// <value>
        ///     The speed x.
        /// </value>
        public double SpeedX { get; set; }

        /// <summary>
        ///     Gets the y speed of the game object.
        /// </summary>
        /// <value>
        ///     The speed y.
        /// </value>
        public double SpeedY { get; set; }

        /// <summary>
        ///     Gets or sets the direction.
        /// </summary>
        /// <value>
        ///     The direction.
        /// </value>
        public Direction Direction { get; set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Moves the game object right.
        ///     Precondition: None
        ///     Post-condition: X == X@prev + SpeedX
        /// </summary>
        public void MoveRight()
        {
            this.moveX(this.SpeedX);
            this.Direction = Direction.Right;
        }

        /// <summary>
        ///     Moves the game object left.
        ///     Precondition: None
        ///     Post-condition: X == X@prev + SpeedX
        /// </summary>
        public void MoveLeft()
        {
            this.moveX(-this.SpeedX);
            this.Direction = Direction.Left;
        }

        /// <summary>
        ///     Moves the game object up.
        ///     Precondition: None
        ///     Post-condition: Y == Y@prev - SpeedY
        /// </summary>
        public virtual void MoveUp()
        {
            this.moveY(-this.SpeedY);
            this.Direction = Direction.Up;
        }

        /// <summary>
        ///     Moves the game object down.
        ///     Precondition: None
        ///     Post-condition: Y == Y@prev + SpeedY
        /// </summary>
        public void MoveDown()
        {
            this.moveY(this.SpeedY);
            this.Direction = Direction.Down;
        }

        #endregion

        #region Private Helpers

        private void moveX(double x)
        {
            this.X += x;
        }

        private void moveY(double y)
        {
            this.Y += y;
        }

        #endregion
    }
}
