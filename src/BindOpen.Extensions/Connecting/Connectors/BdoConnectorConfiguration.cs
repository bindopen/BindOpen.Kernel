using BindOpen.Data;
using BindOpen.Data.Elements;
using BindOpen.Runtime.Definition;

namespace BindOpen.Extensions.Connecting
{
    /// <summary>
    /// This class represents a connector configuration.
    /// </summary>
    public class BdoConnectorConfiguration
        : TBdoExtensionTitledItemConfiguration<IBdoConnectorDefinition>,
        IBdoConnectorConfiguration
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoConnectorConfiguration class.
        /// </summary>
        public BdoConnectorConfiguration() : this(null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the BdoConnectorConfiguration class.
        /// </summary>
        public BdoConnectorConfiguration(string definitionUniqueId)
            : base(BdoExtensionItemKind.Connector, definitionUniqueId)
        {
        }

        #endregion


        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accesors

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <returns>Returns a clone of this instance.</returns>
        public string GetConnectionString() => GetItem<string>("connectionString");

        #endregion


        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        public new IBdoConnectorConfiguration Add(params IBdoElement[] items)
        {
            base.Add(items);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        public new IBdoConnectorConfiguration WithItems(params IBdoElement[] items)
        {
            base.WithItems(items);
            return this;
        }

        /// <summary>
        /// Sets the connection string with the specified string.
        /// </summary>
        /// <param name="connectionString">The connection string to consider.</param>
        /// <returns>Returns a clone of this instance.</returns>
        public virtual IBdoConnectorConfiguration WithConnectionString(string connectionString = null)
        {
            Add(BdoElements.NewScalar("connectionString", DataValueTypes.Text, connectionString));

            return this;
        }

        #endregion

        // ------------------------------------------
        // CLONING
        // ------------------------------------------

        #region Cloning

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>A cloned connector of this instance.</returns>
        public override object Clone(params string[] areas)
        {
            BdoConnectorConfiguration configuration = base.Clone(areas) as BdoConnectorConfiguration;

            return configuration;
        }

        #endregion
    }
}
