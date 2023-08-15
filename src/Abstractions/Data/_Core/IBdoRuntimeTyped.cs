using System;

namespace BindOpen.System.Data
{
    /// <summary>
    /// This interface defines an using data.
    /// </summary>
    public interface IBdoRuntimeTyped
    {
        /// <summary>
        /// 
        /// </summary>
        Type RuntimeType { get; set; }
    }
}
