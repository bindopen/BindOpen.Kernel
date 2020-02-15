using BindOpen.Data.Items;
using BindOpen.System.Diagnostics.Loggers;
using System.ComponentModel;
using System.Xml.Serialization;

namespace BindOpen.Extensions.Definition
{
    /// <summary>
    /// This class represents the definition of BindOpen extension item.
    /// </summary>
    [XmlType("BdoExtensionItemDefinition", Namespace = "https://bindopen.org/xsd")]
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
        public string ImageUri
        {
            get;
            set;
        }

        /// <summary>
        /// Specification of the ImageUrl property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool ImageUrlSpecified
        {
            get
            {
                return !string.IsNullOrEmpty(this.ImageUri);
            }
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
        /// Returns a text summarizing this instance.
        /// </summary>
        /// <param name="logFormat">The log format to consider.</param>
        /// <param name="uiCulture">The UI culture to consider.</param>
        /// <returns>A text summarizing this instance.</returns>
        public virtual string GetText(BdoDefaultLoggerFormat logFormat = BdoDefaultLoggerFormat.Xml, string uiCulture = "*")
        {
            return "";
        }

        #endregion
    }
}
