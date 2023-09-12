using System.Collections.Generic;

namespace BindOpen.Kernel.Data
{
    /// <summary>
    /// This class represents a data page.
    /// </summary>
    public interface ITDataPage<T> : IDataPageReponse where T : class
    {
        /// <summary>
        /// The items of this instance.
        /// </summary>
        IEnumerable<T> Items { get; set; }
    }
}
