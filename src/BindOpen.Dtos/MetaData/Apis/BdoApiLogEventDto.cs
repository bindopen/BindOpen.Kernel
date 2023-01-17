using BindOpen.MetaData.Items;
using BindOpen.Logging;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace BindOpen.MetaData.Apis
{
    /// <summary>
    /// This class represents a Api log event DTO.
    /// </summary>
    [XmlType("ApiLogEventDto", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot(ElementName = "event", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
    public class BdoApiLogEventDto : NamedDataItemDto, IDisplayNamed, IDescribed
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Kind of this instance.
        /// </summary>
        [JsonPropertyName("kind")]
        [XmlAttribute("kind")]
        [DefaultValue(EventKinds.None)]
        public EventKinds Kind { get; set; } = EventKinds.Other;

        /// <summary>
        /// Creation date of this instance.
        /// </summary>
        [JsonPropertyName("date")]
        [XmlAttribute("date")]
        public string Date { get; set; }

        /// <summary>
        /// Criticality of this instance.
        /// </summary>
        [JsonPropertyName("criticality")]
        [XmlElement("criticality")]
        [DefaultValue(Criticalities.None)]
        public Criticalities Criticality { get; set; } = Criticalities.None;

        /// <summary>
        /// Result code of this instance.
        /// </summary>
        [JsonPropertyName("resultCode")]
        [XmlElement("resultCode")]
        [DefaultValue("")]
        public string ResultCode { get; set; }

        /// <summary>
        /// The display name of this instance.
        /// </summary>
        [JsonPropertyName("displayName")]
        [XmlElement("displayName")]
        [DefaultValue("")]
        public string DisplayName { get; set; }

        /// <summary>
        /// The description of this instance.
        /// </summary>
        [JsonPropertyName("description")]
        [XmlElement("description")]
        [DefaultValue("")]
        public string Description { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoApiLogEventDto class.
        /// </summary>
        public BdoApiLogEventDto()
        {
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// 
        /// </summary>
        /// <param name="displayName"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IDisplayNamed WithDisplayName(string displayName)
        {
            DisplayName = displayName;
            return this;
        }

        #endregion
    }
}
