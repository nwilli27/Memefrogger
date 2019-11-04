using System;
using System.Collections.Generic;
using System.Linq;
using FroggerStarter.Enums;
using FroggerStarter.Factory;

namespace FroggerStarter.Model.Sound
{
    /// <summary>
    ///     Holds the static class of sound effects to be used anywhere in the system
    /// </summary>
    public static class SoundEffectManager
    {
        #region Data members

        private static IList<SoundEffect> sounds;

        #endregion

        #region Methods

        /// <summary>
        ///     Plays the sound.
        ///     Precondition: none
        ///     Post-condition: none
        /// </summary>
        /// <param name="soundEffectType">Type of the sound effect.</param>
        /// <exception cref="ArgumentOutOfRangeException">soundEffectType - null</exception>
        public static void PlaySound(SoundEffectType soundEffectType)
        {
            sounds.ToList().First(sound => sound.SoundEffectType == soundEffectType).PlaySound();
        }

        /// <summary>
        ///     Creates and loads all the possible sound effects from the SoundEffectFactory
        ///     Precondition: non
        ///     Post-condition: sounds.Count() += total # sounds
        /// </summary>
        public static void CreateAndLoadAllSoundEffects()
        {
            sounds = new List<SoundEffect>();
            var soundEffectTypes = Enum.GetValues(typeof(SoundEffectType)).Cast<SoundEffectType>();

            soundEffectTypes.ToList().ForEach(soundEffect =>
                sounds.Add(SoundEffectFactory.CreateSoundEffect(soundEffect)));
        }

        #endregion
    }
}