using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BindOpen.Scoping.Tasks
{
    /// <summary>
    /// This class represents a database entity task dico.
    /// </summary>
    public class TaskDictionaryDb : TBdoExtensionDictionaryDb<TaskDefinitionDb>
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Definitions of this instance.
        /// </summary>
        [ForeignKey("ExtensionDefinitionId")]
        public List<TaskDefinitionDb> Definitions { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoTaskDictionaryDb class.
        /// </summary>
        public TaskDictionaryDb()
        {
        }

        #endregion
    }
}
