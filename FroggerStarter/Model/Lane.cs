using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FroggerStarter.Model
{
    internal class Lane : IEnumerable
    {
        #region Properties



        #endregion

        #region Data Members

        //TODO maybe this should be a list? and need to wrap this around the lane
        private readonly ICollection<Vehicle> vehicles;
        private readonly LaneDirection laneDirection;
        private readonly double laneStartingYPoint;
        private readonly double laneWidth;
        private readonly double defaultSpeed;

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
        /// <param name="defaultSpeed"></param>
        public Lane(double laneStartingYPoint, double laneWidth, double defaultSpeed, LaneDirection laneDirection)
        {
            this.vehicles = new Collection<Vehicle>();
            this.laneDirection = laneDirection;
            this.laneStartingYPoint = laneStartingYPoint;
            this.laneWidth = laneWidth;
            this.defaultSpeed = defaultSpeed;
        }

        #endregion

        #region Methods

        public void AddVehicles(VehicleType vehicleType, int numberOfVehicles)
        {
            for (var i = 0; i < numberOfVehicles; i++)
            {
                this.add(new Vehicle(vehicleType));
            }
            this.readjustSpaceBetweenVehicles();
        }

        public void MoveVehicles()
        {
            foreach (var currentVehicle in this.vehicles)
            {
                switch (this.laneDirection)
                {
                    case LaneDirection.Left:

                        //TODO change these to boolean, ifCrossedOverboundary
                        if (currentVehicle.X + currentVehicle.SpeedX < -currentVehicle.Width)
                        {
                            currentVehicle.X = this.laneWidth;
                        }
                        currentVehicle.MoveLeft();
                        break;

                    case LaneDirection.Right:
                        if (currentVehicle.X + currentVehicle.SpeedX > this.laneWidth)
                        {
                            currentVehicle.X = -currentVehicle.Width;
                        }
                        currentVehicle.MoveRight();
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public void IncreaseSpeedOfVehicles()
        {
            foreach (var currentVehicle in this.vehicles)
            {
                currentVehicle.SpeedX += .5;
            }
        }

        public void SetVehiclesToDefaultSpeed()
        {
            foreach (var currentVehicle in this.vehicles)
            {
                currentVehicle.SpeedX = this.defaultSpeed;
            }
        }

        #endregion

        #region Private Helpers

        /// <summary>
        ///     Adds the vehicle to lane.
        /// </summary>
        /// <param name="vehicle">The vehicle.</param>
        private void add(Vehicle vehicle)
        {
            vehicle.Y = this.alignCarVerticallyInLane(vehicle);
            vehicle.SpeedX = this.defaultSpeed;

            switch (this.laneDirection)
            {
                case LaneDirection.Left:
                    this.vehicles.Add(vehicle);
                    break;

                case LaneDirection.Right:
                    vehicle.FlipSpriteHorizontally();
                    this.vehicles.Add(vehicle);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void readjustSpaceBetweenVehicles()
        {
            var startingVehicleXPoint = 0.0;

            foreach (var currentVehicle in this.vehicles)
            {
                currentVehicle.X = startingVehicleXPoint;
                startingVehicleXPoint += this.getSpacingBetweenVehicles();
            }
        }

        private double getSpacingBetweenVehicles()
        {
            //TODO remove need for lanewidth and do vehicle size * # of vehicles for spacing
            return this.laneWidth / this.vehicles.Count;
        }

        private double alignCarVerticallyInLane(Vehicle vehicle)
        {
            return ((50 - vehicle.Height) / 2) + this.laneStartingYPoint;
        }

        public IEnumerator GetEnumerator()
        {
            return this.vehicles.GetEnumerator();
        }


        #endregion
    }
}
