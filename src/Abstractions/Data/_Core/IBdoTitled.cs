namespace BindOpen.Data
{
    /// <summary>
    /// This interface defines a globally titled data.
    /// </summary>
    public interface IBdoTitled
    {
        /// <summary>
        /// The title of this object that is a string dictionary.
        /// </summary>
        ITBdoDictionary<string> Title { get; set; }
    }
}
