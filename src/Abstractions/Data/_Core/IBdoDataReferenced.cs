namespace BindOpen.System.Data
{
    /// <summary>
    /// This interface defines a configurable data.
    /// </summary>
    public interface IBdoDataReferenced
    {
        /// <summary>
        /// 
        /// </summary>
        IBdoReference DataReference { get; set; }
    }
}
