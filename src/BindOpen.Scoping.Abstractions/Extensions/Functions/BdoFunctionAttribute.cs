using System;

namespace BindOpen.Scoping.Extensions.Functions
{
    /// <summary>
    /// This class represents a script word attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true)]
    public class BdoFunctionAttribute : MetaExtensionAttribute
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoScriptwordAttribute class.
        /// </summary>
        public BdoFunctionAttribute() : base()
        {
        }

        public BdoFunctionAttribute(string name) : base(name)
        {
        }

        #endregion
    }
}
