using BindOpen.Application.Scopes;
using BindOpen.Data.Common;
using BindOpen.Data.Elements;
using BindOpen.Data.Helpers.Objects;
using BindOpen.Data.Helpers.Strings;
using BindOpen.Data.Items;
using BindOpen.System.Scripting;
using System;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;

namespace BindOpen.System.Diagnostics.Events
{
    /// <summary>
    /// This class represents an event.
    /// </summary>
    [XmlType("Event", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "event", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    [XmlInclude(typeof(BdoConditionalEvent))]
    public class BdoEvent : DescribedDataItem, IBdoEvent
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
        [DefaultValue("")]
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
        /// Long description of this instance.
        /// </summary>
        [XmlElement("longDescription")]
        public DictionaryDataItem LongDescription { get; set; } = null;

        /// <summary>
        /// Detail of this instance.
        /// </summary>
        [XmlElement("detail")]
        public DataElementSet Detail { get; set; } = null;

        /// <summary>
        /// Criticality of this instance.
        /// </summary>
        [XmlElement("criticality")]
        [DefaultValue(BdoEventCriticality.None)]
        public BdoEventCriticality Criticality { get; set; } = BdoEventCriticality.None;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the LogEvent class.
        /// </summary>
        public BdoEvent() : base(null, "Event_", null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the LogEvent class.
        /// </summary>
        /// <param name="kind">The kind of this instance.</param>
        /// <param name="title">The title of this instance.</param>
        /// <param name="criticality">The criticality of this instance.</param>
        /// <param name="description">The description of this instance.</param>
        /// <param name="date">The date of this instance.</param>
        /// <param name="id">The ID of this instance.</param>
        public BdoEvent(
            EventKinds kind,
            string title = null,
            BdoEventCriticality criticality = BdoEventCriticality.None,
            string description = null,
            DateTime? date = null,
            string id = null) : this()
        {
            Id = (id ?? Id);
            CreationDate = (date ?? DateTime.Now).ToString(StringHelper.__DateFormat);

            Kind = kind;
            Criticality = criticality;
            Title = string.IsNullOrEmpty(title) ? null : ItemFactory.CreateDictionary(title);
            Description = string.IsNullOrEmpty(description) ? null : ItemFactory.CreateDictionary(description);
        }

        /// <summary>
        /// Instantiates a new instance of the LogEvent class.
        /// </summary>
        /// <param name="exception">The exception to consider.</param>
        /// <param name="criticality">The criticality to consider.</param>
        /// <param name="date">The date of this instance.</param>
        /// <param name="id">The ID of this instance.</param>
        public BdoEvent(
            Exception exception,
            BdoEventCriticality criticality = BdoEventCriticality.None,
            DateTime? date = null,
            string id = null) : this()
        {
            Id = (id ?? Id);
            CreationDate = (date ?? DateTime.Now).ToString(DataValueTypes.Date);

            Kind = EventKinds.Exception;
            Criticality = criticality;

            if (exception != null)
            {
                WithTitle(exception.Message);
                WithDescription(exception.ToString());
                LongDescription = ItemFactory.CreateDictionary(exception.ToString());
            }
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns a cloned instance.</returns>
        public override object Clone(params string[] areas)
        {
            var cloned = base.Clone(areas) as BdoEvent;

            cloned.Detail = Detail?.Clone<DataElementSet>();

            return cloned;
        }

        #endregion

        // ------------------------------------------
        // CONVERTERS
        // ------------------------------------------

        #region Converters

        /// <summary>
        /// Converts from string.
        /// </summary>
        /// <param name="st">The string to consider.</param>
        public static implicit operator BdoEvent(string st)
        {
            return new BdoLogEvent(EventKinds.Message, st);
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <param name="aEvent">The event to consider.</param>
        public static implicit operator string(BdoEvent aEvent)
        {
            return aEvent?.GetTitleText();
        }

        #endregion

        // ------------------------------------------
        // SERIALIZATION / UNSERIALIZATION
        // ------------------------------------------

        #region Serialization_Unserialization

        /// <summary>
        /// Updates information for storage.
        /// </summary>
        /// <param name="log">The log to update.</param>
        public override void UpdateStorageInfo(IBdoLog log = null)
        {
            base.UpdateStorageInfo(log);

            Detail?.UpdateStorageInfo(log);
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The set of script variables to consider.</param>
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(IBdoScope scope = null, IScriptVariableSet scriptVariableSet = null, IBdoLog log = null)
        {
            Detail?.UpdateRuntimeInfo(scope, scriptVariableSet, log);

            base.UpdateRuntimeInfo(scope, scriptVariableSet, log);
        }

        #endregion
    }
}
