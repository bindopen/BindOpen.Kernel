using BindOpen.Framework.Core.Application.Configuration;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Extensions.Definition.Items;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Extensions.Runtime.Items
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
        public string Group { get; set; } = "";

        /// <summary>
        /// Specification of the Group property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool GroupSpecified => !string.IsNullOrEmpty(Group);

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
            : base(items)
        {
            Kind = BdoExtensionItemKind.Any;
            DefinitionUniqueId = definitionUniqueId;
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
        public override object Clone()
        {
            ITBdoExtensionItemConfiguration<T> appExtensionItem = base.Clone() as TBdoExtensionItemConfiguration<T>;

            return appExtensionItem;
        }

        #endregion
    }
}
