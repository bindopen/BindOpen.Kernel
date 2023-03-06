using System;

namespace BindOpen.Extensions.Modeling
{
    /// <summary>
    /// This class represents an attribute of routines.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class BdoFunctionAttribute : MetaExtensionAttribute
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoFunctionAttribute class.
        /// </summary>
        public BdoFunctionAttribute() : base()
        {
        }

        #endregion
    }
}
