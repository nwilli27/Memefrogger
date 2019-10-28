﻿
using System.Collections;
using System.Collections.Generic;
using FroggerStarter.Controller;

namespace FroggerStarter.Model
{
    /// <summary>
    ///     Class that holds the collection of frog homes.
    /// </summary>
    /// <seealso cref="System.Collections.Generic.IEnumerable{Home}" />
    internal class FrogHomes : IEnumerable<Home>
    {
        #region Data Members

        private readonly IList<Home> frogHomes;

        #endregion

        #region Constants

        private const int NumberOfHomes = 5;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="FrogHomes"/> class.
        ///     Precondition: none
        ///     Post-condition: this.frogHomes.Count() == NumberOfHomes
        /// </summary>
        public FrogHomes()
        {
            this.frogHomes = new List<Home>();
            this.createFrogHomes();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<Home> GetEnumerator()
        {
            return this.frogHomes.GetEnumerator();
        }

        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.frogHomes.GetEnumerator();
        }

        #endregion

        #region Private Helpers

        private void createFrogHomes()
        {
            var xLocation = getStartingLocation();

            for (var i = 0; i < NumberOfHomes; i++)
            {
                var home = new Home()
                {
                    X = xLocation,
                    Y = GameBoard.HighRoadYLocation
                };

                home.alignInCenterOfHomeLocation();

                this.frogHomes.Add(home);
                xLocation += getSpaceInBetweenEachHome();
            }
        }

        private static double getStartingLocation()
        {
            return GameBoard.HomeLocationGapSize;
        }

        private static double getSpaceInBetweenEachHome()
        {
            return GameBoard.HomeWidth + (GameBoard.HomeLocationGapSize * 2);
        }

        #endregion
    }
}
