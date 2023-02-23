using BindOpen.Data.Items;
using System;

namespace BindOpen.Extensions.Processing
{
    /// <summary>
    /// This class represents a output property attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class BdoTaskOutputAttribute : BdoDataAttribute
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TaskOutputAttribute class.
        /// </summary>
        public BdoTaskOutputAttribute() : base()
        {
        }

        #endregion

    }
}
