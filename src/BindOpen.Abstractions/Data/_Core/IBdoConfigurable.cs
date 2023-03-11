using BindOpen.Data.Meta;

namespace BindOpen.Data
{
    /// <summary>
    /// This interface defines a configurable data.
    /// </summary>
    public interface IBdoConfigurable
    {
        /// <summary>
        /// 
        /// </summary>
        IBdoConfiguration Config { get; set; }
    }
}
