using System;
using System.ComponentModel;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Dictionary;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Core.System.Diagnostics.Loggers;

namespace BindOpen.Framework.Core.Extensions.Definition
{
    /// <summary>
    /// This class represents the definition of application extension item.
    /// </summary>
    [Serializable()]
    [XmlType("AppExtensionItemDefinition", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "extension.item.definition", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public abstract class AppExtensionItemDefinitionDto : IndexedDataItem, IAppExtensionItemDefinitionDto
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// Url of the image representing this instance.
        /// </summary>
        [XmlElement("imageUrl")]
        public string ImageUrl
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
                return !string.IsNullOrEmpty(this.ImageUrl);
            }
        }

        /// <summary>
        /// Business library ID of this instance.
        /// </summary>
        [XmlIgnore()]
        public string LibraryName { get; set; } = "";

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
        /// Instantiates a new instance of the AppExtensionItemDefinition class.
        /// </summary>
        protected AppExtensionItemDefinitionDto()
            : this(null, "extension_", null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the AppExtensionItemDefinition class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="preffix">The preffix to consider.</param>
        /// <param name="id">The ID to consider.</param>
        protected AppExtensionItemDefinitionDto(string name, string preffix, string id)
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
        public virtual string GetText(LogFormat logFormat = LogFormat.Xml, string uiCulture = "*")
        {
            return "";
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Updates this instance with the specified attribute information.
        /// </summary>
        /// <param name="attribute">The attribute to consider.</param>
        public void Update(AppExtensionItemAttribute attribute)
        {
            this.Name = attribute.Name;
            this.Title = new DictionaryDataItem(attribute.Title);
            this.Description = new DictionaryDataItem(attribute.Description);
            this.CreationDate = attribute.CreationDate;
            this.LastModificationDate = attribute.LastModificationDate;
            this.Index = attribute.Index;
        }

        #endregion
    }
}
