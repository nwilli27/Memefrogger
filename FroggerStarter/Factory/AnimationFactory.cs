
using System;
using System.Collections.Generic;
using FroggerStarter.Enums;
using FroggerStarter.View.FrogLeapAnimation;
using FroggerStarter.View.LifeHeartLostAnimation;
using FroggerStarter.View.PlayerDeathAnimation;
using FroggerStarter.View.SpeedBoatSplashAnimation;
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
                    var deathAnimation = new List<BaseSprite>
                    {
                        new PlayerDeathFrameOne(),
                        new PlayerDeathFrameTwo(),
                        new PlayerDeathFrameThree(),
                        new PlayerDeathFrameFour()
                    };

                    return deathAnimation;

                case AnimationType.FrogLeap:
                    var frogLeapAnimation = new List<BaseSprite>
                    {
                        new FrogLeapFrameOne()
                    };
                    return frogLeapAnimation;

                case AnimationType.LifeHeartLost:
                    var heartLostAnimation = new List<BaseSprite>
                    {
                        new LifeHeartLostFrameOne(),
                        new LifeHeartLostFrameTwo(),
                        new LifeHeartLostFrameThree(),
                        new LifeHeartLostFrameFour(),
                        new LifeHeartLostFrameFive(),
                        new LifeHeartLostFrameSix(),
                        new LifeHeartLostFrameSeven(),
                        new LifeHeartLostFrameEight()
                    };

                    return heartLostAnimation;

                case AnimationType.SpeedBoatSplash:
                    var speedBoatSplashAnimation = new List<BaseSprite>
                    {
                        new SplashBoatFrameOne(),
                        new SplashBoatFrameTwo(),
                        new SplashBoatFrameThree()
                    };

                    return speedBoatSplashAnimation;

                default:
                    throw new ArgumentOutOfRangeException(nameof(animationType), animationType, null);
            }
        }
    }
}
