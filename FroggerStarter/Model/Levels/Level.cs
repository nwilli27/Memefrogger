
using System.Collections.Generic;
using FroggerStarter.Interfaces;

namespace FroggerStarter.Model.Levels
{
    /// <summary>
    ///     Class responsible for a single level
    /// </summary>
    internal abstract class Level : ILevelRequirements
    {

        public abstract IList<object> RoadLane1 { get; }
        public abstract IList<object> RoadLane2 { get; }
        public abstract IList<object> RoadLane3 { get; }
        public abstract IList<object> RoadLane4 { get; }
        public abstract IList<object> RoadLane5 { get; }

        public abstract IList<object> WaterLane1 { get; }
        public abstract IList<object> WaterLane2 { get; }
        public abstract IList<object> WaterLane3 { get; }
        public abstract IList<object> WaterLane4 { get; }
        public abstract IList<object> WaterLane5 { get; }
    }
}
