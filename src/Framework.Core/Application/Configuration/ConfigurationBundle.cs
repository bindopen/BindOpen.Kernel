using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Items.Dictionary;

namespace BindOpen.Framework.Core.Application.Configuration
{
    /// <summary>
    /// This class represents a configuration.
    /// </summary>
    [XmlType("ConfigurationBundle", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot("config.bundle", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class ConfigurationBundle : DictionaryDataItem
    {
        // -------------------------------------------------------------
        // CONSTRUCTORS
        // -------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ConfigurationBundle class.
        /// </summary>
        public ConfigurationBundle()
            : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the ConfigurationBundle class.
        /// </summary>
        /// <param name="values">The values to consider.</param>
        public ConfigurationBundle(params DataKeyValue[] values) : base(values)
        {
        }

        #endregion
    }
}
