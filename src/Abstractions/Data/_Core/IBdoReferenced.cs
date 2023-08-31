namespace BindOpen.System.Data
{
    /// <summary>
    /// This interface defines a configurable data.
    /// </summary>
    public interface IBdoReferenced
    {
        /// <summary>
        /// The reference of this object.
        /// </summary>
        IBdoReference Reference { get; set; }
    }
}
