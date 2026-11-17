using BindOpen.Data.Meta;
using System;
using System.Linq;

namespace BindOpen.Scoping.Script;

/// <summary>
/// Provides a collection of standard logical and comparison script functions.
/// </summary>
/// <remarks>
/// These methods are registered in the BindOpen scope to enable boolean logic 
/// and data validation within script expressions.
/// </remarks>
public static partial class FunctionStore
{
    /// <summary>
    /// Returns true if all provided conditions are true.
    /// </summary>
    [BdoFunction(Name = "and")]
    public static bool And(
        [BdoParameter(Title = "Conditions")] params bool[] objs)
        => objs != null && objs.All(q => q);

    /// <summary>
    /// Returns true if at least one provided condition is true.
    /// </summary>
    [BdoFunction(Name = "or")]
    public static bool Or(
        [BdoParameter(Title = "Conditions")] params bool[] objs)
        => objs != null && objs.Any(q => q);

    /// <summary>
    /// Returns true if none of the provided conditions are true.
    /// </summary>
    [BdoFunction(Name = "xor")]
    public static bool Xor(
        [BdoParameter(Title = "Conditions")] params bool[] objs)
        => objs != null && !objs.Any(q => q);

    /// <summary>
    /// Inverts the specified boolean condition.
    /// </summary>
    [BdoFunction(Name = "not")]
    public static bool Not(
        [BdoParameter(Title = "Condition")] bool obj1)
        => !obj1;

    /// <summary>
    /// Determines whether two objects are equal.
    /// </summary>
    [BdoFunction(Name = "eq")]
    public static bool Eq(
        [BdoParameter(Title = "First object")] object obj1,
        [BdoParameter(Title = "Second object")] object obj2)
        => (obj1 == null && obj2 == null) || obj1?.Equals(obj2) == true;

    /// <summary>
    /// Determines whether two objects are not equal.
    /// </summary>
    [BdoFunction(Name = "ne")]
    public static bool Ne(
        [BdoParameter(Title = "First object")] object obj1,
        [BdoParameter(Title = "Second object")] object obj2)
        => (obj1 == null && obj2 != null) || (obj1 != null && !obj1.Equals(obj2));

    /// <summary>
    /// Checks if a value is a valid member of a specific enumeration type.
    /// </summary>
    /// <param name="fullyQualifiedName">The assembly-qualified name of the Enum type.</param>
    [BdoFunction(Name = "inEnum")]
    public static bool InEnum(
        [BdoParameter(Title = "First object")] object obj1,
        [BdoParameter(Title = "Type name")] string fullyQualifiedName)
    {
        if (obj1 == null || string.IsNullOrEmpty(fullyQualifiedName)) return false;

        Type type = Type.GetType(fullyQualifiedName);
        if (type != null && type.IsEnum)
        {
            return Enum.GetNames(type).Contains(obj1.ToString());
        }

        return false;
    }
}