using BindOpen.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BindOpen.Scoping
{
    /// <summary>
    /// This class represents the definition of a library.
    /// </summary>
    public class PackageDefinitionDb : IBdoDb
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The expression of this instance.
        /// </summary>
        [Key]
        [Column("PackageDefinitionId")]
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
        /// Name of the group of this instance.
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Name of the assembly where the library can be loaded.
        /// </summary>
        public string AssemblyName { get; set; }

        /// <summary>
        /// Root name space of this intance.
        /// </summary>
        public string RootNamespace { get; set; }

        // Files -------------------------------------

        /// <summary>
        /// File name of this instance.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Names of the using assembly files of this instance.
        /// </summary>
        public List<string> UsingAssemblyFileNames { get; set; }

        // Dictionary full names -------------------------------------

        /// <summary>
        /// Dictionary full names of this instance.
        /// </summary>
        [ForeignKey(nameof(ItemIndexFullNameDictionaryId))]
        public StringDictionaryDb ItemIndexFullNameDictionary { get; set; }

        /// <summary>
        /// The class name of this instance.
        /// </summary>
        public string ItemIndexFullNameDictionaryId { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of BdoPackageDefinitionDb class.
        /// </summary>
        public PackageDefinitionDb()
        {
        }

        #endregion
    }
}
