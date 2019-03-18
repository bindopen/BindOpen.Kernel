using BindOpen.Framework.Core.Data.Connections;

namespace BindOpen.Framework.Core.Application.Services
{
    /// <summary>
    /// This interfaces represents a data service.
    /// </summary>
    public interface IDataService
    {
        IConnection Connection { get; }
    }
}