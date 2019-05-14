using System;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Diagnostics.Loggers;

namespace BindOpen.Framework.Runtime.System.Diagnostics.Loggers
{
    /// <summary>
    /// This class represents a logger.
    /// </summary>
    /// <remarks>The output format is YAML.</remarks>
    public static class LoggerFactory
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
        /// <param name="outputKind">The output kind to consider.</param>
        /// <param name="isVerbose">Indicates whether .</param>
        /// <param name="uiCulture">The folder path to consider.</param>
        /// <param name="folderPath">The folder path to consider.</param>
        /// <param name="fileName">The file name to consider.</param>
        /// <param name="eventFinder">The function that filters event.</param>
        /// <param name="expirationDayNumber">The expiration day number to consider.</param>
        /// <returns>Returns the specified logger.</returns>
        public static ILogger Create<T>(
            string name,
            LoggerMode mode,
            DataSourceKind outputKind,
            bool isVerbose = false,
            string uiCulture = null,
            string folderPath = null,
            string fileName = null,
            Predicate<ILogEvent> eventFinder = null,
            int expirationDayNumber = -1) where T : ILogger, new()
        {
            name = (string.IsNullOrEmpty(name) ? Logger.__DefaultName : name);

            ILogger logger = new T()
            {
                Name = name,
                Mode = mode,
                OutputKind = outputKind,
                IsVerbose = isVerbose,
                UICulture = uiCulture,
                EventFinder = eventFinder,
            };
            logger.SetFilePath(folderPath, false, fileName);
            logger.DeleteExpiredLogs(expirationDayNumber);

            return logger;
        }

        #endregion
    }
}