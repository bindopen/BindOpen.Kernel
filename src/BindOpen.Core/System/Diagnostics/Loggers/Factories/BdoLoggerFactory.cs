using Microsoft.Extensions.Logging;

namespace BindOpen.System.Diagnostics
{
    /// <summary>
    /// This class represents a logger.
    /// </summary>
    public static class BdoLoggerFactory
    {
        /// <summary>
        /// Creates a new instance of a BdoLogger instance.
        /// </summary>
        /// <param name="logger">The logger to consider.</param>
        /// <returns>Returns the created BDO logger.</returns>
        public static TBdoLogger<T> Create<T>(ILogger logger)
            where T : IBdoLoggerFormat, new()
        {
            var bdoLogger = new TBdoLogger<T>();
            bdoLogger.SetNative(logger);
            return bdoLogger;
        }
    }
}
