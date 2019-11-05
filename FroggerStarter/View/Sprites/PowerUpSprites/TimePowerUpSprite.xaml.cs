

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

using System.ComponentModel;
using FroggerStarter.Model.Game_Objects.Power_Ups;

namespace FroggerStarter.View.PowerUpSprites
{
    /// <summary>
    ///     Home sprite class.
    /// </summary>
    /// <seealso cref="FroggerStarter.View.Sprites.BaseSprite" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    public sealed partial class TimePowerUpSprite
    {

        #region Properties

        /// <summary>
        ///     Gets or sets the time extension.
        /// </summary>
        /// <value>
        ///     The time extension.
        /// </value>
        public string TimeExtension
        {
            get => this.timePowerUp.Text;
            set => this.timePowerUp.Text = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="TimePowerUpSprite"/> class.
        /// </summary>
        public TimePowerUpSprite()
        {
            this.InitializeComponent();
        }

        #endregion
    }
}