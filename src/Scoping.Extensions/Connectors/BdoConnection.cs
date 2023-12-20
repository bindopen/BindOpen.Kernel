using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Logging;
using BindOpen.Scoping.Entities;
using System.Collections.Generic;
using System.Data;

namespace BindOpen.Scoping.Connectors
{
    /// <summary>
    /// This class represents a connection.
    /// </summary>
    public abstract class BdoConnection : BdoObject, IBdoConnection
    {
        // -----------------------------------------------
        // IBdoConnection Implementation
        // -----------------------------------------------

        #region IBdoConnection

        /// <summary>
        /// The connector of this instance.
        /// </summary>
        public IBdoConnector Connector { get; protected set; }

        /// <summary>
        /// The connection string of this instance.
        /// </summary>
        public string ConnectionString => Connector?.ConnectionString;

        /// <summary>
        /// The connection timeout of this instance.
        /// </summary>
        public int ConnectionTimeout => Connector?.ConnectionTimeOut ?? 0;

        /// <summary>
        /// The state of this instance.
        /// </summary>
        public ConnectionState State => ConnectionState.Closed;

        // Open / Close -----------------------------

        /// <summary>
        /// Connects this instance.
        /// </summary>
        public abstract void Connect(IBdoLog log = null);

        /// <summary>
        /// Disconnects this instance.
        /// </summary>
        public abstract void Disconnect(IBdoLog log = null);

        // Push / Pull -----------------------------

        /// <summary>
        /// Pulls entity objects using the specified parameter set.
        /// </summary>
        /// <param name="paramSet">The set of meta parameters.</param>
        /// <returns>Returns the entity objects.</returns>
        public virtual IEnumerable<IBdoEntity> Pull(IBdoMetaSet paramSet = null)
        {
            var entities = Pull<IBdoEntity>(paramSet);

            return entities;
        }

        /// <summary>
        /// Pulls entity objects using the specified parameter set.
        /// </summary>
        /// <typeparam name="T">The BindOpen entity class to consider.</typeparam>
        /// <param name="paramSet">The set of meta parameters.</param>
        /// <returns>Returns the entity objects.</returns>
        public abstract IEnumerable<T> Pull<T>(IBdoMetaSet paramSet = null)
            where T : IBdoEntity;

        /// <summary>
        /// Pushes the specified entity objects.
        /// </summary>
        /// <param name="entities">The entity object to push.</param>
        /// <returns>Returns True whether the entities have been pushed.</returns>
        public abstract bool Push(params IBdoEntity[] entities);

        #endregion

        // ------------------------------------------
        // IDISPOSABLE METHODS
        // ------------------------------------------

        #region IDisposable_Methods

        private bool _isDisposed = false;

        /// <summary>
        /// Disposes this instance. 
        /// </summary>
        /// <param key="isDisposing">Indicates whether this instance is disposing</param>
        protected override void Dispose(bool isDisposing)
        {
            if (_isDisposed)
            {
                return;
            }

            Connector?.Dispose();

            _isDisposed = true;

            base.Dispose(isDisposing);
        }

        #endregion
    }
}
