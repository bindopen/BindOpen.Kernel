namespace BindOpen.System.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITChild<T> : IReferenced where T : IReferenced
    {
        T Parent { get; set; }
    }
}