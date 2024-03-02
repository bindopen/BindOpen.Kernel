namespace BindOpen.Data
{
    /// <summary>
    /// This interface defines an object that can be activable.
    /// </summary>
    public interface IActivable
    {
        bool IsActive { get; set; }
    }
}