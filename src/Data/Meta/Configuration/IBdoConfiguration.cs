namespace BindOpen.Data.Meta
{
    /// <summary>
    /// Defines a BindOpen configuration.
    /// A configuration is a specialized metadata set that supports tree-based 
    /// hierarchy, titling, description, and lifecycle management.
    /// </summary>
    public interface IBdoConfiguration :
        IBdoMetaSet, ITTreeNode<IBdoConfiguration>,
        IBdoTitled, IBdoDescribed,
        IIndexed, IDated, IBdoUsing
    {
        // This interface bridges the gap between raw data sets (IBdoMetaSet)
        // and complex, hierarchical application settings.
    }
}