using BindOpen.MetaData.Items;
using System;

namespace BindOpen.Extensions.Modeling
{
    /// <summary>
    /// This class represents an attribute of entities.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class BdoEntityAttribute : TitledDescribedDataItemAttribute
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
