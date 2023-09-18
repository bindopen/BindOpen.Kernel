using BindOpen.Kernel.Logging;
using BindOpen.Kernel.Scoping.Connectors;
using System;

namespace BindOpen.Kernel.Data.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoRepository : IBdoConnected
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="autoConnect"></param>
        void UsingConnection(Action<IBdoConnection> action, bool autoConnect = true);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="log"></param>
        /// <param name="autoConnect"></param>
        void UsingConnection(Action<IBdoConnection, IBdoLog> action, IBdoLog log, bool autoConnect = true);
    }
}