using BindOpen.Scoping.Data.Meta;

namespace BindOpen.Scoping.Data
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
