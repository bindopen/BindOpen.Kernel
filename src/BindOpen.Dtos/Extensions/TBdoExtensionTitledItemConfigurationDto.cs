using BindOpen.MetaData.Items;
using BindOpen.Runtime.Definition;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Runtime.Dtos.Extensions
{
    /// <summary>
    /// This class represents a BindOpen extension titled item configuration.
    /// </summary>
    public abstract class TBdoExtensionTitledItemConfigurationDto<T>
        : TBdoExtensionItemConfigurationDto<T>
        where T : BdoExtensionItemDefinitionDto
    {
        // -----------------------------------------------
        // PROPERTIES
        // -----------------------------------------------

        #region Properties

        // General -------------------------------

        /// <summary>
        /// Title of this instance.
        /// </summary>
        [JsonPropertyName("title")]
        [XmlElement("title")]
        public DictionaryDto Title { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TBdoExtensionTitledItemConfiguration class.
        /// </summary>
        protected TBdoExtensionTitledItemConfigurationDto()
        {
        }

        #endregion
    }

}
