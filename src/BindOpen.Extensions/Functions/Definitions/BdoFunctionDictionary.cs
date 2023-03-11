using BindOpen.Data.Helpers;

namespace BindOpen.Extensions.Functions
{
    /// <summary>
    /// This class represents a DTO function dico.
    /// </summary>
    public class BdoFunctionDictionary : TBdoExtensionDictionary<IBdoFunctionDefinition>, IBdoFunctionDictionary
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoFunctionDictionary class.
        /// </summary>
        public BdoFunctionDictionary()
        {
        }

        #endregion

        // ------------------------------------------
        // IBdoFunctionDictionary Implementation
        // ------------------------------------------

        #region IBdoFunctionDictionary

        /// <summary>
        /// Gets the specified definition.
        /// </summary>
        /// <param key="definitionName">The defintion name to consider.</param>
        /// <param key="methodName">The name of the method to consider.</param>
        /// <param key="parent">The parent to consider.</param>
        /// <returns></returns>
        public IBdoFunctionDefinition GetDefinition(
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
