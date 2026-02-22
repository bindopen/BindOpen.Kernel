using System;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// Represents an attribute used to identify method parameters that are intended 
    /// to be populated by script-based metadata.
    /// This specialization allows the script interpreter to prioritize or 
    /// pre-process these parameters before method invocation.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = true)]
    public class BdoScriptParameterAttribute : BdoParameterAttribute
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the BdoScriptParameterAttribute class.
        /// </summary>
        public BdoScriptParameterAttribute() : base()
        {
        }

        #endregion
    }
}