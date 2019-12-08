using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Elements.Source;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Core.Extensions.Definition.Items;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;
using System.Linq;

namespace BindOpen.Framework.Core.Extensions.Runtime.Items
{
    /// <summary>
    /// This class represents a connector.
    /// </summary>
    public abstract class BdoConnector : TBdoExtensionItem<IBdoConnectorDefinition>, IBdoConnector
    {
        /// <summary>
        /// 
        /// </summary>
        new public IBdoConnectorConfiguration Configuration { get => base.Configuration as IBdoConnectorConfiguration; }

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
        protected BdoConnector() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the Connector class.
        /// </summary>
        /// <param name="config">The configuration of this instance.</param>
        protected BdoConnector(IBdoConnectorConfiguration config)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the Connector class.
        /// </summary>
        /// <param name="name">The name of this instance.</param>
        /// <param name="connectionString">The connection string to consider.</param>
        protected BdoConnector(string name, string connectionString = null) : base()
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
        public ISourceElement AsElement(string name = null, IBdoLog log = null)
        {
            return ElementFactory.CreateSource(name ?? Name, base.Configuration as IBdoConnectorConfiguration);
        }

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Updates this instance considering the specified scope.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <returns>Returns the database builder.</returns>
        public virtual IBdoConnector WithScope(IBdoScope scope) => this;

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
        public virtual IBdoLog Open()
        {
            return new BdoLog();
        }

        /// <summary>
        /// Closes the existing connection.
        /// </summary>
        public virtual IBdoLog Close()
        {
            return new BdoLog();
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
        public override void UpdateStorageInfo(IBdoLog log = null)
        {
            (Configuration ?? (_configuration = new BdoConnectorConfiguration())).UpdateFromObject<DetailPropertyAttribute>(this);
            if (string.IsNullOrEmpty(Configuration.DefinitionUniqueId)
                && GetType().GetCustomAttributes(typeof(BdoConnectorAttribute), false).FirstOrDefault() is BdoConnectorAttribute attribute
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
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The set of script variables to consider.</param>
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(IBdoScope scope = null, IBdoScriptVariableSet scriptVariableSet = null, IBdoLog log = null)
        {
            if (Configuration != null)
            {
                if (string.IsNullOrEmpty(Name))
                {
                    Name = Configuration.Name?.IndexOf("$") > 0 ?
                       Configuration.Name.Substring(Configuration.Name.IndexOf("$") + 1) :
                       Configuration.Name;
                }

                Configuration.UpdateRuntimeInfo(scope, scriptVariableSet, log);
                this.UpdateFromElementSet<DetailPropertyAttribute>(Configuration, scope, scriptVariableSet);
            }
        }

        #endregion
    }
}
