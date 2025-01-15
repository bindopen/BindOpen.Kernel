using BindOpen.Data.Assemblies;
using BindOpen.Data.Conditions;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a data element specification.
    /// </summary>
    public class SpecDb : IBdoDb, IIdentified
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// ID of this instance.
        /// </summary>
        [Key]
        [Column("SpecId")]
        public string Identifier { get; set; }

        public string ParentId { get; set; }

        public List<SpecDb> Supers { get; set; }

        /// <summary>
        /// The name of this instance.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The rules of this instance.
        /// </summary>
        [ForeignKey(nameof(SpecRuleDb.SpecId))]
        public List<SpecRuleDb> Rules { get; set; }

        /// <summary>
        /// THe children of this instance.
        /// </summary>
        [ForeignKey(nameof(ParentId))]
        public List<SpecDb> Children { get; set; }

        /// <summary>
        /// THe items of this instance.
        /// </summary>
        public List<SpecDb> Items { get; set; }

        // General ------------------------------------------

        /// <summary>
        /// Default items of this instance.
        /// </summary>
        [ForeignKey(nameof(ConditionId))]
        public ConditionDb Condition { get; set; }

        /// <summary>
        /// The class name of this instance.
        /// </summary>
        public string ConditionId { get; set; }

        /// <summary>
        /// The expression of this instance.
        /// </summary>
        [ForeignKey(nameof(ReferenceId))]
        public ReferenceDb Reference { get; set; }

        /// <summary>
        /// The class name of this instance.
        /// </summary>
        public string ReferenceId { get; set; }

        /// <summary>
        /// The description database entity of this instance.
        /// </summary>
        [ForeignKey(nameof(TitleStringDictionaryId))]
        public StringDictionaryDb Title { get; set; }

        /// <summary>
        /// The class name of this instance.
        /// </summary>
        public string TitleStringDictionaryId { get; set; }

        /// <summary>
        /// The description database entity of this instance.
        /// </summary>
        [ForeignKey(nameof(DescriptionStringDictionaryId))]
        public StringDictionaryDb Description { get; set; }

        /// <summary>
        /// The class name of this instance.
        /// </summary>
        public string DescriptionStringDictionaryId { get; set; }

        /// <summary>
        /// ID of the group of this instance.
        /// </summary>
        [ForeignKey(nameof(DetailMetaSetId))]
        public MetaSetDb Detail { get; set; }

        /// <summary>
        /// The class name of this instance.
        /// </summary>
        public string DetailMetaSetId { get; set; }

        /// <summary>
        /// The value type of this instance.
        /// </summary>
        [DefaultValue(DataValueTypes.Any)]
        public DataValueTypes ValueType { get; set; } = DataValueTypes.Any;

        /// <summary>
        /// Creation date of this instance.
        /// </summary>
        public string DefinitionUniqueName { get; set; }

        /// <summary>
        /// The class reference of this instance.
        /// </summary>
        public ClassReferenceDb ClassReference { get; set; }

        /// <summary>
        /// The class name of this instance.
        /// </summary>
        public string ClassReferenceId { get; set; }

        /// <summary>
        /// ID of the group of this instance.
        /// </summary>
        public string GroupId { get; set; }

        /// <summary>
        /// Default items of this instance.
        /// </summary>
        public List<MetaDataDb> DefaultItems { get; set; }

        /// <summary>
        /// The aliases of the entry.
        /// </summary>
        public List<string> Aliases { get; set; }

        // Items ---------------------------------

        /// <summary>
        /// The available itemization modes of this instance.
        /// </summary>
        public List<DataMode> AvailableDataModes { get; set; }

        /// <summary>
        /// Minimum item number of this instance.
        /// </summary>
        public int? MinDataItemNumber { get; set; }

        /// <summary>
        /// Maximum item number of this instance.
        /// </summary>
        public int? MaxDataItemNumber { get; set; }

        // Data 

        /// <summary>
        /// Indicates whether the instance can be allocated.
        /// </summary>
        public bool? IsAllocatable { get; set; }

        public bool? IsStatic { get; set; }

        /// <summary>
        /// Indicates whether the instance can be allocated.
        /// </summary>
        public string Label { get; set; }

        // Levels

        /// <summary>
        /// Level of accessibility of this instance.
        /// </summary>
        public AccessibilityLevels AccessibilityLevel { get; set; }

        /// <summary>
        /// The level of inheritance of this instance.
        /// </summary>
        public InheritanceLevels InheritanceLevel { get; set; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the SpecDb class.
        /// </summary>
        public SpecDb()
        {
        }

        #endregion
    }
}
