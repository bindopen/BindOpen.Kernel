namespace BindOpen.Data
{
    /// <summary>
    /// This interface defines an identified object.
    /// </summary>
    public interface IIdentified
    {
        /// <summary>
        /// ID of this object.
        /// </summary>
        string Identifier { get; set; }
    }
}
