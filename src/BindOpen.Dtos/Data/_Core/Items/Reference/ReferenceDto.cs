﻿using BindOpen.Data.Meta;
using BindOpen.Script;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents a data exp that can contain a literal and script texts.
    /// </summary>
    [XmlType("Reference", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
    [XmlRoot(ElementName = "reference", Namespace = "https://storage.bindopen.org/xsd/bindopen", IsNullable = false)]
    public class ReferenceDto : IDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The kind of this instance.
        /// </summary>
        [JsonPropertyName("kind")]
        [XmlElement("kind")]
        public BdoReferenceKind Kind { get; set; }

        /// <summary>
        /// The identifier of this instance.
        /// </summary>
        [JsonPropertyName("identifier")]
        [XmlElement("identifier")]
        public string Identifier { get; set; }

        /// <summary>
        /// The expression of this instance.
        /// </summary>
        [JsonPropertyName("expression")]
        [XmlElement("expression")]
        public ExpressionDto Expression { get; set; }

        /// <summary>
        /// The script word of this instance.
        /// </summary>
        [JsonPropertyName("word")]
        [XmlElement("word")]
        public ScriptwordDto Word { get; set; }

        /// <summary>
        /// The script word of this instance.
        /// </summary>
        [JsonPropertyName("metaData")]
        [XmlElement("metaData")]
        public MetaDataDto MetaData { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of DataReferenceDto class.
        /// </summary>
        public ReferenceDto()
        {
        }

        #endregion
    }
}