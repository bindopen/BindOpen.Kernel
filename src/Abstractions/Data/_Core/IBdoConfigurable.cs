using BindOpen.System.Data.Meta;

namespace BindOpen.System.Data
{
    /// <summary>
    /// This interface defines a configurable object.
    /// </summary>
    public interface IBdoConfigurable
    {
        /// <summary>
        /// The configuration of this object.
        /// </summary>
        IBdoConfiguration Config { get; set; }
    }
}
