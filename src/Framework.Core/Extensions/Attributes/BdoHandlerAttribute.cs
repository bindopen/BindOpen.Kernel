using System;

namespace BindOpen.Framework.Core.Extensions.Attributes
{
    /// <summary>
    /// This class represents an attribute of handlers.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class BdoHandlerAttribute : BdoExtensionItemAttribute
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the HandlerAttribute class.
        /// </summary>
        public BdoHandlerAttribute() : base()
        {
        }

        #endregion
    }
}
