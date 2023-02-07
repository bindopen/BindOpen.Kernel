﻿using BindOpen.Data.Items;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a catalog element specification.
    /// </summary>
    [XmlType("ObjectSpec", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot(ElementName = "object.spec", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
    public class ObjectSpecDto : SpecDto
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        // Entity ----------------------------------

        /// <summary>
        /// The class filter of this instance.
        /// </summary>
        [JsonPropertyName("class.filter")]
        [XmlElement("class.filter")]
        public StringFilterDto ClassFilter { get; set; }

        /// <summary>
        /// The definition filter of this instance.
        /// </summary>
        [JsonPropertyName("definition.filter")]
        [XmlElement("definition.filter")]
        public StringFilterDto DefinitionFilter { get; set; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CollectionElementSpecDto class.
        /// </summary>
        public ObjectSpecDto()
        {
        }

        #endregion
    }
}