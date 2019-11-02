﻿using FroggerStarter.Enums;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{
    /// <summary>
    ///     A semi truck object of type Obstacle.
    /// </summary>
    /// <seealso cref="FroggerStarter.Model.Obstacle" />
    public sealed class SemiTruck : Obstacle
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="SemiTruck" /> class.
        ///     Precondition: none
        ///     Post-condition: none
        /// </summary>
        /// <param name="direction">The direction the vehicle is facing.</param>
        public SemiTruck(Direction direction) : base(direction)
        {
            Sprite = new SemiTruckSprite();
            this.MoveToDefaultLocation();
        }

        #endregion
    }
}