using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BindOpen.Kernel.Data
{
    /// <summary>
    /// This class represents a data page DTO.
    /// </summary>
    public class TDataPageDto<T> : ITDataPage<T>, IBdoDto where T : class
    {
        /// <summary>
        /// The items of this instance.
        /// </summary>
        [JsonPropertyName("items")]
        public IEnumerable<T> Items { get; set; }

        /// <summary>
        /// The maximum count of this instance.
        /// </summary>
        [JsonPropertyName("maxCount")]
        public int? MaxCount { get; set; }

        /// <summary>
        /// The page size of this instance.
        /// </summary>
        [JsonPropertyName("pageSize")]
        public int? PageSize { get; set; }

        /// <summary>
        /// The page index of this instance.
        /// </summary>
        [JsonPropertyName("pageIndex")]
        public int? PageIndex { get; set; }

        /// <summary>
        /// The total count of this instance.
        /// </summary>
        [JsonPropertyName("totalCount")]
        public int? TotalCount { get; set; }


        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TDataPageDto class.
        /// </summary>
        public TDataPageDto() { }

        #endregion
    }
}
