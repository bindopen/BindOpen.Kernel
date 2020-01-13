using BindOpen.Framework.Data.Elements;
using System.Xml.Serialization;

namespace BindOpen.Framework.Application.Configuration
{
    /// <summary>
    /// This class represents a BindOpen host application configuration.
    /// </summary>
    [XmlType("BdoHostConfiguration", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot("bindopen", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class BdoHostConfiguration : BdoBaseConfiguration, IBdoHostConfiguration
    {
        // -------------------------------------------------------------
        // CONSTRUCTORS
        // -------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoHostConfiguration class.
        /// </summary>
        public BdoHostConfiguration()
            : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the BdoHostConfiguration class.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public BdoHostConfiguration(params IDataElement[] items)
            : base(items)
        {
        }

        #endregion
    }
}
