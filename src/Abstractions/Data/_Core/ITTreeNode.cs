namespace BindOpen.System.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITTreeNode<T> : IReferenced, ITParent<T>, ITChild<T>
        where T : ITChild<T>
    {
    }
}