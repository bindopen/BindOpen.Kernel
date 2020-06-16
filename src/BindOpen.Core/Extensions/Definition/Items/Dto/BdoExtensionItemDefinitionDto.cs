using BindOpen.Data.Items;
using System.ComponentModel;
using System.Xml.Serialization;

namespace BindOpen.Extensions.Definition
{
    /// <summary>
    /// This class represents the definition of BindOpen extension item.
    /// </summary>
    [XmlType("BdoExtensionItemDefinition", Namespace = "https://docs.bindopen.org/xsd")]
    public abstract class BdoExtensionItemDefinitionDto : IndexedDataItem, IBdoExtensionItemDefinitionDto
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// Uri of the image representing this instance.
        /// </summary>
        [XmlElement("imageUri")]
        [DefaultValue("")]
        public string ImageUri
        {
            get;
            set;
        }

        /// <summary>
        /// Business library ID of this instance.
        /// </summary>
        [XmlIgnore()]
        public string LibraryId { get; set; } = "";

        /// <summary>
        /// Indicates whether this instance is editable.
        /// </summary>
        [XmlElement("isEditable")]
        [DefaultValue(true)]
        public bool IsEditable { get; set; } = true;

        /// <summary>
        /// Indicates whether this instance is indexed.
        /// </summary>
        [XmlElement("isIndexed")]
        [DefaultValue(true)]
        public bool IsIndexed { get; set; } = true;

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoExtensionItemDefinitionDto class.
        /// </summary>
        protected BdoExtensionItemDefinitionDto()
            : this(null, "extension_", null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the BdoExtensionItemDefinitionDto class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="preffix">The preffix to consider.</param>
        /// <param name="id">The ID to consider.</param>
        protected BdoExtensionItemDefinitionDto(string name, string preffix, string id)
            : base(name, preffix, id)
        {
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Converts this instance to Html.
        /// </summary>
        /// <param name="uiCulture">The UI culture to consider.</param>
        /// <returns>A text summarizing this instance.</returns>
        public virtual string ToHtml(string uiCulture = "*")
        {
            return "";
        }

        #endregion
    }
}
