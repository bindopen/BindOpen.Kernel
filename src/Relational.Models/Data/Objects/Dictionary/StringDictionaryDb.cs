using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents a string dictionary item.
    /// </summary>
    /// <example>Titles, Descriptions.</example>
    /// <seealso cref="KeyValuePairDb"/>
    public class StringDictionaryDb : IBdoDb
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The expression of this instance.
        /// </summary>
        [Key]
        [Column("ItemId")]
        public string Identifier { get; set; }

        /// <summary>
        /// The values of this instance.
        /// </summary>
        [NotMapped]
        public List<KeyValuePairDb> Values { get; set; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the StringDictionaryDb class. 
        /// </summary>
        public StringDictionaryDb()
        {
        }

        #endregion
    }

}
