namespace BindOpen.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITBdoSpec<T> : IBdoBaseSpec, ITTreeNode<T>
        where T : IBdoBaseSpec
    {
    }
}