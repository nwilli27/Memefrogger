
using FroggerStarter.Constants;

namespace FroggerStarter.Controller
{
    /// <summary>
    ///     Class holds public properties that are only allowed to be set once and
    ///     used to hold values related to the GameBoard layout.
    /// </summary>
    public class GameBoard
    {

        #region Data Members

        private static readonly SetOnce<double> backgroundWidth = new SetOnce<double>();
        private static readonly SetOnce<double> backgroundHeight = new SetOnce<double>();
        private static readonly SetOnce<double> highRoadYLocation = new SetOnce<double>();
        private static readonly SetOnce<double> bottomRoadYLocation = new SetOnce<double>();
        private static readonly SetOnce<double> homeWidth = new SetOnce<double>();
        private static readonly SetOnce<double> homeLocationGapSize = new SetOnce<double>();

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the width of the background.
        /// </summary>
        /// <value>
        ///     The width of the background.
        /// </value>
        public static double BackgroundWidth
        {
            get => backgroundWidth;
            set => backgroundWidth.Value = value;
        }

        /// <summary>
        ///     Gets or sets the height of the background.
        /// </summary>
        /// <value>
        ///     The height of the background.
        /// </value>
        public static double BackgroundHeight
        {
            get => backgroundHeight;
            set => backgroundHeight.Value = value;
        }

        /// <summary>
        ///     Gets or sets the high road y location.
        /// </summary>
        /// <value>
        ///     The high road y location.
        /// </value>
        public static double HighRoadYLocation
        {
            get => highRoadYLocation;
            set => highRoadYLocation.Value = value;
        }

        /// <summary>
        ///     Gets or sets the bottom road y location.
        /// </summary>
        /// <value>
        ///     The bottom road y location.
        /// </value>
        public static double BottomRoadYLocation
        {
            get => bottomRoadYLocation;
            set => bottomRoadYLocation.Value = value;
        }

        /// <summary>
        ///     Gets or sets the width of the home.
        /// </summary>
        /// <value>
        ///     The width of the home.
        /// </value>
        public static double HomeWidth
        {
            get => homeWidth;
            set => homeWidth.Value = value;
        }

        /// <summary>
        ///     Gets or sets the size of the home location gap.
        /// </summary>
        /// <value>
        ///     The size of the home location gap.
        /// </value>
        public static double HomeLocationGapSize
        {
            get => homeLocationGapSize;
            set => homeLocationGapSize.Value = value;
        }

        #endregion
    }
}