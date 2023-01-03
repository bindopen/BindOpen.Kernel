using BindOpen.Data.Elements;
using System;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// This class represents a parameter attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ParameterAttribute : BdoElementAttribute
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ParameterAttribute class.
        /// </summary>
        public ParameterAttribute() : base()
        {
        }

        #endregion
    }
}
