using BindOpen.Framework.Core.Extensions.Definition.Connectors;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Extensions.Items.Connectors
{
    /// <summary>
    /// This class represents a connector.
    /// </summary>
    public abstract class Connector : TAppExtensionItem<IConnectorDefinition>, IConnector
    {
        new public IConnectorDto Dto { get; }

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the Connector class.
        /// </summary>
        protected Connector() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the Connector class.
        /// </summary>
        /// <param name="dto">The DTO item of this instance.</param>
        protected Connector(IConnectorDto dto)
        {
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Sets the connection string with the specified string.
        /// </summary>
        /// <param name="connectionString">The connection string to consider.</param>
        /// <returns>Returns a clone of this instance.</returns>
        public virtual void UpdateConnectionString(string connectionString = null)
        {
            if (Dto!=null && connectionString != null)
                Dto.ConnectionString = connectionString;
        }

        #endregion

        // ------------------------------------------
        // MANAGEMENT
        // ------------------------------------------

        #region Management

        // Open / Close -----------------------------

        /// <summary>
        /// Opens a connection.
        /// </summary>
        public virtual ILog Open()
        {
            return new Log();
        }

        /// <summary>
        /// Closes the existing connection.
        /// </summary>
        public virtual ILog Close()
        {
            return new Log();
        }

        /// <summary>
        /// Indicates whether the instance is connected.
        /// </summary>
        public virtual bool IsConnected()
        {
            return false;
        }

        #endregion
    }
}
