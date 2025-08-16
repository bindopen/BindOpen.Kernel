using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a meta set database entity.
    /// </summary>
    public class MetaSetDb : IBdoDb
    {
        // -------------------------------------------------------
        // PROPERTIES
        // -------------------------------------------------------

        #region Properties

        /// <summary>
        /// The identifier of this instance.
        /// </summary>
        [Key]
        [Column("MetaSetId")]
        public string Identifier { get; set; }

        /// <summary>
        /// Name of this instance.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The elements of this instance.
        /// </summary>
        public List<MetaDataDb> Items { get; set; }

        #endregion

        // -------------------------------------------------------------
        // CONSTRUCTORS
        // -------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the MetaSetDb class.
        /// </summary>
        public MetaSetDb() : base()
        {
        }

        #endregion
    }
}
