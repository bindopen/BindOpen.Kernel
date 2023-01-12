using BindOpen.Meta;
using BindOpen.Runtime.Definition;

namespace BindOpen.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITBdoExtensionTitledItemConfiguration<T>
        : ITBdoExtensionItemConfiguration<T>, ITGloballyTitledPoco<ITBdoExtensionTitledItemConfiguration<T>>
        where T : IBdoExtensionItemDefinition
    {
    }
}