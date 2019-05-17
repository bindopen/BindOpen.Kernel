using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Dto;

namespace BindOpen.Framework.Core.System.Diagnostics.Dto
{
    /// <summary>
    /// This class represents a log.
    /// </summary>
    [Serializable()]
    [XmlType("Log", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "log", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class ApiLogDto : NamedDataItem, IDisplayNamed, IDescribed
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The display name of this instance.
        /// </summary>
        [XmlElement("displayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Specification of the DisplayName property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool DisplayNameSpecified => !string.IsNullOrEmpty(DisplayName);

        /// <summary>
        /// The description of this instance.
        /// </summary>
        [XmlElement("description")]
        public string Description { get; set; }

        /// <summary>
        /// Specification of the Description property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool DescriptionSpecified => !string.IsNullOrEmpty(Description);

        /// <summary>
        /// The events of this instance.
        /// </summary>
        [XmlArray("events")]
        [XmlArrayItem("event")]
        public List<ApiLogEventDto> Events { get; set; }

        /// <summary>
        /// Specification of the Description property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool EventsSpecified => Events?.Count > 0;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ApiLogDto class.
        /// </summary>
        public ApiLogDto() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the ApiLogDto class.
        /// </summary>
        /// <param name="kind">The kind of this instance.</param>
        /// <param name="title">The title of this instance.</param>
        /// <param name="criticality">The criticality of this instance.</param>
        /// <param name="description">The description of this instance.</param>
        /// <param name="resultCode">The result code of this instance.</param>
        /// <param name="date">The date of this instance.</param>
        /// <param name="id">The ID of this instance.</param>
        public ApiLogDto(
            string displayName = null,
            string description = null,
            DateTime? date = null,
            string name = null,
            string id = null) : base(name, "log_", id, date)
        {
            DisplayName = displayName;
            Description = description;
            CreationDate = date.GetString();
            Name = name;
            Id = id;
        }

        #endregion
    }
}
