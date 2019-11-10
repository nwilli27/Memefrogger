using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FroggerStarter.Extensions
{
    /// <summary>
    ///     List extension methods.
    /// </summary>
    public static class ListExtensions
    {
        #region Methods

        /// <summary>
        ///     Converts a List to an ObservableCollection.
        ///     Precondition: none
        ///     Post-condition: none
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <returns>ObservableList of passed in List</returns>
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> collection)
        {
            return collection == null ? null : new ObservableCollection<T>(collection);
        }

        #endregion
    }
}