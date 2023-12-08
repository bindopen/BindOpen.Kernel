using System;

namespace BindOpen.Kernel.Data.Meta
{
    /// <summary>
    /// This class represents a data element attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = true)]
    public class BdoScriptParameterAttribute : BdoParameterAttribute
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoScriptParameterAttribute class.
        /// </summary>
        public BdoScriptParameterAttribute() : base()
        {
        }

        #endregion
    }
}
