namespace BindOpen.Scoping.Script;

/// <summary>
/// Provides extension methods for constructing and navigating <see cref="IBdoScriptword"/> chains.
/// </summary>
public static class IBdoScriptwordExtensions
{
    /// <summary>
    /// Sets the token kind of the scriptword.
    /// </summary>
    /// <typeparam name="T">The type of scriptword.</typeparam>
    /// <param name="word">The scriptword to configure.</param>
    /// <param name="kind">The kind of token to assign.</param>
    /// <returns>Returns the specified scriptword for fluent chaining.</returns>
    public static T WithTokenKind<T>(
        this T word,
        ScriptTokenKinds kind)
        where T : IBdoScriptword
    {
        if (word != null)
        {
            word.TokenKind = kind;
        }

        return word;
    }

    /// <summary>
    /// Sets the child scriptword, creating a bi-directional link.
    /// </summary>
    /// <typeparam name="T">The type of scriptword.</typeparam>
    /// <param name="word">The current scriptword (parent).</param>
    /// <param name="scriptword">The scriptword to add as a child.</param>
    /// <returns>Returns the parent scriptword.</returns>
    public static T WithChild<T>(
        this T word, IBdoScriptword scriptword)
        where T : IBdoScriptword
    {
        if (word != null)
        {
            word.Child = scriptword;
        }

        if (scriptword != null)
        {
            scriptword.Parent = word;
        }

        return word;
    }

    /// <summary>
    /// Sets the parent of the current scriptword by adding it as a child to the specified parent.
    /// </summary>
    /// <typeparam name="T">The type of scriptword.</typeparam>
    /// <param name="word">The current scriptword.</param>
    /// <param name="scriptword">The scriptword to set as parent.</param>
    /// <returns>Returns the current scriptword.</returns>
    public static T WithParent<T>(
        this T word, IBdoScriptword scriptword)
        where T : IBdoScriptword
    {
        scriptword?.WithChild(word);

        return word;
    }

    /// <summary>
    /// Recursively traverses the script chain to find the very last child.
    /// </summary>
    /// <param name="word">The starting scriptword.</param>
    /// <returns>Returns the final <see cref="IBdoScriptword"/> in the chain.</returns>
    public static IBdoScriptword LastChild(this IBdoScriptword word)
    {
        return word?.Child == null ? word : word.Child.LastChild();
    }
}