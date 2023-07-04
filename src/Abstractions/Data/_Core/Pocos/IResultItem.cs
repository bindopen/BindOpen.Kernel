namespace BindOpen.System.Data
{
    /// <summary>
    /// This interface represents a POCO based on a DTO.
    /// </summary>
    public interface IResultItem
    {
        public string Key { get; set; }

        public ResourceStatus Status { get; set; }
    }
}
