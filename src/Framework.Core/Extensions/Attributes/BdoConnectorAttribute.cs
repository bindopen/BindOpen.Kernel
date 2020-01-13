using BindOpen.Framework.Data.Items;
using System;

namespace BindOpen.Framework.Extensions.Attributes
{
    /// <summary>
    /// This class represents an attribute of connectors.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class BdoConnectorAttribute : DescribedDataItemAttribute
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ConnectorAttribute class.
        /// </summary>
        public BdoConnectorAttribute() : base()
        {
        }

        #endregion
    }
}
