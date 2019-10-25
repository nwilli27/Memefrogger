using System;
using Windows.Foundation;
using Windows.UI.Xaml.Media;
using FroggerStarter.Factory;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{
    /// <summary>
    ///     A Obstacle sprite object of type GameObject.
    /// </summary>
    /// <seealso cref="FroggerStarter.Model.GameObject" />
    internal class Obstacle : GameObject
    {

        /// <summary>
        ///     Initializes a new instance of the <see cref="Obstacle"/> class.
        ///     Creates a Game object sprite according to the type of obstacle passed in.
        ///     Precondition: none
        ///     Post-condition: none
        /// </summary>
        /// <param name="obstacleType">Type of obstacle to create.</param>
        public Obstacle(ObstacleType obstacleType)
        {
            Sprite = ObstacleFactory.CreateObstacleSprite(obstacleType);
        }

        /// <summary>
        ///     Flips the game object horizontally.
        ///     Precondition: none
        ///     Post-condition: none
        /// </summary>
        public void FlipSpriteHorizontally()
        {
            this.Sprite.RenderTransformOrigin = new Point(0.5, 0.5);
            this.Sprite.RenderTransform = new ScaleTransform() { ScaleX = -1 };
        }
    }
}
