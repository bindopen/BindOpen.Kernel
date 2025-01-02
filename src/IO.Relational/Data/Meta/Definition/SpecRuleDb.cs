using BindOpen.Data.Conditions;
using BindOpen.Logging;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a data key value.
    /// </summary>
    public class SpecRuleDb
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// Identifier of this instance.
        /// </summary>
        [Key]
        [Column("SpecRuleId")]
        public string Identifier { get; set; }

        public string SpecId { get; set; }

        /// <summary>
        /// The kind of this instance.
        /// </summary>
        public BdoSpecRuleKinds Kind { get; set; }

        /// <summary>
        /// The group identifier of this instance.
        /// </summary>
        public string GroupId { get; set; }

        /// <summary>
        /// Values of this instance.
        /// </summary>
        [ForeignKey(nameof(ValueMetaDataId))]
        public MetaDataDb Value { get; set; }

        /// <summary>
        /// The class name of this instance.
        /// </summary>
        public string ValueMetaDataId { get; set; }

        /// <summary>
        /// The reference of this instance.
        /// </summary>
        [ForeignKey(nameof(ReferenceId))]
        public ReferenceDb Reference { get; set; }

        /// <summary>
        /// The class name of this instance.
        /// </summary>
        public string ReferenceId { get; set; }

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
        /// The result code of this instance.
        /// </summary>
        public string ResultCode { get; set; }

        public EventKinds ResultEventKind { get; set; }

        public string ResultTitle { get; set; }

        public string ResultDescription { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of SpecRuleDb class.
        /// </summary>
        public SpecRuleDb()
        {
        }

        #endregion
    }
}
