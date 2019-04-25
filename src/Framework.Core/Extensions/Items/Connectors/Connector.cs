using System.Linq;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Elements.Factories;
using BindOpen.Framework.Core.Data.Elements.Source;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Core.Extensions.Definitions.Connectors;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Extensions.Items.Connectors
{
    /// <summary>
    /// This class represents a connector.
    /// </summary>
    public abstract class Connector : TAppExtensionItem<IConnectorDefinition>, IConnector
    {
        new public IConnectorConfiguration Configuration { get => base.Configuration as IConnectorConfiguration; }

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The connection string of this instance.
        /// </summary>
        [DetailProperty("connectionString")]
        public string ConnectionString { get; set; }

        #endregion

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
        protected Connector(IConnectorConfiguration dto)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the Connector class.
        /// </summary>
        /// <param name="name">The name of this instance.</param>
        /// <param name="connectionString">The connection string to consider.</param>
        protected Connector(string name, string connectionString = null) : base()
        {
            Name = name;
            ConnectionString = connectionString;
        }

        #endregion

        /// <summary>
        /// Returns a data element representing this instance.
        /// </summary>
        /// <param name="name">The name of the element to create.</param>
        /// <param name="log">The log of the operation.</param>
        /// <returns>Retuns the data element that represents this instace.</returns>
        public ISourceElement AsElement(string name=null, Log log = null)
        {
            return ElementFactory.CreateSource(name ?? Name, base.Configuration as IConnectorConfiguration);
        }

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
            if (connectionString != null)
                ConnectionString = connectionString;
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

        // --------------------------------------------------
        // SERIALIZATION
        // --------------------------------------------------

        #region Serialization

        /// <summary>
        /// Updates information for storage.
        /// </summary>
        /// <param name="log">The log to update.</param>
        public override void UpdateStorageInfo(ILog log = null)
        {
            (Configuration ?? (_configuration = new ConnectorConfiguration())).UpdateFromObject<DetailPropertyAttribute>(this);
            if (string.IsNullOrEmpty(Configuration.DefinitionUniqueId)
                && GetType().GetCustomAttributes(typeof(ConnectorAttribute), false).FirstOrDefault() is ConnectorAttribute attribute
                && attribute.Name.IndexOf("$") > 0)

            {
                Configuration.DefinitionUniqueId = attribute.Name;
            }
            Configuration.UpdateStorageInfo(log);
            if (string.IsNullOrEmpty(Configuration.Name))
                Configuration.Name = Name;
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(IAppScope appScope = null, IScriptVariableSet scriptVariableSet = null, ILog log = null)
        {
            if (Configuration != null)
            {
                if (string.IsNullOrEmpty(Name))
                {
                    Name = Configuration.Name?.IndexOf("$") > 0 ?
                       Configuration.Name.Substring(Configuration.Name.IndexOf("$") + 1) :
                       Configuration.Name;
                }

                Configuration.UpdateRuntimeInfo(appScope, scriptVariableSet, log);
                this.UpdateFromElementSet<DetailPropertyAttribute>(Configuration, appScope, scriptVariableSet);
            }
        }

        #endregion
    }
}
