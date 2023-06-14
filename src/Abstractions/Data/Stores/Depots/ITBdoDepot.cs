namespace BindOpen.System.Data.Stores
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITBdoDepot<T> : ITBdoSet<T>, IBdoDepot
        where T : IReferenced
    {
    }
}