using BindOpen.Data.Meta;

namespace BindOpen.Data.Conditions;

/// <summary>
/// Defines a basic logical condition consisting of two metadata arguments 
/// and a comparison operator.
/// </summary>
public interface IBdoBasicCondition : IBdoCondition
{
    /// <summary>
    /// Gets or sets the left-hand argument of the condition.
    /// </summary>
    IBdoMetaData Argument1 { get; set; }

    /// <summary>
    /// Gets or sets the right-hand argument of the condition.
    /// </summary>
    IBdoMetaData Argument2 { get; set; }

    /// <summary>
    /// Gets or sets the logical operator (e.g., Equals, GreaterThan, Contains) 
    /// used to evaluate the two arguments.
    /// </summary>
    DataOperators Operator { get; set; }
}