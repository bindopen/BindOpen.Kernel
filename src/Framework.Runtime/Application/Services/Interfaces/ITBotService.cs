using BindOpen.Framework.Runtime.Application.Settings;

namespace BindOpen.Framework.Runtime.Application.Services
{
    /// <summary>
    /// The interface defines the bot service.
    /// </summary>
    public interface ITBotService<T> : IBotService
        where T : class, IBaseSettings, new()
    {
        /// <summary>
        /// Settings.
        /// </summary>
        new T Settings { get; set; }
    }
}