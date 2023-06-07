using BindOpen.Scoping.Data;

namespace BindOpen.Scoping.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITBdoExtensionDictionary<T> : IBdoExtensionDictionary, ITBdoSet<T>
        where T : IBdoExtensionDefinition
    {
    }
}