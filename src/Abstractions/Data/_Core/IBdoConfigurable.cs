using BindOpen.System.Data.Meta;

namespace BindOpen.System.Data
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
