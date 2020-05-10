using BindOpen.Data.Items;
using System.Xml.Serialization;

namespace BindOpen.Application.Configuration
{
    /// <summary>
    /// This class represents a configuration.
    /// </summary>
    [XmlType("ConfigurationBundle", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot("config.bundle", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class BdoConfigurationBundle : DictionaryDataItem
    {
        // -------------------------------------------------------------
        // CONSTRUCTORS
        // -------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ConfigurationBundle class.
        /// </summary>
        public BdoConfigurationBundle()
            : base()
        {
        }

        #endregion
    }
}
