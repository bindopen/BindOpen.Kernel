using BindOpen.Meta;
using BindOpen.Extensions.Scripting;

namespace BindOpen.Runtime.Definition
{
    /// <summary>
    /// This class represents a DTO script word dico.
    /// </summary>
    public class BdoScriptwordDictionary : TBdoExtensionDictionary<IBdoScriptwordDefinition>, IBdoScriptwordDictionary
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
        /// <param name="definitionName">The defintion name to consider.</param>
        /// <param name="methodName">The name of the method to consider.</param>
        /// <param name="parent">The parent to consider.</param>
        /// <returns></returns>
        public IBdoScriptwordDefinition GetDefinition(
            string definitionName,
            string methodName,
            IBdoScriptwordDefinition parent = null)
        {
            if (Definitions != null)
            {
                foreach (var childDefinition in Definitions)
                {
                    if ((!string.IsNullOrEmpty(definitionName) && childDefinition.Name.BdoKeyEquals(definitionName))
                        || (string.IsNullOrEmpty(definitionName) && childDefinition.RuntimeFunctionName.BdoKeyEquals(methodName)))
                    {
                        return childDefinition;
                    }
                    else
                    {
                        IBdoScriptwordDefinition definition;
                        if ((definition = GetDefinition(definitionName, methodName, childDefinition)) != null)
                        {
                            return definition;
                        }
                    }
                }
            }

            return null;
        }

        #endregion
    }
}
