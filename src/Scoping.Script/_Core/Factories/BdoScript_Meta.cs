using BindOpen.Data.Meta;

namespace BindOpen.Scoping.Script;

/// <summary>
/// This static class provides factory methods to create and navigate script elements.
/// </summary>
public static partial class BdoScript
{
    /// <summary>
    /// Navigates to the parent of the specified metadata scriptword.
    /// </summary>
    /// <param name="meta">The current metadata scriptword.</param>
    /// <returns>Returns a scriptword representing the parent metadata.</returns>
    public static ITBdoScriptword<IBdoMetaData> _Parent(this ITBdoScriptword<IBdoMetaData> meta)
        => meta.Var<IBdoMetaData>("parent");

    /// <summary>
    /// Retrieves the name property of the specified metadata scriptword.
    /// </summary>
    /// <param name="meta">The current metadata scriptword.</param>
    /// <returns>Returns a scriptword representing the metadata name as a string.</returns>
    public static ITBdoScriptword<string> _Name(this ITBdoScriptword<IBdoMetaData> meta)
        => meta.Var<string>("name");

    /// <summary>
    /// Evaluates the value of the specified metadata scriptword.
    /// </summary>
    /// <param name="meta">The current metadata scriptword.</param>
    /// <returns>Returns a scriptword representing the boolean evaluation of the metadata value.</returns>
    public static ITBdoScriptword<bool> _Value(this ITBdoScriptword<IBdoMetaData> meta)
        => meta.Func<bool>("value");

    /// <summary>
    /// Checks if the specified metadata scriptword has a child with the given name.
    /// </summary>
    /// <param name="meta">The current metadata scriptword.</param>
    /// <param name="name">The name of the child to check for.</param>
    /// <returns>Returns a scriptword evaluating to true if the child exists.</returns>
    public static ITBdoScriptword<bool> _Has(this ITBdoScriptword<IBdoMetaData> meta, string name)
        => meta.Func<bool>("has", name);

    /// <summary>
    /// Navigates to a specific descendant within the metadata or schema hierarchy.
    /// </summary>
    /// <param name="schema">The starting metadata or schema scriptword.</param>
    /// <param name="tokens">The tokens identifying the path to the descendant.</param>
    /// <returns>Returns a scriptword representing the target descendant metadata.</returns>
    public static ITBdoScriptword<IBdoMetaData> _Descendant(this ITBdoScriptword<IBdoMetaData> schema, params object[] tokens)
        => schema.Func<IBdoMetaData>("descendant", tokens);
}