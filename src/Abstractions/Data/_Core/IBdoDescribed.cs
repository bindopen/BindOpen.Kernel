namespace BindOpen.System.Data
{
    /// <summary>
    /// This interface represents a globally described data.
    /// </summary>
    public interface IBdoDescribed
    {
        /// <summary>
        /// The global description of this instance.
        /// </summary>
        IBdoDictionary Description { get; set; }
    }
}
