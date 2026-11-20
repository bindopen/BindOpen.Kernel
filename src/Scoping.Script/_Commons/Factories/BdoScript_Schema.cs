using BindOpen.Data.Schema;

namespace BindOpen.Scoping.Script;

/// <summary>
/// This static class provides factory methods to create and navigate script elements.
/// </summary>
public static partial class BdoScript
{
    /// <summary>
    /// Navigates to the parent of the specified schema scriptword.
    /// </summary>
    /// <param name="schema">The current schema scriptword.</param>
    /// <returns>Returns a scriptword representing the parent schema.</returns>
    public static ITBdoScriptword<IBdoSchema> _Parent(this ITBdoScriptword<IBdoSchema> schema)
        => schema.Var<IBdoSchema>("parent");

    /// <summary>
    /// Retrieves the name property of the specified schema scriptword.
    /// </summary>
    /// <param name="schema">The current schema scriptword.</param>
    /// <returns>Returns a scriptword representing the schema name as a string.</returns>
    public static ITBdoScriptword<string> _Name(this ITBdoScriptword<IBdoSchema> schema)
        => schema.Var<string>("name");

    /// <summary>
    /// Evaluates the value associated with the specified schema scriptword.
    /// </summary>
    /// <param name="schema">The current schema scriptword.</param>
    /// <returns>Returns a scriptword representing the boolean result of the schema value.</returns>
    public static ITBdoScriptword<bool> Value(this ITBdoScriptword<IBdoSchema> schema)
        => schema.Func<bool>("value");

    /// <summary>
    /// Checks if the specified schema scriptword contains a sub-schema with the given name.
    /// </summary>
    /// <param name="schema">The current schema scriptword.</param>
    /// <param name="name">The name of the sub-schema to look for.</param>
    /// <returns>Returns a scriptword evaluating to true if the sub-schema exists.</returns>
    public static ITBdoScriptword<bool> _Has(this ITBdoScriptword<IBdoSchema> schema, string name)
        => schema.Func<bool>("has", name);

    /// <summary>
    /// Navigates to a specific descendant within the schema hierarchy.
    /// </summary>
    /// <param name="schema">The starting schema scriptword.</param>
    /// <param name="tokens">The path tokens to the descendant.</param>
    /// <returns>Returns a scriptword representing the target descendant schema.</returns>
    public static ITBdoScriptword<IBdoSchema> _Descendant(this ITBdoScriptword<IBdoSchema> schema, params object[] tokens)
        => schema.Func<IBdoSchema>("descendant", tokens);
}