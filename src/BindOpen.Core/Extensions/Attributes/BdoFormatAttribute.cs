using BindOpen.Data.Items;
using System;

namespace BindOpen.Extensions.Attributes
{
    /// <summary>
    /// This class represents an attribute of formats.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class BdoFormatAttribute : DescribedDataItemAttribute
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
