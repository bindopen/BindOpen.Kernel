using BindOpen.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BindOpen.Scoping
{
    /// <summary>
    /// This class represents the group of BindOpen extension items.
    /// </summary>
    public class ExtensionGroupDb : IBdoDb
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The expression of this instance.
        /// </summary>
        [Key]
        [Column("ExtensionGroupId")]
        public string Identifier { get; set; }

        /// <summary>
        /// Description of this instance.
        /// </summary>
        [ForeignKey(nameof(DescriptionStringDictionaryId))]
        public StringDictionaryDb Description { get; set; }

        /// <summary>
        /// The class name of this instance.
        /// </summary>
        public string DescriptionStringDictionaryId { get; set; }

        /// <summary>
        /// Sub groups of this instance.
        /// </summary>
        public List<ExtensionGroupDb> SubGroups { get; set; }

        [NotMapped]
        public List<ExtensionGroupDb> Supers { get; set; }

        [NotMapped]
        public List<ExtensionDefinitionDb> Definitions { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoExtensionGroupDb class.
        /// </summary>
        public ExtensionGroupDb()
        {
        }

        #endregion
    }
}