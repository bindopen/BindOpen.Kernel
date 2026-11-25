using BindOpen.Data.Meta;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents a reference database entity.
    /// </summary>
    public class ReferenceDb : IBdoDb
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The expression of this instance.
        /// </summary>
        [Key]
        [Column("ItemId")]
        public string Identifier { get; set; }

        /// <summary>
        /// The kind of this instance.
        /// </summary>
        [DefaultValue(BdoReferenceKind.Expression)]
        public BdoReferenceKind Kind { get; set; } = BdoReferenceKind.Expression;

        /// <summary>
        /// The expression of this instance.
        /// </summary>
        [ForeignKey(nameof(ExpressionId))]
        public ExpressionDb Expression { get; set; }

        /// <summary>
        /// The identifie of the expression of this instance.
        /// </summary>
        public string ExpressionId { get; set; }

        /// <summary>
        /// The meta data  of this instance.
        /// </summary>
        [ForeignKey(nameof(MetaDataId))]
        public MetaDataDb MetaData { get; set; }

        /// <summary>
        /// The class name of this instance.
        /// </summary>
        public string MetaDataId { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of ReferenceDb class.
        /// </summary>
        public ReferenceDb()
        {
        }

        #endregion
    }
}