using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Runtime.Definition;

namespace BindOpen.Extensions.Connecting
{
    /// <summary>
    /// This class represents a connector configuration.
    /// </summary>
    public class BdoConnectorConfiguration
        : TBdoExtensionItemConfiguration<IBdoConnectorDefinition>,
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
        // IBdoConnectorConfiguration Implementation
        // ------------------------------------------

        #region IBdoConnectorConfiguration

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="items">The items of the item to add.</param>
        /// <returns>Returns the new item that has been added.
        /// Returns null if the new item is null or else its name is null.</returns>
        /// <remarks>The new item must have a name.</remarks>
        public new IBdoConnectorConfiguration Add(params IBdoMetaData[] items)
            => base.Add(items) as IBdoConnectorConfiguration;

        /// <summary>
        /// Sets the specified single item of this instance.
        /// </summary>
        /// <param name="items">The items to apply to this instance.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the values will be the default ones..</remarks>
        public new IBdoConnectorConfiguration WithItems(params IBdoMetaData[] items)
            => base.WithItems(items) as IBdoConnectorConfiguration;

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <returns>Returns a clone of this instance.</returns>
        public string GetConnectionString() => this.GetItem<string>("connectionString");

        /// <summary>
        /// Sets the connection string with the specified string.
        /// </summary>
        /// <param name="connectionString">The connection string to consider.</param>
        /// <returns>Returns a clone of this instance.</returns>
        public virtual IBdoConnectorConfiguration WithConnectionString(string connectionString = null)
        {
            this.Add(BdoMeta.NewScalar("connectionString", DataValueTypes.Text, connectionString));

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
