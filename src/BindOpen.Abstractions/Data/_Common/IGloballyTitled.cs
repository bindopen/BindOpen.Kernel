using BindOpen.Data.Items;

namespace BindOpen.Data
{
    /// <summary>
    /// This interface represents a globally titled data.
    /// </summary>
    public interface IGloballyTitled
    {
        /// <summary>
        /// The global title of this instance.
        /// </summary>
        IBdoDictionary Title { get; set; }

        /// <summary>
        /// Returns the description label.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <param name="defaultKey">The default variant name to consider.</param>
        string GetTitleText(string key = StringHelper.__Star, string defaultKey = StringHelper.__Star);
    }
}
