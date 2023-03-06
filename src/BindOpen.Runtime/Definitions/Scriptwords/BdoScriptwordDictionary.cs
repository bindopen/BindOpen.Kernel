using BindOpen.Data.Helpers;

namespace BindOpen.Runtime.Definitions
{
    /// <summary>
    /// This class represents a DTO script word dico.
    /// </summary>
    public class BdoScriptwordDictionary : TBdoExtensionDictionary<IBdoScriptwordDefinition>,
        IBdoScriptwordDictionary
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The definition class of this instance.
        /// </summary>
        public string DefinitionClass { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoScriptwordDictionary class.
        /// </summary>
        public BdoScriptwordDictionary()
        {
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Gets the specified definition.
        /// </summary>
        /// <param key="definitionName">The defintion name to consider.</param>
        /// <param key="methodName">The name of the method to consider.</param>
        /// <param key="parent">The parent to consider.</param>
        /// <returns></returns>
        public IBdoScriptwordDefinition GetDefinition(
            string definitionName,
            string methodName)
        {
            foreach (var childDefinition in this)
            {
                if (!string.IsNullOrEmpty(definitionName) && childDefinition.Name.BdoKeyEquals(definitionName)
                    || string.IsNullOrEmpty(definitionName) && childDefinition.RuntimeFunctionName.BdoKeyEquals(methodName))
                {
                    return childDefinition;
                }
            }

            return null;
        }

        #endregion
    }
}
