using BindOpen.Data.Meta;
using System;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// This class represents a parameter attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ParameterAttribute : BdoDataAttribute
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
