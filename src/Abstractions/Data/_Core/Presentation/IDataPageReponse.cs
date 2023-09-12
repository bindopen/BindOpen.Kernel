namespace BindOpen.Kernel.Data
{
    /// <summary>
    /// This class represents the data page form request DTO.
    /// </summary>
    public interface IDataPageReponse : IDataPageRequest
    {
        /// <summary>
        /// The count.
        /// </summary>
        int? TotalCount { get; set; }
    }
}