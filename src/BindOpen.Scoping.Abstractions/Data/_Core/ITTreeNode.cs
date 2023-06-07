namespace BindOpen.Scoping.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITTreeNode<T> : IReferenced, ITParent<T>, ITChild<T>
        where T : IReferenced
    {
    }
}