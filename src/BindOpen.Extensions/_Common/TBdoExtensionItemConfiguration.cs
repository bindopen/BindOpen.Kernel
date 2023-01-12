using BindOpen.Meta.Configuration;
using BindOpen.Meta.Elements;
using BindOpen.Runtime.Definition;

namespace BindOpen.Extensions
{
    /// <summary>
    /// This class represents a BindOpen extension item configuration.
    /// </summary>
    public abstract class TBdoExtensionItemConfiguration<T>
        : BdoConfiguration, ITBdoExtensionItemConfiguration<T>
        where T : IBdoExtensionItemDefinition
    {
        // -----------------------------------------------
        // PROPERTIES
        // -----------------------------------------------

        #region Properties

        /// <summary>
        /// The kind of this instance.
        /// </summary>
        public BdoExtensionItemKind Kind { get; } = BdoExtensionItemKind.Any;

        /// <summary>
        /// Definition unique ID of this instance.
        /// </summary>
        public string DefinitionUniqueId { get; set; }

        /// <summary>
        /// The group of this instance.
        /// </summary>
        public string GroupId { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoExtensionItemConfiguration class.
        /// </summary>
        protected TBdoExtensionItemConfiguration() : this(BdoExtensionItemKind.Any, null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the BdoExtensionItemConfiguration class.
        /// </summary>
        /// <param name="kind">The kind to consider.</param>
        /// <param name="definitionUniqueId">The definition unique ID to consider.</param>
        /// <param name="items">The items to consider.</param>
        protected TBdoExtensionItemConfiguration(
            BdoExtensionItemKind kind,
            string definitionUniqueId,
            params IBdoMetaElement[] items)
        {
            Kind = kind;
            DefinitionUniqueId = definitionUniqueId;
            WithItems(items);
        }

        #endregion

        // ------------------------------------------
        // CLONING
        // ------------------------------------------

        #region Cloning

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns the cloned metrics definition.</returns>
        public override object Clone(params string[] areas)
        {
            ITBdoExtensionItemConfiguration<T> appExtensionItem = base.Clone(areas) as TBdoExtensionItemConfiguration<T>;

            return appExtensionItem;
        }

        #endregion
    }
}
