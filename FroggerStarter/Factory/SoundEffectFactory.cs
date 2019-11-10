
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

                case SoundEffectType.GtaDeath:
                    return new SoundEffect("GtaDeath.mp3")
                    {
                        SoundEffectType = SoundEffectType.GtaDeath
                    };

                case SoundEffectType.MarioDrown:
                    return new SoundEffect("MarioDrown.mp3")
                    {
                        SoundEffectType = SoundEffectType.MarioDrown
                    };

                case SoundEffectType.MarioDeath:
                    return new SoundEffect("MarioDeath.mp3")
                    {
                        SoundEffectType = SoundEffectType.MarioDeath,
                        IsPlayerDeathSoundEffect = true
                    };

                case SoundEffectType.MarioOof:
                    return new SoundEffect("MarioOof.mp3")
                    {
                        SoundEffectType = SoundEffectType.MarioOof,
                        IsPlayerDeathSoundEffect = true
                    };

                case SoundEffectType.YodaDeath:
                    return new SoundEffect("YodaDeath.mp3")
                    {
                        SoundEffectType = SoundEffectType.YodaDeath,
                        IsPlayerDeathSoundEffect = true
                    };

                case SoundEffectType.WilhelmScream:
                    return new SoundEffect("WilhelmScream.mp3")
                    {
                        SoundEffectType = SoundEffectType.WilhelmScream,
                        IsPlayerDeathSoundEffect = true
                    };

                case SoundEffectType.QuickRevive:
                    return new SoundEffect("QuickRevive.mp3")
                    {
                        SoundEffectType = SoundEffectType.QuickRevive,
                    };

                case SoundEffectType.BeRightBack:
                    return new SoundEffect("BeRightBack.mp3")
                    {
                        SoundEffectType = SoundEffectType.BeRightBack,
                    };

                default:
                    throw new ArgumentOutOfRangeException(nameof(soundEffect), soundEffect, null);
            }
        }

        #endregion
    }
}