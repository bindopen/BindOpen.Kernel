using BindOpen.Framework.Core.Application.Configuration;
using BindOpen.Framework.Core.Data.Items;

namespace BindOpen.Framework.Core.Extensions.Runtime.Items
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

