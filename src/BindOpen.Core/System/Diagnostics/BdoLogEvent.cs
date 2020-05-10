using BindOpen.Application.Scopes;
using BindOpen.Data.Elements;
using BindOpen.Data.Items;
using BindOpen.System.Diagnostics.Events;
using BindOpen.System.Scripting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;

namespace BindOpen.System.Diagnostics
{
    /// <summary>
    /// This class represents a log event.
    /// </summary>
    [XmlType("LogEvent", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "logEvent", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class BdoLogEvent : BdoEvent, IBdoLogEvent
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Result code of this instance.
        /// </summary>
        [XmlElement("resultCode")]
        [DefaultValue("")]
        public string ResultCode { get; set; } = null;

        /// <summary>
        /// Source of this instance.
        /// </summary>
        [XmlElement("source")]
        public string Source { get; set; } = null;

        /// <summary>
        /// Stack traces of this instance.
        /// </summary>
        [XmlArray("stack.traces")]
        [XmlArrayItem("stack.trace")]
        public List<LogEventStackTrace> StackTraces { get; set; } = null;

        // Tree ----------------------------------

        /// <summary>
        /// The log of this instance.
        /// </summary>
        [XmlElement("log")]
        public BdoLog LogDto
        {
            get => Log as BdoLog;
            set { Log = value; }
        }

        /// <summary>
        /// The log of this instance.
        /// </summary>
        [XmlIgnore()]
        public IBdoLog Log { get; set; } = null;

        /// <summary>
        /// Parent of this instance.
        /// </summary>
        [XmlIgnore()]
        public IBdoLog Parent { get; set; } = null;

        /// <summary>
        /// Root of this instance.
        /// </summary>
        [XmlIgnore()]
        public IBdoLog Root => Log?.Root;

        /// <summary>
        /// The level of this instance.
        /// </summary>
        [XmlIgnore()]
        public int Level => Parent == null ? 0 : Parent.Level + 1;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the LogEvent class.
        /// </summary>
        public BdoLogEvent() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the LogEvent class.
        /// </summary>
        /// <param name="kind">The kind of this instance.</param>
        /// <param name="title">The title of this instance.</param>
        /// <param name="criticality">The criticality of this instance.</param>
        /// <param name="description">The description of this instance.</param>
        /// <param name="resultCode">The result code of this instance.</param>
        /// <param name="source">The ExtensionDataContext of this instance.</param>
        /// <param name="date">The date of this instance.</param>
        /// <param name="id">The ID of this instance.</param>
        public BdoLogEvent(
            EventKinds kind,
            string title = null,
            BdoEventCriticality criticality = BdoEventCriticality.None,
            string description = null,
            string resultCode = null,
            string source = null,
            DateTime? date = null,
            string id = null) : base(kind, title, criticality, description, date, id)
        {
            Source = source;
            ResultCode = resultCode;
        }

        /// <summary>
        /// Instantiates a new instance of the LogEvent class.
        /// </summary>
        /// <param name="exception">The exception to consider.</param>
        /// <param name="criticality">The criticality to consider.</param>
        /// <param name="resultCode">The result code to consider.</param>
        /// <param name="source">The ExtensionDataContext to consider.</param>
        /// <param name="date">The date of this instance.</param>
        /// <param name="id">The ID of this instance.</param>
        public BdoLogEvent(
            Exception exception,
            BdoEventCriticality criticality = BdoEventCriticality.None,
            string resultCode = null,
            string source = null,
            DateTime? date = null,
            string id = null) : base(exception, criticality, date, id)
        {
            Source = source;
            ResultCode = resultCode;
        }

        /// <summary>
        /// Instantiates a new instance of the LogEvent class.
        /// </summary>
        /// <param name="event1">The event to consider.</param>
        public BdoLogEvent(BdoEvent event1)
        {
            if (event1 != null)
            {
                Criticality = event1.Criticality;
                Description = event1.Description?.Clone() as DictionaryDataItem;
                Detail = event1.Detail?.Clone() as DataElementSet;
                Kind = event1.Kind;
                LongDescription = event1.LongDescription?.Clone() as DictionaryDataItem;
                Title = event1.Title?.Clone() as DictionaryDataItem;
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
            return Clone(null, Array.Empty<string>());
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <param name="parent">The log to consider.</param>
        /// <returns>Returns a cloned instance.</returns>
        public IBdoLogEvent Clone(IBdoLog parent, params string[] areas)
        {
            var cloned = base.Clone(areas) as BdoLogEvent;

            cloned.Parent = parent;
            cloned.Log = Log?.Clone(parent) as BdoLog;
            cloned.Detail = Detail?.Clone<DataElementSet>();
            //cloned.StackTraces = StackTraces?.Select(p=> p.Clone<LogEventStackTrace>()).ToList();

            return cloned;
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <param name="parent">The parent to consider.</param>
        /// <returns>Returns a cloned instance.</returns>
        public T Clone<T>(IBdoLog parent, params string[] areas) where T : class
        {
            return Clone(parent, areas) as T;
        }

        /// <summary>
        /// Gets the warnings, errors or exceptions of this instance.
        /// </summary>
        /// <param name="isRecursive">Indicate whether the search is recursive.</param>
        /// <param name="kinds">The kinds to consider.</param>
        /// <returns>True if this instance has the specified events. False otherwise.</returns>
        public EventKinds GetMaxEventKind(
            bool isRecursive = true,
            params EventKinds[] kinds)
        {
            EventKinds eventKind = EventKinds.None;

            if (Log != null)
            {
                eventKind = Log.GetMaxEventKind(isRecursive, kinds);
            }

            if (eventKind == EventKinds.None)
            {
                eventKind = Kind;
            }

            return eventKind;
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
            Log?.UpdateStorageInfo(log);
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The set of script variables to consider.</param>
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(IBdoScope scope = null, IScriptVariableSet scriptVariableSet = null, IBdoLog log = null)
        {
            Log?.UpdateRuntimeInfo(scope, scriptVariableSet, log);

            base.UpdateRuntimeInfo(scope, scriptVariableSet, log);
        }

        #endregion
    }
}
