using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FroggerStarter.Model
{
    //TODO make this class generic for any lane of game objects
    /// <summary>
    ///     A row of moving objects of type GameObject.
    /// </summary>
    /// <seealso cref="System.Collections.IEnumerable" />
    internal class Lane : IEnumerable
    {

        #region Data Members

        private readonly ICollection<Vehicle> vehicles;
        private readonly LaneDirection laneDirection;
        private readonly double laneWidth;
        private readonly double defaultSpeed;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Lane"/> class.
        ///     Precondition: none
        ///     Post-condition: this.vehicles.Count == 0
        ///                     this.laneDirection = laneDirection
        ///                     this.defaultSpeed = defaultSpeed
        /// </summary> 
        /// <param name="laneDirection">The direction the game objects are moving in the lane</param>
        /// <param name="laneWidth">The width of the lane</param>
        /// <param name="defaultSpeed">The default speed of all game objects</param>
        public Lane(double laneWidth, double defaultSpeed, LaneDirection laneDirection)
        {
            this.vehicles = new Collection<Vehicle>();
            this.laneDirection = laneDirection;
            this.laneWidth = laneWidth;
            this.defaultSpeed = defaultSpeed;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Adds a specified number of vehicles to the lane of a specified vehicle type.
        ///     Precondition: none
        ///     Post-condition: this.vehicles.Count == numberOfVehicles
        /// </summary>
        /// <param name="vehicleType">Type of the vehicle</param>
        /// <param name="numberOfVehicles">The number of vehicles</param>
        public void AddVehicles(VehicleType vehicleType, int numberOfVehicles)
        {
            for (var i = 0; i < numberOfVehicles; i++)
            {
                this.add(new Vehicle(vehicleType));
            }
            this.readjustSpaceBetweenVehicles();
        }

        /// <summary>
        ///     Moves all game objects according to which direction the lane is going.
        ///     If the object moves past the lane boundary, its placed back on the other end of the lane.
        ///     Precondition: none
        ///     Post-condition: @each in this.vehicles.X +- vehicle.SpeedX
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">When lane direction isn't available</exception>
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

        /// <summary>
        ///     Increases the speed of all vehicles by 0.5.
        ///     Precondition: none
        ///     Post-condition: @each in this.vehicles : vehicle.SpeedX += 0.5
        /// </summary>  
        public void IncreaseSpeedOfVehiclesByOneHalf()
        {
            foreach (var currentVehicle in this.vehicles)
            {
                currentVehicle.SpeedX += .5;
            }
        }

        /// <summary>
        ///     Sets the vehicles to default speed of the lane.
        ///     Precondition: none
        ///     Post-condition: @each vehicle in this.vehicles : vehicle.SpeedX == this.defaultSpeed
        /// </summary>
        public void SetVehiclesToDefaultSpeed()
        {
            foreach (var currentVehicle in this.vehicles)
            {
                currentVehicle.SpeedX = this.defaultSpeed;
            }
        }

        /// <summary>
        ///     Sets all vehicles to the specified yLocation and are aligned
        ///     vertically within the height of the lane.
        ///     Precondition: heightOfLane gt; 0
        ///     Post-condition: @each vehicle in this.vehicles : vehicle.Y == verticalYAlignment
        /// </summary>
        /// <param name="yLocation">The y location.</param>
        /// <param name="heightOfLane">The height of lane.</param>
        public void SetVehiclesToLaneYLocation(double yLocation, double heightOfLane)
        {
            if (heightOfLane <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            foreach (var currentVehicle in this.vehicles)
            {
                var verticalYAlignment = this.getCenteredYLocationOfLane(currentVehicle, yLocation, heightOfLane);
                currentVehicle.Y = verticalYAlignment;
            }
        }

        /// <summary>
        ///     Returns an enumerator that iterates through the collection of game objects.
        ///     Precondition: none
        ///     Post-condition: none
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Collections.IEnumerator"></see> object that can 
        ///     be used to iterate through the collection of game objects.
        /// </returns>
        public IEnumerator GetEnumerator()
        {
            return this.vehicles.GetEnumerator();
        }

        #endregion

        #region Private Helpers

        private double getCenteredYLocationOfLane(Vehicle vehicle, double yLocation, double heightOfLane)
        {
            return ((heightOfLane - vehicle.Height) / 2) + yLocation;
        }

        private void add(Vehicle vehicle)
        {
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
            return this.laneWidth / this.vehicles.Count;
        }

        #endregion
    }
}
