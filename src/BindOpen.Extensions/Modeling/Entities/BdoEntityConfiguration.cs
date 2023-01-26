using BindOpen.Data.Meta;
using BindOpen.Runtime.Definition;

namespace BindOpen.Extensions.Modeling
{
    /// <summary>
    /// This class represents a entity configuration.
    /// </summary>
    public class BdoEntityConfiguration
        : TBdoExtensionItemConfiguration<IBdoEntityDefinition>,
        IBdoEntityConfiguration
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoEntityConfiguration class.
        /// </summary>
        public BdoEntityConfiguration() : this(null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the BdoEntityConfiguration class.
        /// </summary>
        public BdoEntityConfiguration(string definitionUniqueId)
            : base(BdoExtensionItemKind.Entity, definitionUniqueId)
        {
        }

        #endregion

        // ------------------------------------------
        // IBdoEntityConfiguration Implementation
        // ------------------------------------------

        #region IBdoEntityConfiguration

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="items">The items of the item to add.</param>
        /// <returns>Returns the new item that has been added.
        /// Returns null if the new item is null or else its name is null.</returns>
        /// <remarks>The new item must have a name.</remarks>
        public new IBdoEntityConfiguration Add(params IBdoMetaData[] items)
            => base.Add(items) as IBdoEntityConfiguration;

        /// <summary>
        /// Sets the specified single item of this instance.
        /// </summary>
        /// <param name="items">The items to apply to this instance.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the values will be the default ones..</remarks>
        public new IBdoEntityConfiguration WithItems(params IBdoMetaData[] items)
            => base.WithItems(items) as IBdoEntityConfiguration;

        #endregion

        // ------------------------------------------
        // CLONING
        // ------------------------------------------

        #region Cloning

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns a clone of this instance.</returns>
        public override object Clone(params string[] areas)
        {
            BdoEntityConfiguration configuration = base.Clone(areas) as BdoEntityConfiguration;
            return configuration;
        }

        #endregion
    }
}
