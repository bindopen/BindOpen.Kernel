namespace BindOpen.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITChild<T> where T : IReferenced
    {
        T Parent { get; set; }

        /// <summary>
        /// The level of this instance.
        /// </summary>
        int Level();
    }
}