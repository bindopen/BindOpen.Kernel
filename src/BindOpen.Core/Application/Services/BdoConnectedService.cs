﻿using BindOpen.Application.Scopes;
using BindOpen.Extensions.Runtime;

namespace BindOpen.Application.Services
{
    /// <summary>
    /// This class represents a connected service.
    /// </summary>
    public abstract class BdoConnectedService : BdoService, IBdoConnectedService
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        // Scope ----------------------

        /// <summary>
        /// The connector of this instance.
        /// </summary>
        protected IBdoConnector _connector = null;

        #endregion

        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        /// <summary>
        /// The connector of this instance.
        /// </summary>
        public IBdoConnector Connector
        {
            get { return _connector; }
        }

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Initializes a new instance of the BdoConnectedService class.
        /// </summary>
        protected BdoConnectedService()
        {
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Sets the specified connector.
        /// </summary>
        /// <param name="connector">The connector to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IBdoConnectedService WithConnector(IBdoConnector connector)
        {
            _connector = connector;

            return this;
        }

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

        /// <summary>
        /// Disposes this instance. 
        /// </summary>
        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);

            if (isDisposing)
            {
                _connector?.Dispose();
            }
        }

        #endregion
    }
}
