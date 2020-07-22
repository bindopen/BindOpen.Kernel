using BindOpen.Data.Items;
using Microsoft.Extensions.Logging;

namespace BindOpen.System.Diagnostics
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoLogger : IDataItem
    {
        /// <summary>
        /// Sets the native logger.
        /// </summary>
        /// <param name="nativeLogger">The native logger to consider.</param>
        /// <returns>True if this instance has the specified events. False otherwise.</returns>
        IBdoLogger SetNative(ILogger nativeLogger);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="ev"></typeparam>
        void Log(IBdoLogEvent ev);
    }
}