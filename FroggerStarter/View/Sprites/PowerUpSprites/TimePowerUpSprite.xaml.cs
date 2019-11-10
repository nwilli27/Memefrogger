

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace FroggerStarter.View.Sprites.PowerUpSprites
{
    /// <summary>
    ///     Home sprite class.
    /// </summary>
    /// <seealso cref="BaseSprite" />
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
        ///     Initializes a new instance of the <see cref="Sprites.PowerUpSprites.TimePowerUpSprite"/> class.
        /// </summary>
        public TimePowerUpSprite()
        {
            this.InitializeComponent();
        }

        #endregion
    }
}