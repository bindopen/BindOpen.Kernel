using BindOpen.Meta.Elements;
using System;

namespace BindOpen.Extensions.Processing
{
    /// <summary>
    /// This class represents a input property attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class BdoTaskInputAttribute : BdoMetaAttribute
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
