using System;
using System.Collections.Generic;
using System.Linq;
using FroggerStarter.Enums;
using FroggerStarter.Factory;

namespace FroggerStarter.Model.Sound
{
    /// <summary>
    ///     Defines the object for a Sound Effect Manager.
    /// </summary>
    public class SoundEffectManager
    {
        #region Data members

        private readonly IList<SoundEffect> sounds;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="SoundEffectManager" /> class.
        ///     Precondition: none
        ///     Post-condition: Creates one of each type of SoundEffect and sets them up ready to be played.
        /// </summary>
        public SoundEffectManager()
        {
            this.sounds = new List<SoundEffect>();

            var soundEffectTypes = Enum.GetValues(typeof(SoundEffectType)).Cast<SoundEffectType>();
            soundEffectTypes.ToList().ForEach(soundEffect =>
                this.sounds.Add(SoundEffectFactory.CreateSoundEffect(soundEffect)));
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Plays the sound.
        ///     Precondition: none
        ///     Post-condition: none
        /// </summary>
        /// <param name="soundEffectType">Type of the sound effect.</param>
        /// <exception cref="ArgumentOutOfRangeException">soundEffectType - null</exception>
        public void PlaySound(SoundEffectType soundEffectType)
        {
            switch (soundEffectType)
            {
                case SoundEffectType.Coin:
                    this.sounds.ToList().Where(sound => sound is CoinSoundEffect).ToList()
                        .ForEach(sound => sound.PlaySound());
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(soundEffectType), soundEffectType, null);
            }
        }

        #endregion
    }
}