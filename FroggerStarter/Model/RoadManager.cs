using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FroggerStarter.Model
{
    /// <summary>
    ///     Class to hold and manage a collection of Lanes.
    /// </summary>
    /// <seealso cref="System.Collections.Generic.IEnumerable{FroggerStarter.Model.Lane}" />
    internal class RoadManager : IEnumerable<Lane>
    {

        #region Data Members

        private readonly IList<Lane> lanes;
        private readonly double width;
        private readonly double startingYLocation;
        private double roadHeight;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="RoadManager"/> class
        ///     Precondition: startingYLocation < endingYLocation;
        /// 
        ///     Post-condition: this.lanes.Count() == 0
        ///                     this.width == width
        ///                     this.startingYLocation == startingYLocation
        ///                     this.laneHeight += @prev
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="startingYLocation">The starting y location.</param>
        /// <param name="endingYLocation">The ending y location.</param>
        public RoadManager(double width, double startingYLocation, double endingYLocation)
        {
            this.startingYLocation = startingYLocation < endingYLocation ? startingYLocation : throw new ArgumentOutOfRangeException();
            this.width = width;
            this.lanes = new List<Lane>();
            this.roadHeight = endingYLocation - startingYLocation;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Moves all vehicles in all lanes.
        ///     Precondition: none
        ///     Post-condition: @each vehicle in this.lanes moveLeft() || moveRight()
        /// </summary>
        public void MoveAllVehicles()
        {
            foreach (var currentLane in this.lanes)
            {
                currentLane.MoveVehicles();
            }
        }

        /// <summary>
        ///     Increases the speed of all vehicles in all lanes by 0.5.
        ///     Precondition: none
        ///     Post-condition: @each vehicle in this.lanes.SpeedX += 0.5
        /// </summary>
        public void IncreaseSpeedOfVehicles()
        {
            foreach (var currentLane in this.lanes)
            {
                currentLane.IncreaseSpeedOfVehiclesByOneHalf();
            }
        }

        /// <summary>
        ///     Sets all vehicles to default speed of there corresponding lane.
        ///     Precondition: none
        ///     Post-condition: @each lane of vehicles : vehicle.SpeedX == lane.defaultSpeed
        /// </summary>
        public void SetAllVehiclesToDefaultSpeed()
        {
            foreach (var currentLane in this.lanes)
            {
                currentLane.SetVehiclesToDefaultSpeed();
            }
        }

        /// <summary>
        ///     Adds a lane with a specified number of [vehicleType] vehicles to it and
        ///     then readjusts the Y spacing of each lane.
        ///     Precondition: defaultSpeed > 0
        ///                   numberOfVehicles > 0
        ///                   
        ///     Post-condition: this.lanes.Count += 1
        ///                     lane.Vehicles.Count += numberOfVehicles
        ///                     @each lane yLocation readjusts accordingly
        /// </summary>
        /// <param name="laneDirection">The lane direction.</param>
        /// <param name="defaultSpeed">The default speed.</param>
        /// <param name="vehicleType">Type of the vehicle.</param>
        /// <param name="numberOfVehicles">The number of vehicles.</param>
        public void AddLaneOfVehicles(LaneDirection laneDirection, double defaultSpeed, VehicleType vehicleType, int numberOfVehicles)
        {
            if (numberOfVehicles <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (defaultSpeed <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            var lane = new Lane(this.width, defaultSpeed, laneDirection);
            lane.AddVehicles(vehicleType, numberOfVehicles);
            this.lanes.Add(lane);
            this.updateYLocationOfLanes();
        }

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        ///     Precondition: none
        ///     Post-condition: none
        /// </summary>
        /// <returns>
        ///     An enumerator that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<Lane> GetEnumerator()
        {
            return this.lanes.GetEnumerator();
        }

        /// <summary>
        ///     Returns an enumerator that iterates through a collection of lanes.
        ///     Precondition: none
        ///     Post-condition: none
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection of lanes.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.lanes.GetEnumerator();
        }

        #endregion

        #region Private Helpers

        private void updateYLocationOfLanes()
        {
            var heightOfEachLane = this.roadHeight / this.lanes.Count;
            for (int i = 0; i < this.lanes.Count; i++)
            {
                var yLocation = this.startingYLocation + (heightOfEachLane * i);
                this.lanes[i].SetVehiclesToLaneYLocation(yLocation, heightOfEachLane);
            }
        }

        #endregion
    }
}
