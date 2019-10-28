using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Store.Preview.InstallControl;
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

        #endregion
    }
}