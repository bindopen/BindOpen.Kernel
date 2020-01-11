using BindOpen.Framework.Application.Configuration;
using BindOpen.Framework.Data.Items;

namespace BindOpen.Framework.Extensions.Runtime
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

