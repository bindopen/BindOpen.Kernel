using BindOpen.System.Scoping.Tasks;

namespace BindOpen.System.Scoping.Tasks
{
    /// <summary>
    /// This class represents a DTO task dico.
    /// </summary>
    public class BdoTaskDictionary : TBdoExtensionDictionary<IBdoTaskDefinition>, IBdoTaskDictionary
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoTaskDictionary class.
        /// </summary>
        public BdoTaskDictionary()
        {
        }

        #endregion
    }
}
