using BindOpen.Kernel.Data;

namespace BindOpen.Kernel.Data
{
    /// <summary>
    /// This interface represents a conditional object.
    /// </summary>
    public interface IResultItem
    {
        string Key { get; set; }

        ResourceStatus Status { get; set; }
    }
}