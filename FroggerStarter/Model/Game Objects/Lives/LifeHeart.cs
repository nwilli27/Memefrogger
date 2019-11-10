
using FroggerStarter.Enums;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model.Game_Objects.Lives
{
    /// <summary>
    ///     A class to hold a single life
    /// </summary>
    internal abstract class LifeHeart : GameObject
    {

        /// <summary>
        ///     The heart lost animation
        /// </summary>
        public Animation.Animation HeartLostAnimation { get; set; }

    }
}
