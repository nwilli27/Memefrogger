using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FroggerStarter.Model
{

    class LaneManager
    {
        private Lane lane1;
        private Lane lane2;
        private Lane lane3;
        private Lane lane4;
        private Lane lane5;

        public LaneManager(int laneWidth)
        {
            this.lane1 = new Lane(LaneDirection.Left, 55, laneWidth);
            this.lane1.AddVehicle(new Vehicle(VehicleType.Car, 1));

        }
    }
}
