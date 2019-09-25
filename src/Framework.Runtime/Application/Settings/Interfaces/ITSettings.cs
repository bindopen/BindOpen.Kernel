using BindOpen.Framework.Core.Application.Configuration;

namespace BindOpen.Framework.Runtime.Application.Settings
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="Q"></typeparam>
    public interface ITSettings<Q> : IBaseSettings
        where Q : class, IBaseConfiguration, new()
    {
        /// <summary>
        /// 
        /// </summary>
        new Q Configuration { get; }
    }
}