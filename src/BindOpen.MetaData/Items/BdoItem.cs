using System;

namespace BindOpen.Meta.Items
{
    /// <summary>
    /// This class represents a data item.
    /// </summary>
    /// <remarks>The data item has only an ID, a creation and a last-modification dates.</remarks>
    public abstract class BdoItem : IBdoItem, IClonable
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DataItem class.
        /// </summary>
        protected BdoItem()
        {
        }

        /// <summary>
        /// Disposes this instance.
        /// </summary>
        ~BdoItem()
        {
            Dispose(false);
        }

        #endregion

        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <param name="areas">The areas to consider.</param>
        /// <returns>Returns a cloned instance.</returns>
        public virtual object Clone(params string[] areas)
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <param name="areas">The areas to consider.</param>
        /// <returns>Returns a cloned instance.</returns>
        public T Clone<T>(params string[] areas) where T : class
        {
            return Clone(areas) as T;
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
        /// <param name="isDisposing">Indicates whether this instance is disposing</param>
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
