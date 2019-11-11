using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FroggerStarter.Model.Game_Objects.Moving_Object;

namespace FroggerStarter.Model.Area_Managers
{
    /// <summary>
    ///     Class to hold and manage a collection of Lanes.
    /// </summary>
    /// <seealso cref="System.Collections.Generic.IEnumerable{Lane}" />
    public abstract class LaneManager : IEnumerable<Obstacle>
    {

        #region Data Members

        /// <summary>
        ///     The lanes collection
        /// </summary>
        protected readonly IList<Lane> Lanes;

        /// <summary>
        ///     The starting y location
        /// </summary>
        protected readonly double StartingYLocation;

        /// <summary>
        ///     The height
        /// </summary>
        protected readonly double Height;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="LaneManager"/> class
        ///     Precondition: startingYLocation lt; endingYLocation;
        /// 
        ///     Post-condition: this.lanes.Count() == 0
        ///                     this.startingYLocation == startingYLocation
        ///                     this.height += endingYLocation - startingYLocation
        /// </summary>
        /// <param name="startingYLocation">The starting y location.</param>
        /// <param name="endingYLocation">The ending y location.</param>
        protected LaneManager(double startingYLocation, double endingYLocation)
        {
            this.StartingYLocation = startingYLocation < endingYLocation ? startingYLocation : throw new ArgumentOutOfRangeException();
            this.Lanes = new List<Lane>();
            this.Height = endingYLocation - startingYLocation;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Moves all obstacles in all lanes.
        ///     Precondition: none
        ///     Post-condition: @each obstacle in this.lanes moveLeft() || moveRight()
        /// </summary>
        public void MoveAllObstacles()
        {
            this.Lanes.ToList().ForEach(lane => lane.MoveObstacles());
        }

        /// <summary>
        ///     Updates the y location of lanes.
        /// </summary>
        protected void UpdateYLocationOfLanes()
        {
            var heightOfEachLane = this.Height / this.Lanes.Count;
            for (var i = 0; i < this.Lanes.Count; i++)
            {
                var yLocation = this.StartingYLocation + (heightOfEachLane * i);
                this.Lanes[i].SetObstaclesToLaneYLocation(yLocation, heightOfEachLane);
            }
        }

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        ///     An enumerator that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<Obstacle> GetEnumerator()
        {
            return this.Lanes.SelectMany(lane => lane).GetEnumerator();
        }

        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Lanes.SelectMany(lane => lane).GetEnumerator();
        }

        #endregion
    }
}
