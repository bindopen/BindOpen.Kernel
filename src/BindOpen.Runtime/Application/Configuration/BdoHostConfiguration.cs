using System.Xml.Serialization;

namespace BindOpen.Application.Configuration
{
    /// <summary>
    /// This class represents a BindOpen host application configuration.
    /// </summary>
    [XmlType("BdoHostConfiguration", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot("bindopen", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
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

        #endregion
    }
}
