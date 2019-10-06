using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FroggerStarter.Model
{

    class LaneManager : IEnumerable<Lane>
    {
        //TODO encapsulate this to be the wrapper collection
        public IList<Lane> Lanes { get; }

        public LaneManager(int laneWidth)
        {
            this.Lanes = new List<Lane>();
            var lane1 = new Lane(LaneDirection.Left, 305, laneWidth, 1);
            lane1.AddVehicle(new Vehicle(VehicleType.Car));
            lane1.AddVehicle(new Vehicle(VehicleType.Car));

            this.Lanes.Add(lane1);
        }

        public void MoveAllVehicles()
        {
            foreach (var currentLane in this.Lanes)
            {
                currentLane.MoveVehicles();
            }
        }

        public void IncreaseSpeedOfVehicles()
        {
            foreach (var currentLane in this.Lanes)
            {
                currentLane.IncreaseSpeedOfVehicles();
            }
        }

        public IEnumerator<Lane> GetEnumerator()
        {
            return this.Lanes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Lanes.GetEnumerator();
        }
    }
}
