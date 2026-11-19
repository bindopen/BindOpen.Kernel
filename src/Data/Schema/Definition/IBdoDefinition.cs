namespace BindOpen.Data.Schema;

/// <summary>
/// Defines a BindOpen definition.
/// A definition serves as a schema blueprint, providing the rules, structures, 
/// and constraints that metadata sets must adhere to.
/// </summary>
public interface IBdoDefinition :
    IBdoSchemaSet, ITTreeNode<IBdoDefinition>,
    IBdoTitled, IBdoDescribed,
    IIndexed, IDated, IBdoUsing
{
    // This interface represents the "Schema" version of a configuration.
    // It allows for the hierarchical organization of data requirements
    // and specifications.
}