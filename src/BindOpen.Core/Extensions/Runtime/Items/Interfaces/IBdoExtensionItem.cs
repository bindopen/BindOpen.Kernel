using BindOpen.Application.Configuration;
using BindOpen.Data.Items;

namespace BindOpen.Extensions.Runtime
{
    /// <summary>
    /// This class represents a BindOpen extension runtime item.
    /// </summary>
    public interface IBdoExtensionItem : IDataItem
    {
        /// <summary>
        /// Configuration.
        /// </summary>
        IBdoBaseConfiguration Configuration { get; }
    }
}

