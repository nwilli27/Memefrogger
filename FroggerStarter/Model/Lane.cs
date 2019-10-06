using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FroggerStarter.Model
{
    internal class Lane : IEnumerable
    {

        private const int LaneHeight = 50;

        #region Data Members

        //TODO maybe this should be a list? and need to wrap this around the lane
        public IList<Vehicle> Vehicles { get; }


        private readonly LaneDirection laneDirection;
        private readonly int laneStartingYPoint;
        private readonly int laneWidth;
        private readonly int initialSpeed;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Lane"/> class.
        ///     Precondition: spaceBetweenVehicles gt;= 0
        ///     Post-condition: this.Vehicles.Count == 0
        ///                     this.SpaceBetweenVehicles = spaceBetweenVehicles
        /// </summary>
        /// <param name="laneDirection"></param>
        /// <param name="laneStartingYPoint"></param>
        /// <param name="laneWidth"></param>
        public Lane(LaneDirection laneDirection, int laneStartingYPoint, int laneWidth, int initialSpeed)
        {
            this.Vehicles = new List<Vehicle>();
            this.laneDirection = laneDirection;
            this.laneStartingYPoint = laneStartingYPoint;
            this.laneWidth = laneWidth;
            this.initialSpeed = initialSpeed;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Adds the vehicle to lane.
        /// </summary>
        /// <param name="vehicle">The vehicle.</param>
        public void AddVehicle(Vehicle vehicle)
        {
            vehicle.Y = this.laneStartingYPoint;
            vehicle.SpeedX = this.initialSpeed;
            this.Vehicles.Add(vehicle);

            //TODO sequential coupling?
            this.readjustSpaceBetweenVehicles();
        }

        public void MoveVehicles()
        {
            foreach (var currentVehicle in this.Vehicles)
            {
                switch (this.laneDirection)
                {
                    case LaneDirection.Left:
                        if (currentVehicle.X - currentVehicle.SpeedX < -50)
                        {
                            currentVehicle.X = this.laneWidth;
                        }
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

        public void IncreaseSpeedOfVehicles()
        {
            foreach (var currentVehicle in this.Vehicles)
            {
                currentVehicle.SpeedX += 1;
            }
        }

        #endregion

        #region Private Helpers

        private void readjustSpaceBetweenVehicles()
        {
            var startingVehicleXPoint = 0;

            foreach (var currentVehicle in this.Vehicles)
            {
                currentVehicle.X = startingVehicleXPoint;
                startingVehicleXPoint += this.getSpacingBetweenVehicles();
            }
        }

        private int getSpacingBetweenVehicles()
        {
            //TODO remove need for lanewidth and do vehicle size * # of Vehicles for spacing
            return this.laneWidth / this.Vehicles.Count;
        }



        #endregion

        public IEnumerator GetEnumerator()
        {
            return this.Vehicles.GetEnumerator();
        }
    }
}
