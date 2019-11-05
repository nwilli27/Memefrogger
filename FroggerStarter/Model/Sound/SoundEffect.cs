using System;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using FroggerStarter.Enums;

namespace FroggerStarter.Model.Sound
{
    /// <summary>
    ///     Represents the sound effect base object.
    /// </summary>
    public class SoundEffect
    {

        #region Properties

        /// <summary>
        ///     Gets or sets the type of the sound effect.
        /// </summary>
        /// <value>
        ///     The type of the sound effect.
        /// </value>
        public SoundEffectType SoundEffectType { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is player death sound effect.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is player death sound effect; otherwise, <c>false</c>.
        /// </value>
        public bool IsPlayerDeathSoundEffect { get; set; }

        #endregion

        #region Data members

        private readonly MediaElement soundEffectElement;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="SoundEffect" /> class.
        ///     Precondition: fileName != null
        ///     Post-condition: none
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public SoundEffect(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException();
            }
            this.soundEffectElement = new MediaElement();
            this.setSoundFile(fileName);
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Plays the sound.
        ///     Precondition: none
        ///     Post-condition: none
        /// </summary>
        public void PlaySound()
        {
            this.soundEffectElement.Play();
        }

        #endregion

        #region Private Helpers

        private async void setSoundFile(string fileName)
        {
            var folder = await Package.Current.InstalledLocation.GetFolderAsync("SoundEffects");
            var file = await folder.GetFileAsync(fileName);
            var stream = await file.OpenAsync(FileAccessMode.Read);
            this.soundEffectElement.AutoPlay = false;
            this.soundEffectElement.SetSource(stream, "");
        }

        #endregion
    }
}