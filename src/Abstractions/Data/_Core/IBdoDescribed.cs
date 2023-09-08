namespace BindOpen.Kernel.Data
{
    /// <summary>
    /// This interface represents a globally-described data.
    /// </summary>
    public interface IBdoDescribed
    {
        /// <summary>
        /// The description of this object that is a string dictionary.
        /// </summary>
        ITBdoDictionary<string> Description { get; set; }
    }
}
