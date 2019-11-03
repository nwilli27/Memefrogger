

using System.Collections;
using System.Collections.Generic;
using FroggerStarter.Constants;
using FroggerStarter.Utility;

namespace FroggerStarter.Model.Game_Objects.Power_Ups
{
    /// <summary>
    ///     A Class that manages a collection of Power Ups
    /// </summary>
    internal class PowerUpManager : IEnumerable<PowerUp>
    {

        #region Data Members

        private readonly IList<PowerUp> powerUps;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PowerUpManager"/> class.
        ///     Precondition: none
        ///     Post-condition: this.powerUps.Count() == 0
        /// </summary>
        public PowerUpManager()
        {
            this.powerUps = new List<PowerUp>();
        }

        #endregion

        #region Methods


        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        ///     An enumerator that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<PowerUp> GetEnumerator()
        {
            return this.powerUps.GetEnumerator();
        }

        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

    }
}
