using BindOpen.Data.Meta;
using BindOpen.Logging;

namespace BindOpen.Scoping.Script.Standard;

/// <summary>
/// This static class contains the standard implementation of metadata-related script functions.
/// </summary>
public static class Functions
{
    /// <summary>
    /// Retrieves the data value of the specified metadata element within the current script domain.
    /// </summary>
    /// <param name="data">The metadata element to evaluate.</param>
    /// <param name="scriptDomain">The current script execution domain providing context.</param>
    /// <returns>Returns the evaluated data object.</returns>
    [BdoFunction("value")]
    public static object BdoValue(
        this IBdoMetaData data,
        IBdoScriptDomain scriptDomain = null)
    {
        // Extracts data using the scope, variables, and log from the domain
        return data?.GetData(scriptDomain?.Scope, scriptDomain?.VariableSet, scriptDomain?.Log);
    }

    /// <summary>
    /// Retrieves a specific descendant from a metadata set based on a path of tokens.
    /// </summary>
    /// <param name="set">The metadata set to search.</param>
    /// <param name="tokens">The path tokens (names or indexes) identifying the descendant.</param>
    /// <returns>Returns the found <see cref="IBdoMetaData"/>; otherwise, null.</returns>
    [BdoFunction("descendant")]
    public static IBdoMetaData BdoDescendant(
        this IBdoMetaSet set,
        params object[] tokens)
    {
        return set?.Descendant<IBdoMetaData>(tokens);
    }

    /// <summary>
    /// Checks for the existence of a descendant within a metadata set.
    /// </summary>
    /// <param name="set">The metadata set to check.</param>
    /// <param name="tokens">The path tokens identifying the potential descendant.</param>
    /// <returns>Returns <c>true</c> if the descendant exists; otherwise, <c>false</c>.</returns>
    [BdoFunction("has")]
    public static bool BdoHas(
        this IBdoMetaSet set,
        params object[] tokens)
    {
        // Existence check by attempting to find the descendant
        return set?.Descendant<IBdoMetaData>(tokens) != null;
    }
}