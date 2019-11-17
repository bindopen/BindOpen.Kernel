using BindOpen.Framework.Runtime.Application.Configuration;

namespace BindOpen.Framework.Runtime.Application.Settings
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="Q"></typeparam>
    public interface ITBotSettings<Q> : ITSettings<Q>, IBotSettings
        where Q : class, IBotConfiguration, new()
    {
    }
}