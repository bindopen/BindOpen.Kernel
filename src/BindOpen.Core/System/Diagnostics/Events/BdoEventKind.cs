using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BindOpen.System.Diagnostics.Events
{

    /// <summary>
    /// This enumeration lists the possible event kinds.
    /// </summary>
    [Serializable()]
    [Flags()]
    [XmlType("EventKinds", Namespace = "https://bindopen.org/xsd")]
    public enum EventKinds
    {
        /// <summary>
        /// None.
        /// </summary>
        None = 0,

        /// <summary>
        /// Message.
        /// </summary>
        Message = 1 << 0,

        /// <summary>
        /// Checkpoint.
        /// </summary>
        Checkpoint = 1 << 1,

        /// <summary>
        /// Warning.
        /// </summary>
        Warning = 1 << 2,

        /// <summary>
        /// Error.
        /// </summary>
        Error = 1 << 3,

        /// <summary>
        /// Exception.
        /// </summary>
        Exception = 1 << 4,

        /// <summary>
        /// Other.
        /// </summary>
        Other = 1 << 5,

        /// <summary>
        /// None.
        /// </summary>
        Any = EventKinds.Checkpoint | EventKinds.Error | EventKinds.Exception | EventKinds.Message | EventKinds.Other | EventKinds.Warning,
    };


    // --------------------------------------------------
    // EXTENSION
    // --------------------------------------------------

    #region Extension

    /// <summary>
    /// This class represents a event kind extension.
    /// </summary>
    public static class EventKindExtension
    {

        // Gets -------------------------

        /// <summary>
        /// Gets the maximum kind of events of the specified event kinds.
        /// </summary>
        /// <param name="eventKinds">The event kinds to consider.</param>
        /// <returns>True if this instance has the specified events. False otherwise.</returns>
        public static EventKinds Max(this List<EventKinds> eventKinds)
        {
            EventKinds eventKind = EventKinds.None;
            if (eventKinds != null)
                foreach (EventKinds currentEventKind in eventKinds)
                    eventKind = currentEventKind.Max(eventKind);

            return eventKind;
        }

        /// <summary>
        /// Gets the maximum between the two specified event kinds.
        /// </summary>
        /// <param name="eventKind1">The first event kind to consider.</param>
        /// <param name="eventKind2">The second event kind to consider.</param>
        /// <returns>True if the first event kind is greater than the second one.</returns>
        public static EventKinds Max(this EventKinds eventKind1, EventKinds eventKind2)
        {
            return eventKind2 == EventKinds.Any ? eventKind1 : (eventKind1 > eventKind2 ? eventKind1 : eventKind2);
        }

        /// <summary>
        /// Indicates whether the first event kind is greater than the second one.
        /// </summary>
        /// <param name="eventKind1">The first event kind to consider.</param>
        /// <param name="eventKind2">The second event kind to consider.</param>
        /// <returns>True if the first event kind is greater than the second one.</returns>
        public static bool IsGreaterThan(this EventKinds eventKind1, EventKinds eventKind2)
        {
            return eventKind1 > eventKind2;
        }

    }

    #endregion

}
