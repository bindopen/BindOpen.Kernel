using BindOpen.Kernel.Data;

namespace BindOpen.Kernel.Scoping
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