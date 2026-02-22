using BindOpen.Data;

namespace BindOpen.Scoping.Script;

/// <summary>
/// Defines a basic item within a script expression.
/// </summary>
/// <remarks>
/// Script items are the flat tokens resulting from the lexing process. 
/// They serve as the raw material for building higher-level <see cref="IBdoScriptword"/> trees.
/// </remarks>
public interface IScriptItem : IBdoObject, INamed, IIndexed
{
    /// <summary>
    /// Gets or sets the specific token kind of this script item.
    /// </summary>
    /// <value>The category of the token (e.g., Syntax, Text, or Function).</value>
    ScriptTokenKinds Kind { get; set; }
}