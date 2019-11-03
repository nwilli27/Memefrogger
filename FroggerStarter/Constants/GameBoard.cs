
using FroggerStarter.Utility;

namespace FroggerStarter.Constants
{
    /// <summary>
    ///     Class holds static public properties that are only allowed to be set once and
    ///     used to hold values related to the GameBoard layout.
    /// </summary>
    public class GameBoard
    {

        #region Data Members

        private static readonly SetOnce<double> BackgroundWidthOnce = new SetOnce<double>();
        private static readonly SetOnce<double> BackgroundHeightOnce = new SetOnce<double>();
        private static readonly SetOnce<double> HighRoadYLocationOnce = new SetOnce<double>();
        private static readonly SetOnce<double> BottomRoadYLocationOnce = new SetOnce<double>();
        private static readonly SetOnce<double> HomeWidthOnce = new SetOnce<double>();
        private static readonly SetOnce<double> HomeLocationGapSizeOnce = new SetOnce<double>();
        private static readonly SetOnce<double> RoadShoulderOffsetOnce = new SetOnce<double>();

        #endregion

        #region Constants

        /// <summary>
        ///     The size of the player sprite
        /// </summary>
        public const int PlayerJumpRange = 50;

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
            get => BackgroundWidthOnce;
            set => BackgroundWidthOnce.Value = value;
        }

        /// <summary>
        ///     Gets or sets the height of the background.
        /// </summary>
        /// <value>
        ///     The height of the background.
        /// </value>
        public static double BackgroundHeight
        {
            get => BackgroundHeightOnce;
            set => BackgroundHeightOnce.Value = value;
        }

        /// <summary>
        ///     Gets or sets the high road y location.
        /// </summary>
        /// <value>
        ///     The high road y location.
        /// </value>
        public static double HighRoadYLocation
        {
            get => HighRoadYLocationOnce;
            set => HighRoadYLocationOnce.Value = value;
        }

        /// <summary>
        ///     Gets or sets the bottom road y location.
        /// </summary>
        /// <value>
        ///     The bottom road y location.
        /// </value>
        public static double BottomRoadYLocation
        {
            get => BottomRoadYLocationOnce;
            set => BottomRoadYLocationOnce.Value = value;
        }

        /// <summary>
        ///     Gets or sets the width of the home.
        /// </summary>
        /// <value>
        ///     The width of the home.
        /// </value>
        public static double HomeWidth
        {
            get => HomeWidthOnce;
            set => HomeWidthOnce.Value = value;
        }

        /// <summary>
        ///     Gets or sets the size of the home location gap.
        /// </summary>
        /// <value>
        ///     The size of the home location gap.
        /// </value>
        public static double HomeLocationGapSize
        {
            get => HomeLocationGapSizeOnce;
            set => HomeLocationGapSizeOnce.Value = value;
        }

        /// <summary>
        ///     Gets or sets the road shoulder offset.
        /// </summary>
        /// <value>
        ///     The road shoulder offset.
        /// </value>
        public static double RoadShoulderOffset
        {
            get => RoadShoulderOffsetOnce;
            set => RoadShoulderOffsetOnce.Value = value;
        }

        #endregion
    }
}