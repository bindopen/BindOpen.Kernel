using BindOpen.Meta.Items;
using System;

namespace BindOpen.Extensions
{
    /// <summary>
    /// This class represents an attribute of handlers.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class BdoHandlerAttribute : TitledDescribedDataItemAttribute
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
