using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Data.Objects;

namespace BindOpen.Scoping.Script;

/// <summary>
/// Defines a scriptword, representing a single executable or evaluable element 
/// within a BindOpen script expression.
/// </summary>
/// <remarks>
/// Scriptwords are the building blocks of the Abstract Syntax Tree (AST). 
/// They can represent functions, variables, or literals and can be chained 
/// together to form complex logic.
/// </remarks>
public interface IBdoScriptword : IBdoMetaObject, ITSingleChildParent<IBdoScriptword>, IBdoExpression
{
    /// <summary>
    /// Gets or sets the specific kind of this script token (Function, Variable, etc.).
    /// </summary>
    ScriptTokenKinds TokenKind { get; set; }

    /// <summary>
    /// Traverses the chain of scriptwords to find the final element.
    /// </summary>
    /// <param name="levelMax">The maximum depth to traverse to prevent infinite loops.</param>
    /// <returns>Returns the last <see cref="IBdoScriptword"/> in the current chain.</returns>
    /// <remarks>
    /// This is useful when the interpreter needs to append a new method to the end 
    /// of a fluent expression chain.
    /// </remarks>
    IBdoScriptword Last(int levelMax = 50);

    /// <summary>
    /// Formats the script word.
    /// </summary>
    /// <param name="fromRoot">If true, climbs to the root. Set to false for internal recursion.</param>
    string ToString(bool fromRoot);
}