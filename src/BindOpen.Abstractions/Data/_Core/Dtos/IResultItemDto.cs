namespace BindOpen.Data
{
    /// <summary>
    /// This interface represents a result item DTO.
    /// </summary>
    public interface IResultItemDto : IBdoDto
    {
        /// <summary>
        /// The key of this instance.
        /// </summary>
        string Key { get; set; }

        /// <summary>
        /// The status of this instance.
        /// </summary>
        ResourceStatus Status { get; set; }
    }
}