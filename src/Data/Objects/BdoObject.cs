using System;
using BindOpen.Kernel.Data;

namespace BindOpen.Kernel.Data
{
    /// <summary>
    /// This class represents a data item.
    /// </summary>
    /// <remarks>The data item has only an ID, a creation and a last-modification dates.</remarks>
    public abstract class BdoObject : IBdoObject, IClonable
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DataItem class.
        /// </summary>
        protected BdoObject()
        {
        }

        /// <summary>
        /// Disposes this instance.
        /// </summary>
        ~BdoObject()
        {
            Dispose(false);
        }

        #endregion

        // --------------------------------------------------
        // IClonable Implementation
        // --------------------------------------------------

        #region IClonable

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <param key="areas">The areas to consider.</param>
        /// <returns>Returns a cloned instance.</returns>
        public virtual object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <param key="areas">The areas to consider.</param>
        /// <returns>Returns a cloned instance.</returns>
        public T Clone<T>() where T : class
        {
            return Clone() as T;
        }

        #endregion

        // --------------------------------------------------
        // IDisposable Implementation
        // --------------------------------------------------

        #region IDisposable Implementation

        /// <summary>
        /// Disposes this instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _isDisposed = false;

        /// <summary>
        /// Disposes specifying whether this instance is disposing.
        /// </summary>
        /// <param key="isDisposing">Indicates whether this instance is disposing</param>
        protected virtual void Dispose(bool isDisposing)
        {
            if (_isDisposed)
            {
                return;
            }

            _isDisposed = true;
        }

        #endregion
    }
}
