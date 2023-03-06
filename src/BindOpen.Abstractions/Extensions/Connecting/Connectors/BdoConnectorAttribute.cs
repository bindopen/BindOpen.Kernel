using System;

namespace BindOpen.Extensions.Connecting
{
    /// <summary>
    /// This class represents an attribute of connectors.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class BdoConnectorAttribute : MetaExtensionAttribute
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
