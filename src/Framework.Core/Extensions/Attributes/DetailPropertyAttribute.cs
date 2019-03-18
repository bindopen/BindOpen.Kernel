using System;

namespace BindOpen.Framework.Core.Extensions.Attributes
{
    /// <summary>
    /// This class represents a detail property attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DetailPropertyAttribute : DataElementAttribute
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DetailPropertyAttribute class.
        /// </summary>
        public DetailPropertyAttribute() : base()
        {
        }

        #endregion

    }
}
