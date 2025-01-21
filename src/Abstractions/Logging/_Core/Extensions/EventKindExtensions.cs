using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Logging
{
    /// <summary>
    /// This class represents a event level extension.
    /// </summary>
    public static class EventLevelExtensions
    {

        // Gets -------------------------

        /// <summary>
        /// Gets the maximum kind of events of the specified event levels.
        /// </summary>
        /// <param key="eventLevels">The event levels to consider.</param>
        /// <returns>True if this instance has the specified events. False otherwise.</returns>
        public static BdoEventKinds Max(this IEnumerable<BdoEventKinds> eventLevels)
        {
            BdoEventKinds eventLevel = BdoEventKinds.None;
            if (eventLevels != null)
            {
                foreach (BdoEventKinds currentEventLevel in eventLevels)
                {
                    eventLevel = currentEventLevel.Max(eventLevel);
                }
            }

            return eventLevel;
        }

        /// <summary>
        /// Gets the maximum kind of events of the specified event levels.
        /// </summary>
        /// <param key="eventLevels">The event levels to consider.</param>
        /// <returns>True if this instance has the specified events. False otherwise.</returns>
        public static bool Has(this IEnumerable<BdoEventKinds> eventLevels, BdoEventKinds kind)
        {
            return kind == BdoEventKinds.Any
                || eventLevels.Any(q => q == BdoEventKinds.Any || q == kind);
        }

        /// <summary>
        /// Gets the maximum between the two specified event levels.
        /// </summary>
        /// <param key="eventLevel1">The first event level to consider.</param>
        /// <param key="eventLevel2">The second event level to consider.</param>
        /// <returns>True if the first event level is greater than the second one.</returns>
        public static BdoEventKinds Max(this BdoEventKinds eventLevel1, BdoEventKinds eventLevel2)
        {
            return eventLevel2 == BdoEventKinds.Any ? eventLevel1 : eventLevel1 > eventLevel2 ? eventLevel1 : eventLevel2;
        }

        /// <summary>
        /// Indicates whether the first event level is greater than the second one.
        /// </summary>
        /// <param key="eventLevel1">The first event level to consider.</param>
        /// <param key="eventLevel2">The second event level to consider.</param>
        /// <returns>True if the first event level is greater than the second one.</returns>
        public static bool IsGreaterThan(this BdoEventKinds eventLevel1, BdoEventKinds eventLevel2)
        {
            return eventLevel1 > eventLevel2;
        }

    }
}
