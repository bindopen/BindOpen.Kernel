using BindOpen.Application.Scopes;
using BindOpen.Data.Items;
using System;

namespace BindOpen.Application.Services
{
    /// <summary>
    /// This class represents an bot.
    /// </summary>
    public abstract class BdoService : IdentifiedDataItem, IBdoScoped
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        // Scope ----------------------

        /// <summary>
        /// The scope of this instance.
        /// </summary>
        protected IBdoScope _scope = null;

        #endregion

        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        /// <summary>
        /// The scope of this instance.
        /// </summary>
        public IBdoScope Scope
        {
            get { return _scope; }
        }

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Initializes a new instance of the BdoService class.
        /// </summary>
        protected BdoService()
        {
        }

        /// <summary>
        /// Initializes a new instance of the BdoService class.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        protected BdoService(IBdoScope scope)
        {
            _scope = scope;
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Sets the specified scope.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <returns>Returns this instance.</returns>
        public virtual IBdoScoped WithScope(IBdoScope scope)
        {
            _scope = scope;

            return this;
        }

        #endregion

        // ------------------------------------------
        // IDISPOSABLE METHODS
        // ------------------------------------------

        #region IDisposable_Methods

        private bool _isDisposed = false;

        /// <summary>
        /// Disposes this instance. 
        /// </summary>
        /// <param name="isDisposing">Indicates whether this instance is disposing</param>
        protected override void Dispose(bool isDisposing)
        {
            if (_isDisposed)
            {
                return;
            }

            _scope?.Dispose();

            _isDisposed = true;

            if (isDisposing)
            {
                GC.SuppressFinalize(this);
            }

            base.Dispose(isDisposing);
        }

        #endregion
    }
}