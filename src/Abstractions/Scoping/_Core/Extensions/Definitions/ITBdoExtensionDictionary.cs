using BindOpen.System.Data;

namespace BindOpen.System.Scoping
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