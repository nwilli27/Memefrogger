﻿using System;
using Windows.Foundation;
using Windows.UI.Xaml.Media;
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
            this.createObstacleFrom(obstacleType);
        }

        private void createObstacleFrom(ObstacleType obstacleType)
        {
            switch (obstacleType)
            {
                case ObstacleType.Car:
                    Sprite = new CarSprite();
                    break;

                case ObstacleType.SemiTruck:
                    Sprite = new SemiTruckSprite();
                    break;

                case ObstacleType.ToadTruck:
                    Sprite = new ToadTruckSprite();
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(obstacleType), obstacleType, null);
            }
        }

        /// <summary>
        ///     Flips the game object horizontally.
        ///     Precondition: none
        ///     Post-condition: none
        /// </summary>
        public void FlipObstacleSpriteHorizontally()
        {
            this.Sprite.RenderTransformOrigin = new Point(0.5, 0.5);
            this.Sprite.RenderTransform = new ScaleTransform() { ScaleX = -1 };
        }
    }
}