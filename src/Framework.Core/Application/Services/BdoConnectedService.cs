﻿using BindOpen.Framework.Data.Items;
using BindOpen.Framework.Extensions.Runtime;

namespace BindOpen.Framework.Application.Services
{
    /// <summary>
    /// This class represents a connected service.
    /// </summary>
    public abstract class BdoConnectedService : DataItem, IBdoConnectedService
    {
        /// <summary>
        /// The connector of this instance.
        /// </summary>
        protected IBdoConnector _connector = null;

        /// <summary>
        /// The connector of this instance.
        /// </summary>
        public IBdoConnector Connector
        {
            get { return _connector; }
        }

        /// <summary>
        /// Initializes a new instance of the BdoConnectedService class.
        /// </summary>
        protected BdoConnectedService()
        {
        }

        /// <summary>
        /// Initializes a new instance of the BdoConnectedService class.
        /// </summary>
        /// <param name="connector">The connector to consider.</param>
        protected BdoConnectedService(IBdoConnector connector)
        {
            SetConnector(connector);
        }

        /// <summary>
        /// Sets the specified connector.
        /// </summary>
        /// <param name="connector">The connector to consider.</param>
        public void SetConnector(IBdoConnector connector)
        {
            _connector = connector;
        }

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