using BindOpen.Data.Elements;
using System;

namespace BindOpen.Extensions.Processing
{
    /// <summary>
    /// This class represents a input property attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class BdoTaskInputAttribute : BdoElementAttribute
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
