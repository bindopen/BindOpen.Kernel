using BindOpen.MetaData.Items;

namespace BindOpen.MetaData
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
    }
}
