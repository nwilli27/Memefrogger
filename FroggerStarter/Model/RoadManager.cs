using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FroggerStarter.Model
{
    internal class RoadManager : IEnumerable<Lane>
    {

        #region Data Members

        private readonly IList<Lane> lanes;
        private readonly double width;
        private readonly double startingYLocation;
        private double laneHeight;

        #endregion

        #region Constructors

        public RoadManager(double width, double startingYLocation, double endingYLocation, int numberOfLanes)
        {
            this.lanes = new List<Lane>();
            this.width = width;
            this.startingYLocation = startingYLocation;
            this.setHeightOfEachLane(startingYLocation, endingYLocation, numberOfLanes);
        }

        #endregion

        #region Methods

        public void MoveAllVehicles()
        {
            foreach (var currentLane in this.lanes)
            {
                currentLane.MoveVehicles();
            }
        }

        public void IncreaseSpeedOfVehicles()
        {
            foreach (var currentLane in this.lanes)
            {
                currentLane.IncreaseSpeedOfVehicles();
            }
        }

        public void SetAllVehiclesToDefaultSpeed()
        {
            foreach (var currentLane in this.lanes)
            {
                currentLane.SetVehiclesToDefaultSpeed();
            }
        }

        public void AddLaneOfVehicles(LaneDirection laneDirection, double defaultSpeed, VehicleType vehicleType, int numberOfVehicles)
        {
            var lane = new Lane(this.getNextYLocationForLane(), this.width, defaultSpeed, laneDirection);
            lane.AddVehicles(vehicleType, numberOfVehicles);
            this.lanes.Add(lane);
        }

        #endregion

        #region Private Helpers

        private void setHeightOfEachLane(double roadStartingY, double roadEndingY, int numberOfLanes)
        {
            this.laneHeight = (roadEndingY - roadStartingY) / (numberOfLanes - 1);
        }

        private double getNextYLocationForLane()
        {
            if (this.lanes.Count == 0)
            {
                return this.startingYLocation;
            }
            return (this.lanes.Count * this.laneHeight) + this.startingYLocation;
        }

        public IEnumerator<Lane> GetEnumerator()
        {
            return this.lanes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.lanes.GetEnumerator();
        }

        #endregion
    }
}
