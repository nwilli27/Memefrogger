using System;
using System.Collections.Generic;
using System.Linq;
using FroggerStarter.Enums;
using FroggerStarter.Factory;
using FroggerStarter.Utility;

namespace FroggerStarter.Model.Sound
{
    /// <summary>
    ///     Holds the static class of sound effects to be used anywhere in the system
    /// </summary>
    public static class SoundEffectManager
    {
        #region Data members

        private static IList<SoundEffect> allSounds;
        private static IList<SoundEffect> playerDeathSounds;

        #endregion

        #region Methods

        /// <summary>
        ///     Plays the sound.
        ///     Precondition: none
        ///     Post-condition: none
        /// </summary>
        /// <param name="soundEffectType">Type of the sound effect.</param>
        /// <exception cref="ArgumentOutOfRangeException">soundEffectType - null</exception>
        public static void PlaySound(Enums.SoundEffectType soundEffectType)
        {
            allSounds.ToList().First(sound => sound.SoundEffectType == soundEffectType).PlaySound();
        }

        /// <summary>
        ///     Plays a random player death sound.
        ///     Precondition: none
        ///     Post-condition: randomDeathSound.Play()
        /// </summary>
        public static void PlayRandomPlayerDeathSound()
        {
            var randomIndexValue = Randomizer.GetRandomValueInRange(0, playerDeathSounds.Count);
            playerDeathSounds[randomIndexValue].PlaySound();
        }

        /// <summary>
        ///     Creates and loads all the possible sound effects from the SoundEffectFactory
        ///     Precondition: non
        ///     Post-condition: sounds.Count() += total # sounds
        /// </summary>
        public static void CreateAndLoadAllSoundEffects()
        {
            allSounds = new List<SoundEffect>();
            playerDeathSounds = new List<SoundEffect>();

            var soundEffectTypes = Enum.GetValues(typeof(SoundEffectType)).Cast<SoundEffectType>();
            soundEffectTypes.ToList().ForEach(soundEffectType =>
                             allSounds.Add(SoundEffectFactory.CreateSoundEffect(soundEffectType)));

            var deathSounds = allSounds.Where(soundEffect => soundEffect.IsPlayerDeathSoundEffect);
            deathSounds.ToList().ForEach(deathSound => playerDeathSounds.Add(deathSound));
        }

        #endregion
    }
}