using BindOpen.MetaData.Elements;

namespace BindOpen.MetaData
{
    /// <summary>
    /// This interface represents a data with detail.
    /// </summary>
    public interface IDetailed
    {
        /// <summary>
        /// The detail of this instance.
        /// </summary>
        IBdoElementSet Detail { get; set; }
    }
}
