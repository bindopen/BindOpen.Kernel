using BindOpen.Data.Common;
using BindOpen.Data.Items;
using BindOpen.Extensions.Definition;

namespace BindOpen.Extensions.Runtime
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITBdoExtensionTitledItemConfiguration<T>
        : ITBdoExtensionItemConfiguration<T>, ITitledDataItem, IIdentifiedDataItem, ISavable
        where T : IBdoExtensionItemDefinition
    {
    }
}