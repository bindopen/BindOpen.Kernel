using BindOpen.Data.Items;

namespace BindOpen.Data
{
    /// <summary>
    /// This interface represents a globally described data.
    /// </summary>
    public interface IGloballyDescribed
    {
        /// <summary>
        /// The global description of this instance.
        /// </summary>
        IBdoDictionary Description { get; set; }

        /// <summary>
        /// Returns the description label.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <param name="defaultKey">The default variant name to consider.</param>
        string GetDescriptionText(string key = StringHelper.__Star, string defaultKey = StringHelper.__Star);
    }
}
