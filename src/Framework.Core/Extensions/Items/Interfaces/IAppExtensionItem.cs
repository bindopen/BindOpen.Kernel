using BindOpen.Framework.Core.Application.Configuration;
using BindOpen.Framework.Core.Data.Items;

namespace BindOpen.Framework.Core.Extensions.Items
{
    /// <summary>
    /// This class represents an application extension runtime item.
    /// </summary>
    public interface IAppExtensionItem : IDataItem
    {
        IBaseConfiguration Configuration { get; }
    }
}

