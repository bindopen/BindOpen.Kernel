namespace BindOpen.Scoping.Data
{
    /// <summary>
    /// This interface represents a globally titled data.
    /// </summary>
    public interface IBdoTitled
    {
        /// <summary>
        /// The global title of this instance.
        /// </summary>
        IBdoDictionary Title { get; set; }
    }
}
