using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items.Dictionary;
using BindOpen.Framework.Core.System.Diagnostics.Events;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.System.Diagnostics
{
    /// <summary>
    /// This class represents a log event.
    /// </summary>
    [Serializable()]
    [XmlType("LogEvent", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "logEvent", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class LogEvent : Event, ILogEvent
    {
        // ------------------------------------------
        // STRUCTURES
        // ------------------------------------------

        #region Structures

        /// <summary>
        /// This structures defines the stack trace of a task result.
        /// </summary>
        [Serializable()]
        [XmlType("LogEventStackTrace", Namespace = "http://meltingsoft.com/bindopen/xsd")]
        public struct LogEventStackTrace
        {
            /// <summary>
            /// The name of the full class.
            /// </summary>
            [XmlElement("fullClassName")]
            public string FullClassName;

            /// <summary>
            /// The name of the called method.
            /// </summary>
            [XmlElement("methodName")]
            public string MethodName;

            /// <summary>
            /// Parameters of the called method.
            /// </summary>
            [XmlElement("methodParams")]
            public string MethodParams;

            /// <summary>
            /// Path of the called file.
            /// </summary>
            [XmlElement("filePath")]
            public string FilePath;

            /// <summary>
            /// Called line number.
            /// </summary>
            [XmlElement("lineNumber")]
            public string LineNumber;
        }

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Result code of this instance.
        /// </summary>
        [XmlElement("resultCode")]
        public string ResultCode { get; set; } = null;

        /// <summary>
        /// Specification of the ResultCode property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool ResultCodeSpecified => !string.IsNullOrEmpty(ResultCode);

        /// <summary>
        /// Source of this instance.
        /// </summary>
        [XmlElement("source")]
        public string Source { get; set; } = null;

        /// <summary>
        /// Specification of the Source property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool SourceSpecified => !string.IsNullOrEmpty(Source);

        /// <summary>
        /// Stack traces of this instance.
        /// </summary>
        [XmlArray("stack.traces")]
        [XmlArrayItem("stack.trace")]
        public List<LogEventStackTrace> StackTraces { get; set; } = null;

        /// <summary>
        /// Specification of the stack traces of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool StackTracesSpecified => StackTraces?.Count > 0;

        // Tree ----------------------------------

        /// <summary>
        /// The log of this instance.
        /// </summary>
        [XmlIgnore()]
        public ILog Log { get; set; } = null;

        /// <summary>
        /// Parent of this instance.
        /// </summary>
        [XmlIgnore()]
        public ILog Parent { get; set; } = null;

        /// <summary>
        /// Root of this instance.
        /// </summary>
        [XmlIgnore()]
        public ILog Root => Log?.Root;

        /// <summary>
        /// Specification of the Task property of this instance.
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
        public LogEvent() : base()
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
        public LogEvent(
            EventKind kind,
            string title = null,
            EventCriticality criticality = EventCriticality.None,
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
        public LogEvent(
            Exception exception,
            EventCriticality criticality = EventCriticality.None,
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
        public LogEvent(Event event1)
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
        /// Gets the warnings, errors or exceptions of this instance.
        /// </summary>
        /// <param name="isRecursive">Indicate whether the search is recursive.</param>
        /// <param name="kinds">The kinds to consider.</param>
        /// <returns>True if this instance has the specified events. False otherwise.</returns>
        public EventKind GetMaxEventKind(
            bool isRecursive = true,
            params EventKind[] kinds)
        {
            EventKind eventKind = EventKind.None;

            if (Log != null)
            {
                eventKind = Log.GetMaxEventKind(isRecursive, kinds);
            }

            if (eventKind == EventKind.None)
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
        public override void UpdateStorageInfo(ILog log = null)
        {
            base.UpdateStorageInfo(log);
            Log?.UpdateStorageInfo(log);
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(ILog log = null)
        {
            base.UpdateRuntimeInfo(log);

            Log?.UpdateRuntimeInfo(log);
        }

        #endregion
    }
}
