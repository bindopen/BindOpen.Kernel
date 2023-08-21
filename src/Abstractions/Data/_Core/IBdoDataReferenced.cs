namespace BindOpen.System.Data
{
    /// <summary>
    /// This interface defines a configurable data.
    /// </summary>
    public interface IBdoDataReferenced
    {
        /// <summary>
        /// The reference of this object.
        /// </summary>
        IBdoReference DataReference { get; set; }
    }
}
