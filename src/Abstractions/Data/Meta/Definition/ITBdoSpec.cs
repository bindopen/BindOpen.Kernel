namespace BindOpen.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITBdoSpec<T> : IBdoBaseSpec, ITTreeNode<T>, ITGroup<T>
        where T : IBdoBaseSpec
    {
    }
}