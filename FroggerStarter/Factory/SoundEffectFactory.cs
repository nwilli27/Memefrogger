using System;
using FroggerStarter.Enums;
using FroggerStarter.Model.Sound;

namespace FroggerStarter.Factory
{
    /// <summary>
    ///     Handles the creation of a sound object
    /// </summary>
    public class SoundEffectFactory
    {
        #region Methods

        /// <summary>
        ///     Creates the sound effect based on the passed in type.
        ///     Precondition: none
        ///     Post-condition: none
        /// </summary>
        /// <param name="soundEffect">The sound effect.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">soundEffect - null</exception>
        public static SoundEffect CreateSoundEffect(SoundEffectType soundEffect)
        {
            switch (soundEffect)
            {
                case SoundEffectType.TimePowerUp:
                    return new SoundEffect("TimePowerUp.wav") {
                        SoundEffectType = SoundEffectType.TimePowerUp
                    };

                case SoundEffectType.GameOver:
                    return new SoundEffect("GameOver.wav") {
                        SoundEffectType = SoundEffectType.GameOver
                    };

                case SoundEffectType.HomeSafely:
                    return new SoundEffect("HomeSafely.mp3")
                    {
                        SoundEffectType = SoundEffectType.HomeSafely
                    };

                default:
                    throw new ArgumentOutOfRangeException(nameof(soundEffect), soundEffect, null);
            }
        }

        #endregion
    }
}