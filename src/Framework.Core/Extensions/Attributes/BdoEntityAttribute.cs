using BindOpen.Framework.Data.Items;
using System;

namespace BindOpen.Framework.Extensions.Attributes
{
    /// <summary>
    /// This class represents an attribute of entities.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class BdoEntityAttribute : DescribedDataItemAttribute
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the EntityAttribute class.
        /// </summary>
        public BdoEntityAttribute() : base()
        {
        }

        #endregion
    }
}
