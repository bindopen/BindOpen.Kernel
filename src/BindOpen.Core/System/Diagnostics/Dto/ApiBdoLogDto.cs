using BindOpen.Data.Common;
using BindOpen.Data.Helpers.Strings;
using BindOpen.Data.Items;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace BindOpen.System.Diagnostics.Dto
{
    /// <summary>
    /// This class represents a log.
    /// </summary>
    [XmlType("ApiBdoLogDto", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "log", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class ApiBdoLogDto : NamedDataItem, IDisplayNamed, IDescribed
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The display name of this instance.
        /// </summary>
        [XmlElement("displayName")]
        [DefaultValue("")]
        public string DisplayName { get; set; }

        /// <summary>
        /// The description of this instance.
        /// </summary>
        [XmlElement("description")]
        [DefaultValue("")]
        public string Description { get; set; }

        /// <summary>
        /// The events of this instance.
        /// </summary>
        [XmlArray("events")]
        [XmlArrayItem("event")]
        public List<ApiBdoLogEventDto> Events { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ApiLogDto class.
        /// </summary>
        public ApiBdoLogDto() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the ApiLogDto class.
        /// </summary>
        /// <param name="displayName"></param>
        /// <param name="description"></param>
        /// <param name="date"></param>
        /// <param name="name"></param>
        /// <param name="id"></param>
        public ApiBdoLogDto(
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
