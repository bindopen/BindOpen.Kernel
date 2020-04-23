using BindOpen.Application.Configuration;
using BindOpen.Data.Elements;
using BindOpen.Extensions.Definition;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;

namespace BindOpen.Extensions.Runtime
{
    /// <summary>
    /// This class represents a BindOpen extension item configuration.
    /// </summary>
    public abstract class TBdoExtensionItemConfiguration<T>
        : BdoBaseConfiguration, ITBdoExtensionItemConfiguration<T>
        where T : IBdoExtensionItemDefinition
    {
        // -----------------------------------------------
        // PROPERTIES
        // -----------------------------------------------

        #region Properties

        /// <summary>
        /// The kind of this instance.
        /// </summary>
        [XmlAttribute("kind")]
        public BdoExtensionItemKind Kind { get; } = BdoExtensionItemKind.Any;

        /// <summary>
        /// Definition unique ID of this instance.
        /// </summary>
        [XmlAttribute("definition")]
        public string DefinitionUniqueId { get; set; }

        /// <summary>
        /// The group of this instance.
        /// </summary>
        [XmlElement("group")]
        [DefaultValue("")]
        public string Group { get; set; } = "";

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
            params IDataElement[] items)
        {
            Kind = kind;
            DefinitionUniqueId = definitionUniqueId;
            Items = items?.ToList();
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
