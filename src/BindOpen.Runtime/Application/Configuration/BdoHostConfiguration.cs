using System.Xml.Serialization;

namespace BindOpen.Application.Configuration
{
    /// <summary>
    /// This class represents a BindOpen host application configuration.
    /// </summary>
    [XmlType("BdoHostConfiguration", Namespace = "https://storage.bindopen.org/pgrkhpym/docs/code/xsd/bindopen")]
    [XmlRoot("bindopen", Namespace = "https://storage.bindopen.org/pgrkhpym/docs/code/xsd/bindopen", IsNullable = false)]
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
