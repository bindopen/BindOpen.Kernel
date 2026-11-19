namespace BindOpen.Scoping.Script;

/// <summary>
/// This static class provides factory methods to create script elements for text manipulation.
/// </summary>
public static partial class BdoScript
{
    /// <summary>
    /// Creates a scriptword that converts the specified object to its text representation.
    /// </summary>
    /// <param name="obj">The object to convert.</param>
    /// <returns>Returns a scriptword representing the text conversion.</returns>
    /// <remarks>
    /// Often used to ensure that complex objects or numbers are treated as strings 
    /// before further processing.
    /// </remarks>
    public static ITBdoScriptword<string> Text(
        object obj)
        => Func<string>("text", obj);

    /// <summary>
    /// Creates a scriptword that concatenates multiple objects into a single string.
    /// </summary>
    /// <param name="parameters">The objects or scriptwords to concatenate.</param>
    /// <returns>Returns a scriptword representing the joined string.</returns>
    /// <example>
    /// BdoScript.Concat("Hello ", BdoScript.Var("userName")) results in "{{concat('Hello ', $userName)}}"
    /// </example>
    public static ITBdoScriptword<string> Concat(
        params object[] parameters)
        => Func<string>("concat", parameters);
}