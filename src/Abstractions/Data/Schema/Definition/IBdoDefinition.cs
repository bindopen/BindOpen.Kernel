namespace BindOpen.Data.Schema;

/// <summary>
/// This interface defines a configuration.
/// </summary>
public interface IBdoDefinition :
    IBdoSchemaSet, ITTreeNode<IBdoDefinition>,
    IBdoTitled, IBdoDescribed,
    IIndexed, IDated, IBdoUsing
{
}