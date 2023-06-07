namespace BindOpen.Scoping.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITChild<T> where T : IReferenced
    {
        T Parent { get; set; }
    }
}