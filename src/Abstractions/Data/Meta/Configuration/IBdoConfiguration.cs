using BindOpen.Data;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This interface defines a configuration.
    /// </summary>
    public interface IBdoConfiguration :
        IBdoMetaSet, ITTreeNode<IBdoConfiguration>,
        IBdoTitled, IBdoDescribed,
        IIndexed, IDated, IBdoUsing
    {
    }
}