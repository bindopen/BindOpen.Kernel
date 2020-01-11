using BindOpen.Framework.System.Diagnostics;
using BindOpen.Framework.System.Diagnostics.Loggers;
using System;

namespace BindOpen.Framework.Runtime.System.Diagnostics.Loggers
{
    /// <summary>
    /// This class represents a logger.
    /// </summary>
    /// <remarks>The output format is YAML.</remarks>
    public static class BdoLoggerFactory
    {
        // ------------------------------------------------------
        // CREATORS
        // ------------------------------------------------------

        #region Creators

        /// <summary>
        /// Creates the specified logger.
        /// </summary>
        /// <typeparam name="T">The logger class to consider.</typeparam>
        /// <param name="name">The name to consider.</param>
        /// <param name="mode">The mode to consider.</param>
        /// <param name="eventFilter">The function that filters events.</param>
        /// <returns>Returns the specified logger.</returns>
        public static IBdoLogger Create<T>(
            string name,
            BdoLoggerMode mode,
            Predicate<IBdoLogEvent> eventFilter = null) where T : IBdoLogger, new()
        {
            name = (string.IsNullOrEmpty(name) ? BdoLogger.__DefaultName : name);

            IBdoLogger logger = new T()
            {
                Name = name
            }
            .WithMode(mode)
            .WithEventFilter(eventFilter);

            return logger;
        }

        #endregion
    }
}