using BindOpen.System.Data.Meta;

namespace BindOpen.System.Data
{
    /// <summary>
    /// This interface represents an object that has a data detail that is a meta set.
    /// </summary>
    public interface IBdoDetailed
    {
        /// <summary>
        /// The detail of this object that is a meta data set.
        /// </summary>
        IBdoMetaSet Detail { get; set; }
    }
}
