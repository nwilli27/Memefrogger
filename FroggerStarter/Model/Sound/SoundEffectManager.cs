using System;
using System.Collections.Generic;
using System.Linq;
using FroggerStarter.Enums;
using FroggerStarter.Factory;

namespace FroggerStarter.Model.Sound
{
    public class SoundEffectManager
    {
        #region Data members

        private readonly IList<SoundEffect> sounds;

        #endregion

        #region Constructors

        public SoundEffectManager()
        {
            this.sounds = new List<SoundEffect>();

            var soundEffectTypes = Enum.GetValues(typeof(SoundEffectType)).Cast<SoundEffectType>();
            soundEffectTypes.ToList().ForEach(soundEffect =>
                this.sounds.Add(SoundEffectFactory.CreateSoundEffect(soundEffect)));
        }

        public void PlaySound(SoundEffectType soundEffectType)
        {
            switch (soundEffectType)
            {
                case SoundEffectType.Coin:
                    this.sounds.ToList().Where(sound => sound is CoinSoundEffect).ToList().ForEach(sound => sound.PlaySound());
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(soundEffectType), soundEffectType, null);
            }
        }

        #endregion
    }
}