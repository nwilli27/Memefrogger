using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FroggerStarter.Model
{

    class RoadManager
    {
        #region Properties

        //TODO what should this be? Property, const?
        public const int LaneHeight = 50;

        #endregion

        #region Data Members

        private IList<Lane> lanes;

        #endregion

        #region Constructors

        public RoadManager(int roadWidth, int roadHeight)
        {
            this.lanes = new List<Lane>();
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

        public void Add(Lane lane, )
        {
            this.lanes.Add(lane);
        }

        public void AddVehiclesToLane(Lane lane, VehicleType vehicleType, int numberOfVehicles)
        {
            for (int i = 0; i < numberOfVehicles; i++)
            {
                lane.Add(new Vehicle(vehicleType));
            }
        } 

        public IList<Vehicle> GetAllVehicles()
        {
            var vehicles = new List<Vehicle>();
            foreach (var currentLane in this.lanes) {
                foreach (Vehicle currentVehicle in currentLane)
                {
                    vehicles.Add(currentVehicle);
                }
            }
            return vehicles;
        }

        #endregion
    }
}
