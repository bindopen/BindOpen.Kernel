namespace BindOpen.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITBdoNodeSpec<T> : IBdoSpec, ITTreeNode<T>
        where T : IBdoSpec
    {
    }
}