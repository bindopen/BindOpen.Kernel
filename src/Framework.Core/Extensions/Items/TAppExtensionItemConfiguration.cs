using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Configuration;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Extensions.Common;
using BindOpen.Framework.Core.Extensions.Items;

namespace BindOpen.Framework.Core.Extensions.Items
{
    /// <summary>
    /// This class represents an application extension item configuration.
    /// </summary>
    public abstract class TAppExtensionItemConfiguration<T>
        : BaseConfiguration, ITAppExtensionItemConfiguration<T>
        where T : IAppExtensionItemDefinition
    {
        // -----------------------------------------------
        // PROPERTIES
        // -----------------------------------------------

        #region Properties

        /// <summary>
        /// The kind of this instance.
        /// </summary>
        [XmlAttribute("kind")]
        public AppExtensionItemKind Kind { get; } = AppExtensionItemKind.Any;

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
        /// Instantiates a new instance of the AppExtensionItemConfiguration class.
        /// </summary>
        protected TAppExtensionItemConfiguration() : this(AppExtensionItemKind.Any, null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the AppExtensionItemConfiguration class.
        /// </summary>
        /// <param name="kind">The kind to consider.</param>
        /// <param name="definitionUniqueId">The definition unique ID to consider.</param>
        /// <param name="items">The items to consider.</param>
        protected TAppExtensionItemConfiguration(
            AppExtensionItemKind kind,
            string definitionUniqueId,
            params IDataElement[] items)
            : base(items)
        {
            Kind = AppExtensionItemKind.Any;
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
            ITAppExtensionItemConfiguration<T> appExtensionItem = base.Clone() as TAppExtensionItemConfiguration<T>;

            return appExtensionItem;
        }

        #endregion
    }
}
