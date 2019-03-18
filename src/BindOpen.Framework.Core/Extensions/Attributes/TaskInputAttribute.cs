using System;

namespace BindOpen.Framework.Core.Extensions.Attributes
{
    /// <summary>
    /// This class represents a input detail property attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class TaskInputAttribute : DataElementAttribute
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the InputDetailPropertyAttribute class.
        /// </summary>
        public TaskInputAttribute() : base()
        {
        }

        #endregion

    }
}
