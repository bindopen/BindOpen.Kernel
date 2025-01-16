using BindOpen.Data.Meta;

namespace BindOpen.Data.Conditions;

/// <summary>
/// This interface defines a basic condition.
/// </summary>
public interface IBdoBasicCondition : IBdoCondition
{
    /// <summary>
    /// The first argument.
    /// </summary>
    IBdoMetaData Argument1 { get; set; }

    /// <summary>
    /// The second argument.
    /// </summary>
    IBdoMetaData Argument2 { get; set; }

    /// <summary>
    /// The operator.
    /// </summary>
    DataOperators Operator { get; set; }
}