using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FroggerStarter.Model
{

    class Lane
    {

        private const int LaneHeight = 50;

        #region Data Members

        //TODO maybe this should be a list?
        private readonly IList<Vehicle> vehicles;
        private readonly LaneDirection laneDirection;
        private readonly int laneStartingYPoint;
        private readonly int laneWidth;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Lane"/> class.
        ///     Precondition: spaceBetweenVehicles gt;= 0
        ///     Post-condition: this.vehicles.Count == 0
        ///                     this.SpaceBetweenVehicles = spaceBetweenVehicles
        /// </summary>
        /// <param name="laneDirection"></param>
        /// <param name="laneStartingYPoint"></param>
        /// <param name="laneWidth"></param>
        public Lane(LaneDirection laneDirection, int laneStartingYPoint, int laneWidth)
        {
            this.vehicles = new List<Vehicle>();
            this.laneDirection = laneDirection;
            this.laneStartingYPoint = laneStartingYPoint;
            this.laneWidth = laneWidth;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Adds the vehicle to lane.
        /// </summary>
        /// <param name="vehicle">The vehicle.</param>
        public void AddVehicle(Vehicle vehicle)
        {
            this.vehicles.Add(vehicle);
            //TODO sequential coupling?
            this.readjustSpaceBetweenVehicles();
        }

        public void MoveVehicles()
        {
            foreach (var currentVehicle in this.vehicles)
            {
                switch (this.laneDirection)
                {
                    case LaneDirection.Left:
                        currentVehicle.MoveLeft();
                        break;

                    case LaneDirection.Right:
                        currentVehicle.MoveRight();
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        #endregion

        #region Private Helpers

        private void readjustSpaceBetweenVehicles()
        {
            var startingVehicleXPoint = 0;

            foreach (var currentVehicle in this.vehicles)
            {
                currentVehicle.X = startingVehicleXPoint;
                startingVehicleXPoint += this.getSpacingBetweenVehicles();
            }
        }

        private int getSpacingBetweenVehicles()
        {
            return this.laneWidth / this.vehicles.Count;
        }

        #endregion
    }
}
