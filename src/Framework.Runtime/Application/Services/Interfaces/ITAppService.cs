using BindOpen.Framework.Runtime.Application.Settings;

namespace BindOpen.Framework.Runtime.Application.Services
{
    /// <summary>
    /// The interface defines the application service.
    /// </summary>
    public interface ITAppService<T> : IAppService
        where T : class, IBaseSettings, new()
    {
        new T Settings { get; set; }
    }
}