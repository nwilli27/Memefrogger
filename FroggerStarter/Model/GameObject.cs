using System;
using Windows.Foundation;
using Windows.UI.Xaml.Media;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{
    /// <summary>
    ///     Defines basic properties and behavior of every game object.
    /// </summary>
    public abstract class GameObject
    {
        #region Data members

        private Point location;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the x location of the game object.
        /// </summary>
        /// <value>
        ///     The x.
        /// </value>
        public double X
        {
            get => this.location.X;
            set
            {
                this.location.X = value;
                this.render();
            }
        }

        /// <summary>
        ///     Gets or sets the y location of the game object.
        /// </summary>
        /// <value>
        ///     The y.
        /// </value>
        public double Y
        {
            get => this.location.Y;
            set
            {
                this.location.Y = value;
                this.render();
            }
        }

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
        ///     Gets the width of the game object.
        /// </summary>
        /// <value>
        ///     The width.
        /// </value>
        public double Width => this.Sprite.Width;

        /// <summary>
        ///     Gets the height of the game object.
        /// </summary>
        /// <value>
        ///     The height.
        /// </value>
        public double Height => this.Sprite.Height;

        /// <summary>
        ///     Gets or sets the sprite associated with the game object.
        /// </summary>
        /// <value>
        ///     The sprite.
        /// </value>
        public BaseSprite Sprite { get; protected set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Moves the game object right.
        ///     Precondition: None
        ///     Postcondition: X == X@prev + SpeedX
        /// </summary>
        public void MoveRight()
        {
            this.moveX(this.SpeedX);
        }

        /// <summary>
        ///     Moves the game object left.
        ///     Precondition: None
        ///     Postcondition: X == X@prev + SpeedX
        /// </summary>
        public void MoveLeft()
        {
            this.moveX(-this.SpeedX);
        }

        /// <summary>
        ///     Moves the game object up.
        ///     Precondition: None
        ///     Postcondition: Y == Y@prev - SpeedY
        /// </summary>
        public void MoveUp()
        {
            this.moveY(-this.SpeedY);
        }

        /// <summary>
        ///     Moves the game object down.
        ///     Precondition: None
        ///     Post-condition: Y == Y@prev + SpeedY
        /// </summary>
        public void MoveDown()
        {
            this.moveY(this.SpeedY);
        }

        /// <summary>
        ///     Flips the sprite horizontally.
        ///     Precondition: none
        ///     Post-condition: none
        /// </summary>
        public void FlipSpriteHorizontally()
        {
            this.Sprite.RenderTransformOrigin = new Point(0.5, 0.5);
            this.Sprite.RenderTransform = new ScaleTransform() { ScaleX = -1 };
        }

        /// <summary>
        ///     Determines whether [has collided with] [the specified other game object].
        ///     Precondition: otherGameObject != null
        ///     Post-condition: none
        /// </summary>
        /// <param name="otherGameObject">The other game object.</param>
        /// <returns>
        ///   <c>true</c> if [has collided with] [the specified other game object]; otherwise, <c>false</c>.
        /// </returns>
        public bool HasCollidedWith(GameObject otherGameObject)
        {
            if (otherGameObject == null)
            {
                throw new NullReferenceException();
            }
            return this.hasIntersectedOtherObjectXBoundary(otherGameObject) &&
                   this.containsSameYCoordinatesAsOtherObject(otherGameObject);
        }

        private bool hasIntersectedOtherObjectXBoundary(GameObject otherGameObject)
        {
            var thisObjectRightSide = this.X + this.Width;
            var otherObjectRightSide = otherGameObject.X + otherGameObject.Width;

            var doesObjectIntersectFromRight = thisObjectRightSide > otherGameObject.X && thisObjectRightSide < otherObjectRightSide;
            var doesObjectIntersectFromLeft = this.X > otherGameObject.X && this.X < otherObjectRightSide;

            return doesObjectIntersectFromRight || doesObjectIntersectFromLeft;
        }

        private bool containsSameYCoordinatesAsOtherObject(GameObject otherGameObject)
        {
            var thisBottomSide = this.Y + this.Height;
            var otherObjectBottomSide = otherGameObject.Y + otherGameObject.Height;

            return this.Y <= otherGameObject.Y && thisBottomSide >= otherObjectBottomSide;
        }

        private void moveX(double x)
        {
            this.X += x;
        }

        private void moveY(double y)
        {
            this.Y += y;
        }

        private void render()
        {
            this.Sprite.RenderAt(this.X, this.Y);
        }

        /// <summary>
        ///     Sets the speed of the game object.
        ///     Precondition: speedX >= 0 AND speedY >=0
        ///     Post-condition: SpeedX == speedX AND SpeedY == speedY
        /// </summary>
        /// <param name="speedX">The speed x.</param>
        /// <param name="speedY">The speed y.</param>
        protected void SetSpeed(double speedX, double speedY)
        {
            if (speedX < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(speedX));
            }

            if (speedY < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(speedY));
            }

            this.SpeedX = speedX;
            this.SpeedY = speedY;
        }

        #endregion
    }
}