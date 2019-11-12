using System.Collections.Generic;

namespace FroggerStarter.Model.Levels
{
    /// <summary>
    ///     Class responsible for a single level
    /// </summary>
    internal abstract class Level
    {
        #region Properties

        /// <summary>
        ///     Gets the road obstacle spawn time.
        /// </summary>
        /// <value>
        ///     The road obstacle spawn time.
        /// </value>
        public abstract int RoadObstacleSpawnTime { get; }

        /// <summary>
        ///     Gets the road lane1.
        /// </summary>
        /// <value>
        ///     The road lane1.
        /// </value>
        public abstract IList<object> RoadLane1 { get; }

        /// <summary>
        ///     Gets the road lane2.
        /// </summary>
        /// <value>
        ///     The road lane2.
        /// </value>
        public abstract IList<object> RoadLane2 { get; }

        /// <summary>
        ///     Gets the road lane3.
        /// </summary>
        /// <value>
        ///     The road lane3.
        /// </value>
        public abstract IList<object> RoadLane3 { get; }

        /// <summary>
        ///     Gets the road lane4.
        /// </summary>
        /// <value>
        ///     The road lane4.
        /// </value>
        public abstract IList<object> RoadLane4 { get; }

        /// <summary>
        ///     Gets the road lane5.
        /// </summary>
        /// <value>
        ///     The road lane5.
        /// </value>
        public abstract IList<object> RoadLane5 { get; }

        /// <summary>
        ///     Gets the water lane1.
        /// </summary>
        /// <value>
        ///     The water lane1.
        /// </value>
        public abstract IList<object> WaterLane1 { get; }

        /// <summary>
        ///     Gets the water lane2.
        /// </summary>
        /// <value>
        ///     The water lane2.
        /// </value>
        public abstract IList<object> WaterLane2 { get; }

        /// <summary>
        ///     Gets the water lane3.
        /// </summary>
        /// <value>
        ///     The water lane3.
        /// </value>
        public abstract IList<object> WaterLane3 { get; }

        /// <summary>
        ///     Gets the water lane4.
        /// </summary>
        /// <value>
        ///     The water lane4.
        /// </value>
        public abstract IList<object> WaterLane4 { get; }

        /// <summary>
        ///     Gets the water lane5.
        /// </summary>
        /// <value>
        ///     The water lane5.
        /// </value>
        public abstract IList<object> WaterLane5 { get; }

        #endregion
    }
}