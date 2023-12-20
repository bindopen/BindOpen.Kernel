namespace BindOpen.Data
{
    /// <summary>
    /// This interface represents a tree node.
    /// </summary>
    public interface ITTreeNode<T> : IReferenced, ITParent<T>, ITChild<T>
        where T : IReferenced
    {
    }
}