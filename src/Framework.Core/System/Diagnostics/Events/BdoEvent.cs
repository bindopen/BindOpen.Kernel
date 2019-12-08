using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.System.Scripting;
using System;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.System.Diagnostics.Events
{
    /// <summary>
    /// This class represents an event.
    /// </summary>
    [Serializable()]
    [XmlType("Event", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "event", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    [XmlInclude(typeof(BdoConditionalEvent))]
    public class BdoEvent : DescribedDataItem, BdoIEvent
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
        /// Specification of the Date property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool DateSpecified => base.CreationDateSpecified;

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
        /// Specification of the CreationDate property of this instance.
        /// </summary>
        [XmlIgnore()]
        public new bool CreationDateSpecified => false;

        /// <summary>
        /// Long description of this instance.
        /// </summary>
        [XmlElement("longDescription")]
        public DictionaryDataItem LongDescription { get; set; } = null;

        /// <summary>
        /// Specification of the LongDescription property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool LongDescriptionSpecified => this.LongDescription != null && (this.LongDescription.AvailableKeysSpecified || this.LongDescription.ValuesSpecified || this.LongDescription.SingleValueSpecified);

        /// <summary>
        /// Detail of this instance.
        /// </summary>
        [XmlElement("detail")]
        public DataElementSet Detail { get; set; } = null;

        /// <summary>
        /// Specification of the Detail property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool DetailSpecified => this.Detail != null && (this.Detail.ElementsSpecified);

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
            this.Id = (id ?? this.Id);
            this.CreationDate = (date ?? DateTime.Now).ToString(StringHelper.__DateFormat);

            this.Kind = kind;
            this.Criticality = criticality;
            this.Title = string.IsNullOrEmpty(title) ? null : new DictionaryDataItem(title);
            this.Description = string.IsNullOrEmpty(description) ? null : new DictionaryDataItem(description);
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
            this.Id = (id ?? this.Id);
            this.CreationDate = ObjectHelper.ToString((date ?? global::System.DateTime.Now));

            this.Kind = EventKinds.Exception;
            this.Criticality = criticality;

            if (exception != null)
            {
                this.SetTitle(exception.Message);
                this.SetDescription(exception.ToString());
                this.LongDescription = new DictionaryDataItem(exception.ToString());
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
        public override object Clone()
        {
            var cloned = base.Clone() as BdoEvent;

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
            return aEvent?.GetTitle();
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

            this.Detail?.UpdateStorageInfo(log);
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The set of script variables to consider.</param>
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(IBdoScope scope = null, IBdoScriptVariableSet scriptVariableSet = null, IBdoLog log = null)
        {
            base.UpdateRuntimeInfo(scope, scriptVariableSet, log);

            Detail?.UpdateRuntimeInfo(scope, scriptVariableSet, log);
        }

        #endregion
    }
}
