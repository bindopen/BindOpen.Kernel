using System;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Diagnostics.Loggers;

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
        /// <param name="outputKind">The output kind to consider.</param>
        /// <param name="isVerbose">Indicates whether .</param>
        /// <param name="uiCulture">The folder path to consider.</param>
        /// <param name="folderPath">The folder path to consider.</param>
        /// <param name="fileName">The file name to consider.</param>
        /// <param name="eventFinder">The function that filters event.</param>
        /// <param name="expirationDayNumber">The expiration day number to consider.</param>
        /// <returns>Returns the specified logger.</returns>
        public static IBdoLogger Create<T>(
            string name,
            BdoLoggerMode mode,
            DatasourceKind outputKind,
            bool isVerbose = false,
            string uiCulture = null,
            string folderPath = null,
            string fileName = null,
            Predicate<IBdoLogEvent> eventFinder = null,
            int expirationDayNumber = -1) where T : IBdoLogger, new()
        {
            name = (string.IsNullOrEmpty(name) ? BdoLogger.__DefaultName : name);

            IBdoLogger logger = new T()
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