namespace BindOpen.Data
{
    /// <summary>
    /// This interface defines a conditional object.
    /// </summary>
    public interface IResultItem
    {
        string Key { get; set; }

        ResourceStatus Status { get; set; }
    }
}