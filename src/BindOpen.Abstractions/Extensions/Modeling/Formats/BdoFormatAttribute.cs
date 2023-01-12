using BindOpen.Meta.Items;
using System;

namespace BindOpen.Extensions.Modeling
{
    /// <summary>
    /// This class represents an attribute of formats.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class BdoFormatAttribute : TitledDescribedDataItemAttribute
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
