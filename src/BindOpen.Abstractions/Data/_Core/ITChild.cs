namespace BindOpen.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITChild<T> where T : IReferenced
    {
        T Parent { get; set; }
    }
}