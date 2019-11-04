using System;
using System.Collections.Generic;

namespace FroggerStarter.Model.Area_Managers.Water
{
    class WaterManager : LaneManager
    {
        public WaterManager(double startingYLocation, double endingYLocation) : base(startingYLocation, endingYLocation)
        {
        }

        public override void AddLaneOfObstacles(IList<object> laneSettings)
        {
            throw new NotImplementedException();
        }

        //TODO: Similar to WaterLane and RoadLane, there may be functionality present within WaterManager vs RoadManager
        //TODO: that require some more methods from the LaneManager class to be abstract because of how differently they're implemented
    }
}
