using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FroggerStarter.Model
{

    class LaneManager
    {
        #region Properties

        //TODO what should this be? Property, const?
        public const int LaneHeight = 50;

        #endregion

        #region Data Members

        private IList<Lane> lanes;

        #endregion

        #region Constructors

        public LaneManager()
        {
            this.lanes = new List<Lane>();
        }

        #endregion

        #region Methods

        public void MoveAllVehicles()
        {
            foreach (var currentLane in this.lanes)
            {
                currentLane.MoveVehicles();
            }
        }

        public void IncreaseSpeedOfVehicles()
        {
            foreach (var currentLane in this.lanes)
            {
                currentLane.IncreaseSpeedOfVehicles();
            }
        }

        public void Add(Lane lane)
        {
            this.lanes.Add(lane);
        }

        #endregion
    }
}
