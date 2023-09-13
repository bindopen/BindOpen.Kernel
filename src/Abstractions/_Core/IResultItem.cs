using BindOpen.Kernel.Abstractions.Data._Core.Enums;

namespace BindOpen.Kernel
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