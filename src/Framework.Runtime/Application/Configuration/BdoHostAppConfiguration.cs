using BindOpen.Framework.Core.Application.Configuration;
using BindOpen.Framework.Core.Data.Elements;
using System.Xml.Serialization;

namespace BindOpen.Framework.Runtime.Application.Configuration
{
    /// <summary>
    /// This class represents a BindOpen host application configuration.
    /// </summary>
    [XmlType("BdoHostAppConfiguration", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot("bindopen", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class BdoHostAppConfiguration : BdoBaseConfiguration, IBdoHostAppConfiguration
    {
        // -------------------------------------------------------------
        // CONSTRUCTORS
        // -------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoHostAppConfiguration class.
        /// </summary>
        public BdoHostAppConfiguration()
            : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the BdoHostAppConfiguration class.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public BdoHostAppConfiguration(params IDataElement[] items)
            : base(items)
        {
        }

        #endregion
    }
}
