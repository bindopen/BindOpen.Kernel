namespace BindOpen.System.Data
{
    /// <summary>
    /// This interface represents a globally titled data.
    /// </summary>
    public interface IBdoTitled
    {
        /// <summary>
        /// The global title of this instance.
        /// </summary>
        ITBdoDictionary<string> Title { get; set; }
    }
}
