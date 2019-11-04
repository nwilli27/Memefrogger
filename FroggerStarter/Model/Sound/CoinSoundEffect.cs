namespace FroggerStarter.Model.Sound
{
    /// <summary>
    ///     Represents the object for a coin sound effect
    /// </summary>
    /// <seealso cref="FroggerStarter.Model.Sound.SoundEffect" />
    public class CoinSoundEffect : SoundEffect
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="CoinSoundEffect" /> class.
        ///     Precondition: none
        ///     Post-condition: Sets file of media element to 'Coin Pickup Sound.mp3'.
        /// </summary>
        public CoinSoundEffect() : base("Coin Pickup Sound.mp3") {}

        #endregion
    }
}