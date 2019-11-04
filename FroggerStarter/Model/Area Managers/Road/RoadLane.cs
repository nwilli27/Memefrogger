using FroggerStarter.Enums;

namespace FroggerStarter.Model.Area_Managers.Road
{
    /// <summary>
    ///     Defines the object for a RoadLane based on the Lane.
    /// </summary>
    /// <seealso cref="FroggerStarter.Model.Area_Managers.Lane" />
    public class RoadLane : Lane
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="RoadLane" /> class.
        ///     Precondition: none
        ///     Post-condition: this.obstacles.Count == 0
        ///                     this.direction = direction
        ///                     this.defaultSpeed = defaultSpeed
        /// </summary>
        /// <param name="defaultSpeed">The default speed of all obstacles</param>
        /// <param name="direction">The direction the obstacles are moving in the lane</param>
        public RoadLane(double defaultSpeed, Direction direction) : base(defaultSpeed, direction)
        {
        }

        #endregion
    }
}