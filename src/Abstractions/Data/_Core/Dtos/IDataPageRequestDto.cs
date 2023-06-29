namespace BindOpen.System.Data
{
    /// <summary>
    /// This class represents the data page information DTO.
    /// </summary>
    public interface IDataPageRequestDto
    {
        /// <summary>
        /// The maximum count.
        /// </summary>
        int? MaxCount { get; set; }

        /// <summary>
        /// The page size.
        /// </summary>
        int? PageSize { get; set; }

        /// <summary>
        /// The page index.
        /// </summary>
        int? PageIndex { get; set; }
    }
}
