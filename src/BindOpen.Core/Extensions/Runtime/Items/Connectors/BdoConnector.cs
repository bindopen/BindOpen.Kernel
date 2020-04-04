using BindOpen.Application.Scopes;
using BindOpen.Data.Connections;
using BindOpen.Data.Elements;
using BindOpen.Data.Helpers.Objects;
using BindOpen.Extensions.Definition;
using BindOpen.System.Diagnostics;
using BindOpen.System.Scripting;
using System;
using System.Linq;

namespace BindOpen.Extensions.Runtime
{
    /// <summary>
    /// This class represents a connector.
    /// </summary>
    public abstract class BdoConnector : TBdoExtensionItem<IBdoConnectorDefinition>, IBdoConnector
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        new public IBdoConnectorConfiguration Configuration { get => base.Configuration as IBdoConnectorConfiguration; }

        /// <summary>
        /// The connection string of this instance.
        /// </summary>
        [DetailProperty("connectionString")]
        public string ConnectionString { get; set; }

        /// <summary>
        /// The connection string of this instance.
        /// </summary>
        [DetailProperty("connectionTimeOut")]
        public int ConnectionTimeOut { get; set; }

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

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

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

        #endregion

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
        public virtual IBdoConnector WithConnectionString(string connectionString = null)
        {
            if (connectionString != null)
                ConnectionString = connectionString;

            return this;
        }

        /// <summary>
        /// Executes the specified function.
        /// </summary>
        /// <param name="action">The action using the created connection and the current log to consider.</param>
        /// <param name="isAutoConnected">Indicates whether the connection is automatically opened.</param>
        /// <returns></returns>
        public void UsingConnection(
            Action<IBdoConnection> action,
            bool isAutoConnected = true)
            => UsingConnection((c, l) => action?.Invoke(c), null, isAutoConnected);

        /// <summary>
        /// Executes the specified function.
        /// </summary>
        /// <param name="action">The action using the created connection and the current log to consider.</param>
        /// <param name="isAutoConnected">Indicates whether the connection is automatically opened.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="isAutoConnected">Indicates whether the connection is automatically opened.</param>
        /// <returns></returns>
        public void UsingConnection(
            Action<IBdoConnection, IBdoLog> action,
            IBdoLog log,
            bool isAutoConnected = true)
        {
            log = log ?? new BdoLog();

            using (var connection = CreateConnection(log))
            {
                if (!log.HasErrorsOrExceptions() && connection != null)
                {
                    if (isAutoConnected)
                    {
                        try
                        {
                            log.AddEvents(connection.Connect());
                        }
                        catch (Exception ex)
                        {
                            log.AddException("An exception occured while trying to open connection",
                                description: ex.ToString());
                        }
                    }

                    if (!log.HasErrorsOrExceptions())
                    {
                        action?.Invoke(connection, log);
                    }
                }
            }
        }

        #endregion

        // ------------------------------------------
        // MANAGEMENT
        // ------------------------------------------

        #region Management

        /// <summary>
        /// Creates a new connection.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        public virtual IBdoConnection CreateConnection(IBdoLog log = null)
        {
            return null;
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
