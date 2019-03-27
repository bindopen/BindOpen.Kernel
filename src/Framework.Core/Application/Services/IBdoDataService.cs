using BindOpen.Framework.Core.Data.Connections;

namespace BindOpen.Framework.Core.Application.Services
{
    /// <summary>
    /// This interfaces represents a BindOpen data service.
    /// </summary>
    public interface IBdoDataService
    {
        /// <summary>
        /// The connection of the service.
        /// </summary>
        BdoAppHost Connection { get; }
    }
}