using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Dto;

namespace BindOpen.Framework.Core.Extensions.Items
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITAppExtensionTitledItemConfiguration<T>
        : ITAppExtensionItemConfiguration<T>, IGloballyTitled, INamed, IIdentifiedDataItem, ISavable
        where T : IAppExtensionItemDefinition
    {
    }
}