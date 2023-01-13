using BindOpen.MetaData.Items;

namespace BindOpen.MetaData.Stores
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