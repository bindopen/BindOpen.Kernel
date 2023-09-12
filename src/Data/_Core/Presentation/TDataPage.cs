using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BindOpen.Kernel.Data
{
    /// <summary>
    /// This class represents a data page.
    /// </summary>
    public class TDataPage<T> : ITDataPage<T> where T : class
    {
        #region Properties

        /// <summary>
        /// The items of this instance.
        /// </summary>
        [JsonPropertyName("items")]
        public IEnumerable<T> Items { get; set; }

        /// <summary>
        /// The maximum count.
        /// </summary>
        [JsonPropertyName("maxCount")]
        public int? MaxCount { get; set; }

        /// <summary>
        /// The page size.
        /// </summary>
        [JsonPropertyName("pageSize")]
        public int? PageSize { get; set; }

        /// <summary>
        /// The page index.
        /// </summary>
        [JsonPropertyName("pageIndex")]
        public int? PageIndex { get; set; }

        /// <summary>
        /// The total count of this instance.
        /// </summary>
        [JsonPropertyName("totalCount")]
        public int? TotalCount { get; set; }

        #endregion

        public TDataPage() { }
    }
}
