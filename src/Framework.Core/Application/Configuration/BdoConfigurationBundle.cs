using BindOpen.Framework.Core.Data.Items;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Application.Configuration
{
    /// <summary>
    /// This class represents a configuration.
    /// </summary>
    [XmlType("ConfigurationBundle", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot("config.bundle", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
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
