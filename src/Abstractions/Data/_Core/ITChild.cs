namespace BindOpen.System.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITChild<T> where T : IReferenced
    {
        T Parent { get; set; }
    }
}