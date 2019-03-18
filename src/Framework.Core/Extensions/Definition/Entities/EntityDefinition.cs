using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Items.Schema;
using BindOpen.Framework.Core.Extensions.Definition.Formats;

namespace BindOpen.Framework.Core.Extensions.Definition.Entities
{

    /// <summary>
    /// This class represents the entity definition.
    /// </summary>
    [Serializable()]
    [XmlType("EntityDefinition", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "entity.definition", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class EntityDefinition : AppExtensionItemDefinition
    {
        // --------------------------------------------------
        // VARIABLES
        // --------------------------------------------------

        #region Variables

        private List<FormatDefinition> _FormatDefinitions = new List<FormatDefinition>();

        private List<DataSchema> _PossibleMetaSchemas = new List<DataSchema>();

        #endregion

        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// Item class of this instance.
        /// </summary>
        [XmlElement("itemClass")]
        public String ItemClass
        {
            get;
            set;
        }

        /// <summary>
        /// The kind of this instance. 
        /// </summary>
        [XmlElement("kind")]
        public EntityKind Kind { get; set; } = EntityKind.Any;

        /// <summary>
        /// Viewer class of this instance.
        /// </summary>
        /// <remarks>Class names using the following format: winForm=xxx.xxx.xxx;webForm=xxx.xxx.xxx</remarks>
        [XmlElement("viewerClass")]
        public String ViewerClass
        {
            get;
            set;
        }

        /// <summary>
        /// Formats of this instance.
        /// </summary>
        [XmlArray("formats")]
        [XmlArrayItem("format")]
        public List<FormatDefinition> FormatDefinitions
        {
            get
            {
                return this._FormatDefinitions ?? (this._FormatDefinitions = new List<FormatDefinition>());
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
                return this._PossibleMetaSchemas ?? (this._PossibleMetaSchemas = new List<DataSchema>());
            }
        }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the EntityDefinition class.
        /// </summary>
        public EntityDefinition()
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
        public FormatDefinition GetFormatWithUniqueName(String uniqueName)
        {
            if (uniqueName == null) return null;

            return this.FormatDefinitions.Find(p => p.KeyEquals(uniqueName));
        }

        /// <summary>
        /// Gets the format with the specified name.
        /// </summary>
        /// <param name="name">Name of the application module.</param>
        /// <returns>The current visitor application module.</returns>
        public FormatDefinition GetFormatWithName(String name)
        {
            if (name == null) return null;

            return this.FormatDefinitions.Find(p => p.KeyEquals(name));
        }

        /// <summary>
        /// Gets the format with the specified ID.
        /// </summary>
        /// <param ID="id">Id of the application module.</param>
        /// <returns>The current visitor application module.</returns>
        public FormatDefinition GetFormatWithId(String id)
        {
            if (id == null) return null;

            return this.FormatDefinitions.Find(p => p.Id.KeyEquals(id));
        }

        #endregion
    }
}
