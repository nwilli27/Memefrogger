
using System;

namespace FroggerStarter.Constants
{
    /// <summary>
    ///     Class allows you to create an object that only allows for it to be set once.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SetOnce<T>
    {
        #region Data Members

        private bool hasValue;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the value.
        /// </summary>
        /// <value>
        ///     The value to be set.
        /// </value>
        /// <exception cref="InvalidOperationException">
        /// Value not set
        /// or
        /// Value already set
        /// </exception>
        public T Value
        {
            get
            {
                if (!this.hasValue) throw new InvalidOperationException("Value not set");
                return this.ValueOrDefault;
            }
            set
            {
                if (this.hasValue) throw new InvalidOperationException("Value already set");
                this.ValueOrDefault = value;
                this.hasValue = true;
            }
        }

        /// <summary>
        ///     Gets the value or default.
        /// </summary>
        /// <value>
        ///     The value or default.
        /// </value>
        public T ValueOrDefault { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Performs an implicit conversion from <see cref="SetOnce{T}"/> to <see cref="T"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     The result of the conversion.
        /// </returns>
        public static implicit operator T(SetOnce<T> value) { return value.Value; }

        /// <summary>
        ///     Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.hasValue ? Convert.ToString(this.ValueOrDefault) : "";
        }

        #endregion
    }
}
