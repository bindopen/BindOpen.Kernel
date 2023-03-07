using BindOpen.Data.Items;

namespace BindOpen.Runtime.Definitions
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