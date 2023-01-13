using BindOpen.MetaData.Items;

namespace BindOpen.MetaData
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
    }
}
