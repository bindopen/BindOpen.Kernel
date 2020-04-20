using BindOpen.Data.Helpers.Strings;
using BindOpen.Data.Items;
using BindOpen.System.Diagnostics.Events;
using System;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;

namespace BindOpen.System.Diagnostics.Dto
{
    /// <summary>
    /// This class represents a log event.
    /// </summary>
    [XmlType("ApiLogEventDto", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "event", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class ApiBdoLogEventDto : NamedDataItem, IDisplayNamed, IDescribed
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Kind of this instance.
        /// </summary>
        [XmlAttribute("kind")]
        [DefaultValue(EventKinds.None)]
        public EventKinds Kind { get; set; } = EventKinds.Other;

        /// <summary>
        /// Creation date of this instance.
        /// </summary>
        [XmlAttribute("date")]
        public string Date
        {
            get { return base.CreationDate; }
            set
            {
                base.CreationDate = value;
            }
        }

        /// <summary>
        /// Creation date of this instance.
        /// </summary>
        [XmlIgnore()]
        public new string CreationDate
        {
            get => base.CreationDate;
            set => base.CreationDate = value;
        }

        /// <summary>
        /// Criticality of this instance.
        /// </summary>
        [XmlElement("criticality")]
        [DefaultValue(BdoEventCriticality.None)]
        public BdoEventCriticality Criticality { get; set; } = BdoEventCriticality.None;

        /// <summary>
        /// Result code of this instance.
        /// </summary>
        [XmlElement("resultCode")]
        [DefaultValue("")]
        public string ResultCode { get; set; } = null;

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

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ApiLogEventDto class.
        /// </summary>
        public ApiBdoLogEventDto() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the ApiLogEventDto class.
        /// </summary>
        /// <param name="kind">The kind of this instance.</param>
        /// <param name="displayName">The title of this instance.</param>
        /// <param name="criticality">The criticality of this instance.</param>
        /// <param name="description">The description of this instance.</param>
        /// <param name="resultCode">The result code of this instance.</param>
        /// <param name="date">The date of this instance.</param>
        /// <param name="name">The name of this instance.</param>
        /// <param name="id">The ID of this instance.</param>
        public ApiBdoLogEventDto(
            EventKinds kind,
            string displayName = null,
            BdoEventCriticality criticality = BdoEventCriticality.None,
            string description = null,
            string resultCode = null,
            DateTime? date = null,
            string name = null,
            string id = null) : base(name, "event_", id, date)
        {
            Kind = kind;
            DisplayName = displayName;
            Criticality = criticality;
            Description = description;
            ResultCode = resultCode;
            CreationDate = date.GetString();
            Name = name;
            Id = id;
        }

        #endregion
    }
}
