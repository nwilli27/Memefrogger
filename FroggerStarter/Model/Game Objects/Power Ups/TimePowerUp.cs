
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FroggerStarter.Enums;
using FroggerStarter.Model.Score;
using FroggerStarter.Model.Sound;
using FroggerStarter.Properties;
using FroggerStarter.Utility;
using FroggerStarter.View.PowerUpSprites;

namespace FroggerStarter.Model.Game_Objects.Power_Ups
{
    /// <summary>
    ///     A special Power up that gains the user extra time
    /// </summary>
    /// <seealso cref="PowerUp" />
    internal sealed class TimePowerUp : PowerUp, INotifyPropertyChanged
    {
        #region Data Members

        private int timeExtension;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the time extension.
        /// </summary>
        /// <value>
        ///     The time extension.
        /// </value>
        public int TimeExtension
        {
            get => this.timeExtension;
            set
            {
                this.timeExtension = value;
                this.onPropertyChanged();
            }
        }

        #endregion

        #region Constants

        private const int LowSecondRange = 3;
        private const int HighSecondRange = 9;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="TimePowerUp"/> class.
        ///     Precondition: none
        ///     Post-condition: Sprite == TimePowerUpSprite
        /// </summary>
        public TimePowerUp()
        {
            this.Sprite = new TimePowerUpSprite();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Adds a random score extension between [3-9]
        ///     seconds to the current ongoing ScoreTick.
        ///     Precondition: none
        ///     Post-condition: ScoreTick += timeExtension
        /// </summary>
        public override void Activate()
        {
            ScoreTimer.ScoreTick += this.TimeExtension;
            SoundEffectManager.PlaySound(SoundEffectType.Coin);
        }

        /// <summary>
        ///     Setups the the power up by assigning a random time extension
        ///     and binding the TimeExtension property to the Sprite text block.
        ///     Precondition: none
        ///     Post-condition: this.TimeExtension = random [3-9]
        /// </summary>
        public override void SetupAbility()
        {
            this.TimeExtension = getRandomTimeExtension();
        }

        #endregion

        #region Private Helpers

        private static int getRandomTimeExtension()
        {
            return Randomizer.GetRandomValueInRange(LowSecondRange, HighSecondRange);
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void onPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}
