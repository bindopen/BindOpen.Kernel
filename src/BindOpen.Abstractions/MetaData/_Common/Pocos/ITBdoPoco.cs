namespace BindOpen.MetaData
{
    /// <summary>
    /// This interface represents a POCO based on a DTO.
    /// </summary>
    public interface ITBdoPoco<T> where T : IBdoDto
    {
        /// <summary>
        /// The DTO of this instance.
        /// </summary>
        T Dto { get; set; }
    }
}
