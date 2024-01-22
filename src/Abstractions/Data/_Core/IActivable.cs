namespace BindOpen.Data
{
    /// <summary>
    /// This interface represents an object that be checked.
    /// </summary>
    public interface IActivable
    {
        bool IsActive { get; set; }
    }
}