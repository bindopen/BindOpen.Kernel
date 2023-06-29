using System.Collections.Generic;

namespace BindOpen.System.Data
{
    /// <summary>
    /// This class represents a data page.
    /// </summary>
    public interface ITDataPageDto<T> : IDataPageReponseDto where T : class
    {
        /// <summary>
        /// The items of this instance.
        /// </summary>
        IEnumerable<T> Items { get; set; }
    }
}
