﻿using BindOpen.Data.Assemblies;
using BindOpen.Data.Schema;
using BindOpen.Scoping.Script;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a data element.
    /// </summary>
    [XmlType("MetaData", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
    [XmlInclude(typeof(MetaNodeDto))]
    [XmlInclude(typeof(MetaObjectDto))]
    [XmlInclude(typeof(MetaScalarDto))]
    [XmlInclude(typeof(ScriptwordDto))]
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "metaKind")]
    [JsonDerivedType(typeof(MetaNodeDto), "Node")]
    [JsonDerivedType(typeof(MetaObjectDto), "Object")]
    [JsonDerivedType(typeof(MetaScalarDto), "Scalar")]
    [JsonDerivedType(typeof(ScriptwordDto), "Scripword")]
    public abstract class MetaDataDto : IBdoDto, IIdentified
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// ID of this instance.
        /// </summary>
        [JsonPropertyName("id")]
        [XmlAttribute("id")]
        public string Identifier { get; set; }

        /// <summary>
        /// Name of this instance.
        /// </summary>
        [JsonPropertyName("name")]
        [XmlAttribute("name")]
        [DefaultValue(null)]
        public string Name { get; set; }

        // IIndexedDataItem -------------------------------

        /// <summary>
        /// The index of this instance.
        /// </summary>
        [JsonPropertyName("index")]
        [XmlElement("index")]
        public int? Index { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NotMapped]
        [JsonIgnore()]
        [XmlIgnore()]
        public bool IndexSpecified => Index != null;

        // Items

        /// <summary>
        /// The expression of this instance.
        /// </summary>
        [JsonPropertyName("ref")]
        [XmlElement("ref")]
        public ReferenceDto Reference { get; set; }

        /// <summary>
        /// The elements of this instance.
        /// </summary>
        [JsonPropertyName("schema")]
        [XmlElement("schema")]
        public SchemaDto Schema { get; set; }

        /// <summary>
        /// The value type of this instance.
        /// </summary>
        [JsonPropertyName("valueType")]
        [XmlAttribute("valueType")]
        [DefaultValue(DataValueTypes.Any)]
        public DataValueTypes ValueType { get; set; } = DataValueTypes.Any;

        /// <summary>
        /// Creation date of this instance.
        /// </summary>
        [JsonPropertyName("definition")]
        [XmlAttribute("definition")]
        public string DefinitionUniqueName { get; set; }

        /// <summary>
        /// The class reference of this instance.
        /// </summary>
        [JsonPropertyName("class")]
        [XmlElement("class")]
        public ClassReferenceDto ClassReference { get; set; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new DTO data element.
        /// </summary>
        public MetaDataDto()
        {
        }

        #endregion
    }
}
