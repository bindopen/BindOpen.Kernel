using BindOpen.Data.Items;

namespace BindOpen.Data.Stores
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITBdoDepot<T> : ITBdoList<T>, IBdoDepot
        where T : IReferenced
    {
    }
}