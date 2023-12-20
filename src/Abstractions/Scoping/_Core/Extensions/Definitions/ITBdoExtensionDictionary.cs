using BindOpen.Data;

namespace BindOpen.Scoping
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