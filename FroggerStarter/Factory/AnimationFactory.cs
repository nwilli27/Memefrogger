
using System;
using System.Collections.Generic;
using FroggerStarter.Enums;
using FroggerStarter.View.PlayerDeathAnimation;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Factory
{
    /// <summary>
    ///     Factory class that returns a collection of BaseSprites in order of an animation.
    /// </summary>
    internal class AnimationFactory
    {

        /// <summary>
        ///     Creates the animation sprites list according to the [animationType]
        ///     and returns that list.
        ///     Precondition: none
        ///     Post-condition: none
        /// </summary>
        /// <param name="animationType">Type of the animation.</param>
        /// <returns>
        ///     A list of base sprites in order of the animation.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">animationType - null</exception>
        public static List<BaseSprite> CreateAnimationSprites(AnimationType animationType)
        {
            switch (animationType)
            {
                case AnimationType.PlayerDeath:
                    var deathAnimation = new List<BaseSprite>();

                    deathAnimation.Add(new PlayerDeathFrameOne());
                    deathAnimation.Add(new PlayerDeathFrameTwo());
                    deathAnimation.Add(new PlayerDeathFrameThree());
                    deathAnimation.Add(new PlayerDeathFrameFour());
                    deathAnimation.Add(new PlayerDeathFrameFive());

                    return deathAnimation;

                default:
                    throw new ArgumentOutOfRangeException(nameof(animationType), animationType, null);
            }
        }
    }
}
