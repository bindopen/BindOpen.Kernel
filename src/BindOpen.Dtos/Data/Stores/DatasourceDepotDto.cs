using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data.Stores
{
    /// <summary>
    /// This class represents a datasource depot.
    /// </summary>
    [XmlType("DatasourceDepot", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
    [XmlRoot(ElementName = "datasource.depot", Namespace = "https://storage.bindopen.org/xsd/bindopen", IsNullable = false)]
    public class BdoDatasourceDepotDto : IDto, IIdentified
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// ID of this instance.
        /// </summary>
        [JsonPropertyName("id")]
        [XmlAttribute("id")]
        public string Id { get; set; }

        /// <summary>
        /// The sources of this instance.
        /// </summary>
        [JsonPropertyName("sources")]
        [XmlElement("source")]
        public List<DatasourceDto> Sources { get; set; }

        #endregion


        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the DatasourceDepotDto class.
        /// </summary>
        public BdoDatasourceDepotDto()
        {
        }

        #endregion
    }
}

