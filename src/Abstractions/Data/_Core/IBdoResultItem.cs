namespace BindOpen.Kernel.Data
{
    /// <summary>
    /// This interface represents a conditional object.
    /// </summary>
    public interface IBdoResultItem
    {
        string Key { get; set; }

        ResourceStatus Status { get; set; }
    }
}