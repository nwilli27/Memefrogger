using System;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace FroggerStarter.Model.Sound
{
    /// <summary>
    ///     Represents the sound effect base object.
    /// </summary>
    public abstract class SoundEffect
    {
        #region Data members

        private readonly MediaElement soundEffectElement;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="SoundEffect" /> class.
        /// </summary>
        protected SoundEffect(string fileName)
        {
            this.soundEffectElement = new MediaElement();
            this.setSoundFile(fileName);
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Plays the sound.
        /// </summary>
        public void PlaySound()
        {
            this.soundEffectElement.Play();
        }

        private async void setSoundFile(string fileName)
        {
            var folder = await Package.Current.InstalledLocation.GetFolderAsync("SoundFiles");
            var file = await folder.GetFileAsync(fileName);
            var stream = await file.OpenAsync(FileAccessMode.Read);
            this.soundEffectElement.AutoPlay = false;
            this.soundEffectElement.SetSource(stream, "");
        }

        #endregion
    }
}