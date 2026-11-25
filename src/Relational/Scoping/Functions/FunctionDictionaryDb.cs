using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BindOpen.Scoping.Functions
{
    /// <summary>
    /// This class represents a database entity script word dico.
    /// </summary>
    public class FunctionDictionaryDb : TBdoExtensionDictionaryDb<FunctionDefinitionDb>
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The definition class of this instance.
        /// </summary>
        public string DefinitionClass { get; set; }

        /// <summary>
        /// Definitions of this instance.
        /// </summary>
        [ForeignKey("ExtensionDefinitionId")]
        public List<FunctionDefinitionDb> Definitions { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoFunctionDictionaryDb class.
        /// </summary>
        public FunctionDictionaryDb()
        {
        }

        #endregion
    }
}
