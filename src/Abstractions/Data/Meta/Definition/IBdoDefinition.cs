namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This interface defines a configuration.
    /// </summary>
    public interface IBdoDefinition :
        IBdoSpecSet, ITTreeNode<IBdoDefinition>,
        IBdoTitled, IBdoDescribed,
        IIndexed, IDated, IBdoUsing
    {
    }
}