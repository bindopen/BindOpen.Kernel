using System.Collections.Generic;

namespace BindOpen.Data
{
    /// <summary>
    /// This interface defines an using data.
    /// </summary>
    public interface IBdoUsing
    {
        /// <summary>
        /// 
        /// </summary>
        List<string> UsedItemIds { get; set; }
    }
}
