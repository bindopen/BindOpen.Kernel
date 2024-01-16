using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Logging;
using BindOpen.Scoping.Entities;
using System;
using System.Collections.Generic;
using System.Data;

namespace BindOpen.Scoping.Connectors
{
    /// <summary>
    /// This interfaces represents a connection.
    /// </summary>
    public interface IBdoConnection : IDisposable
    {
        /// <summary>
        /// Connector.
        /// </summary>
        IBdoConnector Connector { get; }

        /// <summary>
        /// The connection string.
        /// </summary>
        string ConnectionString { get; }

        /// <summary>
        /// The connection timeout.
        /// </summary>
        int ConnectionTimeout { get; }

        /// <summary>
        /// The state.
        /// </summary>
        ConnectionState State { get; }

        // Open / Close -----------------------------

        /// <summary>
        /// Opens a connection.
        /// </summary>
        void Connect(IBdoLog log = null);

        /// <summary>
        /// Closes the existing connection.
        /// </summary>
        void Disconnect(IBdoLog log = null);

        // Push / Pull -----------------------------

        /// <summary>
        /// Pulls entity objects using the specified parameter set.
        /// </summary>
        /// <param name="paramSet">The set of meta parameters.</param>
        /// <returns>Returns the entity objects.</returns>
        IEnumerable<IBdoEntity> Pull(IBdoMetaSet paramSet = null, IBdoLog log = null);

        /// <summary>
        /// Pulls entity objects using the specified parameter set.
        /// </summary>
        /// <typeparam name="T">The BindOpen entity class to consider.</typeparam>
        /// <param name="paramSet">The set of meta parameters.</param>
        /// <returns>Returns the entity objects.</returns>
        IEnumerable<T> Pull<T>(IBdoMetaSet paramSet = null, IBdoLog log = null)
            where T : IBdoEntity;

        IEnumerable<IResultItem> Push(params IBdoEntity[] entities);

        /// <summary>
        /// Pushes the specified entity objects.
        /// </summary>
        /// <param name="entities">The entity object to push.</param>
        /// <returns>Returns True whether the entities have been pushed.</returns>
        IEnumerable<IResultItem> Push(IBdoLog log = null, params IBdoEntity[] entities);
    }
}
