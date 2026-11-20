using BindOpen.Data;
using BindOpen.Data.Conditions;

namespace BindOpen.Scoping.Script;

/// <summary>
/// This static class provides extension methods for <see cref="IBdoScriptword"/> 
/// to facilitate conversions to other BindOpen data structures.
/// </summary>
public static partial class BdoScriptwordExtensions
{
    /// <summary>
    /// Converts the scriptword into a data reference.
    /// </summary>
    /// <param name="word">The scriptword to wrap.</param>
    /// <returns>Returns a new <see cref="IBdoReference"/> that points to the result of this scriptword.</returns>
    /// <remarks>
    /// This allows a script (e.g., "$env:Var1") to be treated as a source for data injection.
    /// </remarks>
    public static IBdoReference ToReference(this IBdoScriptword word)
    {
        return BdoData.NewRef(word);
    }

    /// <summary>
    /// Converts the scriptword into a logical condition.
    /// </summary>
    /// <param name="word">The scriptword to wrap.</param>
    /// <returns>Returns a new <see cref="IBdoCondition"/> based on the scriptword's evaluation.</returns>
    /// <remarks>
    /// This is typically used for workflow constraints or requirement checks where the script 
    /// returns a boolean value.
    /// </remarks>
    public static IBdoCondition ToCondition(this IBdoScriptword word)
    {
        return BdoData.NewCondition(word);
    }
}