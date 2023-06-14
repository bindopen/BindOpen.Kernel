using System.Collections.Generic;

namespace BindOpen.System.Data
{
    /// <summary>
    /// This interface defines an using data.
    /// </summary>
    public interface IBdoUsing
    {
        /// <summary>
        /// 
        /// </summary>
        IList<string> UsedItemIds { get; set; }
    }
}
