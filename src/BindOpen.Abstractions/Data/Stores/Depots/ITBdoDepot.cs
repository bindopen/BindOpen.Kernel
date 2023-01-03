using BindOpen.Data.Items;

namespace BindOpen.Data.Stores
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