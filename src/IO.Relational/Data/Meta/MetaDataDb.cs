using BindOpen.Data.Assemblies;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a data element.
    /// </summary>
    public class MetaDataDb : IBdoDb, IIdentified
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// ID of this instance.
        /// </summary>
        [Key]
        [Column("MetaDataId")]
        public string Identifier { get; set; }

        /// <summary>
        /// Name of this instance.
        /// </summary>
        public BdoMetaDataKind Kind { get; set; } = BdoMetaDataKind.None;

        /// <summary>
        /// Name of this instance.
        /// </summary>
        public string Name { get; set; }

        // IIndexedDataItem -------------------------------

        /// <summary>
        /// The index of this instance.
        /// </summary>
        public int? Index { get; set; }

        // Items

        /// <summary>
        /// The expression of this instance.
        /// </summary>
        public ReferenceDb Reference { get; set; }

        /// <summary>
        /// The elements of this instance.
        /// </summary>
        [ForeignKey(nameof(SpecId))]
        public SpecDb Spec { get; set; }

        /// <summary>
        /// The class name of this instance.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public string SpecId { get; set; }

        /// <summary>
        /// The value type of this instance.
        /// </summary>
        public DataValueTypes ValueType { get; set; } = DataValueTypes.Any;

        /// <summary>
        /// Creation date of this instance.
        /// </summary>
        public string DefinitionUniqueName { get; set; }

        /// <summary>
        /// The class reference of this instance.
        /// </summary>
        public ClassReferenceDb ClassReference { get; set; }

        public List<MetaSetDb> Sets { get; set; }

        // Node ------------------------

        /// <summary>
        /// The elements of this instance.
        /// </summary>
        [NotMapped]
        public List<MetaDataDb> MetaItems { get; set; }

        // Object ------------------------

        // Scalar ------------------------

        /// <summary>
        /// The values of this instance.
        /// </summary>
        public List<string> Items { get; set; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new database entity data element.
        /// </summary>
        public MetaDataDb()
        {
        }

        #endregion
    }
}
