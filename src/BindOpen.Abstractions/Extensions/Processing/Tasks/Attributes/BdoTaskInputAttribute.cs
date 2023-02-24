using BindOpen.Data.Items;
using System;

namespace BindOpen.Extensions.Processing
{
    /// <summary>
    /// This class represents a input property attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class BdoTaskInputAttribute : BdoPropertyAttribute
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TaskInputAttribute class.
        /// </summary>
        public BdoTaskInputAttribute() : base()
        {
        }

        #endregion
    }
}
