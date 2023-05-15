using System.Collections.Generic;

namespace BindOpen.Logging
{
    /// <summary>
    /// This class represents a event kind extension.
    /// </summary>
    public static class EventKindExtensions
    {

        // Gets -------------------------

        /// <summary>
        /// Gets the maximum kind of events of the specified event kinds.
        /// </summary>
        /// <param key="eventKinds">The event kinds to consider.</param>
        /// <returns>True if this instance has the specified events. False otherwise.</returns>
        public static EventKinds Max(this IEnumerable<EventKinds> eventKinds)
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
        /// <param key="eventKind1">The first event kind to consider.</param>
        /// <param key="eventKind2">The second event kind to consider.</param>
        /// <returns>True if the first event kind is greater than the second one.</returns>
        public static EventKinds Max(this EventKinds eventKind1, EventKinds eventKind2)
        {
            return eventKind2 == EventKinds.Any ? eventKind1 : eventKind1 > eventKind2 ? eventKind1 : eventKind2;
        }

        /// <summary>
        /// Indicates whether the first event kind is greater than the second one.
        /// </summary>
        /// <param key="eventKind1">The first event kind to consider.</param>
        /// <param key="eventKind2">The second event kind to consider.</param>
        /// <returns>True if the first event kind is greater than the second one.</returns>
        public static bool IsGreaterThan(this EventKinds eventKind1, EventKinds eventKind2)
        {
            return eventKind1 > eventKind2;
        }

    }
}
