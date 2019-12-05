using System;

namespace BindOpen.Framework.Core.Extensions.Attributes
{
    /// <summary>
    /// This class represents an attribute of formats.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class BdoFormatAttribute : BdoExtensionItemAttribute
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the FormatAttribute class.
        /// </summary>
        public BdoFormatAttribute() : base()
        {
        }

        #endregion
    }
}
