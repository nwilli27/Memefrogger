using System.Collections.Generic;

namespace FroggerStarter.Interfaces
{
    /// <summary>
    ///     Interface responsible for level requirements
    /// </summary>
    public interface ILevelRequirements
    {

        /// <summary>
        /// Gets the road obstacle spawn time.
        /// </summary>
        /// <value>
        /// The road obstacle spawn time.
        /// </value>
        int RoadObstacleSpawnTime { get; }

        /// <summary>
        /// Gets the road lane1.
        /// </summary>
        /// <value>
        /// The road lane1.
        /// </value>
        IList<object> RoadLane1 { get; }

        /// <summary>
        /// Gets the road lane2.
        /// </summary>
        /// <value>
        /// The road lane2.
        /// </value>
        IList<object> RoadLane2 { get; }

        /// <summary>
        /// Gets the road lane3.
        /// </summary>
        /// <value>
        /// The road lane3.
        /// </value>
        IList<object> RoadLane3 { get; }

        /// <summary>
        /// Gets the road lane4.
        /// </summary>
        /// <value>
        /// The road lane4.
        /// </value>
        IList<object> RoadLane4 { get; }

        /// <summary>
        /// Gets the road lane5.
        /// </summary>
        /// <value>
        /// The road lane5.
        /// </value>
        IList<object> RoadLane5 { get; }

        /// <summary>
        /// Gets the water lane1.
        /// </summary>
        /// <value>
        /// The water lane1.
        /// </value>
        IList<object> WaterLane1 { get; }

        /// <summary>
        /// Gets the water lane2.
        /// </summary>
        /// <value>
        /// The water lane2.
        /// </value>
        IList<object> WaterLane2 { get; }

        /// <summary>
        /// Gets the water lane3.
        /// </summary>
        /// <value>
        /// The water lane3.
        /// </value>
        IList<object> WaterLane3 { get; }

        /// <summary>
        /// Gets the water lane4.
        /// </summary>
        /// <value>
        /// The water lane4.
        /// </value>
        IList<object> WaterLane4 { get; }

        /// <summary>
        /// Gets the water lane5.
        /// </summary>
        /// <value>
        /// The water lane5.
        /// </value>
        IList<object> WaterLane5 { get; }

    }
}
