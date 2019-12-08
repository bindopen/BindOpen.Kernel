using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Definition.Items;

namespace BindOpen.Framework.Core.Extensions.Runtime.Items
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITBdoExtensionTitledItemConfiguration<T>
        : ITBdoExtensionItemConfiguration<T>, IGloballyTitled, INamed, IIdentifiedDataItem, ISavable
        where T : IBdoExtensionItemDefinition
    {
    }
}