using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Dto;
using BindOpen.Framework.Core.Extensions.Items.Libraries;

namespace BindOpen.Framework.Core.Extensions.Definition
{
    public interface IAppExtensionItemDefinition : IDataItem, IReferenced
    {
        ILibrary Library { get; }
    }
}