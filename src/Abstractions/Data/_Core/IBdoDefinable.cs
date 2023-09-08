namespace BindOpen.Kernel.Data
{
    /// <summary>
    /// This interface defines a definable data.
    /// </summary>
    public interface IBdoDefinable
    {
        /// <summary>
        /// The définition of this object.
        /// </summary>
        string DefinitionUniqueName { get; set; }
    }
}
