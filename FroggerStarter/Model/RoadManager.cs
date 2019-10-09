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
        ///     Precondition: width > 0
        ///                   startingYLocation < endingYLocation;
        ///                   numberOfLanes &gt= 0
        /// 
        ///     Post-condition: this.lanes.Count() == 0
        ///                     this.width == width
        ///                     this.startingYLocation == startingYLocation
        ///                     this.laneHeight += @prev
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="startingYLocation">The starting y location.</param>
        /// <param name="endingYLocation">The ending y location.</param>
        /// <param name="numberOfLanes">The number of lanes.</param>
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
        ///     Increases the speed of all vehicles in all lanes.
        ///     Precondition: none
        ///     Post-condition: @each vehicle in this.lanes.SpeedX += 0.5
        /// </summary>
        public void IncreaseSpeedOfVehicles()
        {
            foreach (var currentLane in this.lanes)
            {
                currentLane.IncreaseSpeedOfVehicles();
            }
        }

        /// <summary>
        ///     Sets all vehicles to default speed of there corresponding lane.
        ///     Precondition: n
        /// </summary>
        public void SetAllVehiclesToDefaultSpeed()
        {
            foreach (var currentLane in this.lanes)
            {
                currentLane.SetVehiclesToDefaultSpeed();
            }
        }

        public void AddLaneOfVehicles(LaneDirection laneDirection, double defaultSpeed, VehicleType vehicleType, int numberOfVehicles)
        {
            var lane = new Lane(this.width, defaultSpeed, laneDirection);
            lane.AddVehicles(vehicleType, numberOfVehicles);
            this.lanes.Add(lane);
            this.adjustSpacingOfVehiclesInLanes();
        }

        #endregion

        #region Private Helpers

        private void adjustSpacingOfVehiclesInLanes()
        {
            var heightOfEachLane = this.roadHeight / this.lanes.Count;
            for (int i = 0; i < this.lanes.Count; i++)
            {
                var yLocation = this.startingYLocation + (heightOfEachLane * i);
                this.lanes[i].SetVehiclesToLaneYLocation(yLocation, heightOfEachLane);
            }
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
