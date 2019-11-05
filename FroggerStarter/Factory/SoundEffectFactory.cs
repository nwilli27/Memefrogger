
using FroggerStarter.Model.Sound;
using System;
using SoundEffectType = FroggerStarter.Enums.SoundEffectType;

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
                    return new SoundEffect("TimePowerUp.mp3") {
                        SoundEffectType = SoundEffectType.TimePowerUp
                    };

                case SoundEffectType.GameOver:
                    return new SoundEffect("GameOver.wav") {
                        SoundEffectType = SoundEffectType.GameOver
                    };

                case SoundEffectType.HomeSafely:
                    return new SoundEffect("HomeSafely.mp3") {
                        SoundEffectType = SoundEffectType.HomeSafely
                    };

                case SoundEffectType.AlmostHadIt:
                    return new SoundEffect("AlmostHadIt.mp3")
                    {
                        SoundEffectType = SoundEffectType.AlmostHadIt
                    };

                case SoundEffectType.HomerDoh:
                    return new SoundEffect("HomerDoh.mp3")
                    {
                        SoundEffectType = SoundEffectType.HomerDoh,
                        IsPlayerDeathSoundEffect = true
                    };

                case SoundEffectType.Oof:
                    return new SoundEffect("Oof.mp3")
                    {
                        SoundEffectType = SoundEffectType.Oof,
                        IsPlayerDeathSoundEffect = true
                    };

                case SoundEffectType.GTADeath:
                    return new SoundEffect("GTADeath.mp3")
                    {
                        SoundEffectType = SoundEffectType.GTADeath
                    };

                default:
                    throw new ArgumentOutOfRangeException(nameof(soundEffect), soundEffect, null);
            }
        }

        #endregion
    }
}