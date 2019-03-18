using System;

namespace BindOpen.Framework.Core.Extensions.Attributes
{
    /// <summary>
    /// This class represents a output detail property attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class TaskOutputAttribute : DataElementAttribute
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the OutputAttribute class.
        /// </summary>
        public TaskOutputAttribute() : base()
        {
        }

        #endregion

    }
}
