using BindOpen.Meta.Items;

namespace BindOpen.Meta.Stores
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITBdoDepot<T> : ITBdoItemSet<T>, IBdoDepot
        where T : IReferenced
    {
    }
}