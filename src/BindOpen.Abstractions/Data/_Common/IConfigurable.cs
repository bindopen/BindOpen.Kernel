using BindOpen.Data.Configuration;

namespace BindOpen.Data
{
    /// <summary>
    /// This interface defines a configurable data.
    /// </summary>
    public interface IConfigurable
    {
        /// <summary>
        /// 
        /// </summary>
        IBdoConfiguration Config { get; set; }
    }
}
