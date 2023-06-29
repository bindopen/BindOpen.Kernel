namespace BindOpen.System.Data
{
    /// <summary>
    /// This class represents the data page form request DTO.
    /// </summary>
    public interface IDataPageReponseDto : IDataPageRequestDto
    {
        /// <summary>
        /// The count.
        /// </summary>
        int? TotalCount { get; set; }
    }
}