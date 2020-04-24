using BindOpen.Data.Elements;
using BindOpen.Data.Helpers.Objects;
using BindOpen.Data.Items;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BindOpen.Extensions.Definition
{
    /// <summary>
    /// This class represents the entity definition.
    /// </summary>
    [XmlType("BdoEntityDefinition", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "entity.definition", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class BdoEntityDefinitionDto : BdoExtensionItemDefinitionDto, IBdoEntityDefinitionDto
    {
        // --------------------------------------------------
        // VARIABLES
        // --------------------------------------------------

        #region Variables

        private List<BdoFormatDefinitionDto> _formatDefinitions = new List<BdoFormatDefinitionDto>();

        private List<DataSchema> _possibleMetaSchemas = new List<DataSchema>();

        #endregion

        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// Item class of this instance.
        /// </summary>
        [XmlElement("itemClass")]
        public string ItemClass
        {
            get;
            set;
        }

        /// <summary>
        /// The kind of this instance. 
        /// </summary>
        [XmlElement("kind")]
        public BdoEntityKind Kind { get; set; } = BdoEntityKind.Any;

        /// <summary>
        /// Viewer class of this instance.
        /// </summary>
        /// <remarks>Class names using the following format: winForm=xxx.xxx.xxx;webForm=xxx.xxx.xxx</remarks>
        [XmlElement("viewerClass")]
        public string ViewerClass
        {
            get;
            set;
        }

        /// <summary>
        /// Formats of this instance.
        /// </summary>
        [XmlArray("formats")]
        [XmlArrayItem("format")]
        public List<BdoFormatDefinitionDto> FormatDefinitions
        {
            get
            {
                return this._formatDefinitions ?? (this._formatDefinitions = new List<BdoFormatDefinitionDto>());
            }
        }

        /// <summary>
        /// The possible meta schemas of this instance.
        /// </summary>
        [XmlArray("schemas")]
        [XmlArrayItem("schema")]
        public List<DataSchema> PossibleMetaSchemas
        {
            get
            {
                return this._possibleMetaSchemas ?? (this._possibleMetaSchemas = new List<DataSchema>());
            }
        }

        /// <summary>
        /// The set of detail specifications of this instance.
        /// </summary>
        [XmlElement("detail.specification")]
        public DataElementSpecSet DetailSpec { get; set; } = new DataElementSpecSet();

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the EntityDefinitionDto class.
        /// </summary>
        public BdoEntityDefinitionDto()
        {
        }

        #endregion

        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        // Formats ----------------------------

        /// <summary>
        /// Gets the format with the specified unique name.
        /// </summary>
        /// <param name="uniqueName">Unique name of the application module.</param>
        /// <returns>The current visitor application module.</returns>
        public IBdoFormatDefinitionDto GetFormatWithUniqueName(String uniqueName)
        {
            if (uniqueName == null) return null;

            return (IBdoFormatDefinitionDto)this.FormatDefinitions.Find(p => p.KeyEquals(uniqueName));
        }

        /// <summary>
        /// Gets the format with the specified name.
        /// </summary>
        /// <param name="name">Name of the application module.</param>
        /// <returns>The current visitor application module.</returns>
        public IBdoFormatDefinitionDto GetFormatWithName(String name)
        {
            if (name == null) return null;

            return (IBdoFormatDefinitionDto)this.FormatDefinitions.Find(p => p.KeyEquals(name));
        }

        /// <summary>
        /// Gets the format with the specified ID.
        /// </summary>
        /// <param ID="id">Id of the application module.</param>
        /// <returns>The current visitor application module.</returns>
        public IBdoFormatDefinitionDto GetFormatWithId(String id)
        {
            if (id == null) return null;

            return (IBdoFormatDefinitionDto)this.FormatDefinitions.Find(p => p.Id.KeyEquals(id));
        }

        #endregion
    }
}
