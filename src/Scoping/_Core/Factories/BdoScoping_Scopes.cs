using BindOpen.Scoping.Script;
using System;

namespace BindOpen.Scoping;

/// <summary>
/// This static class provides factory methods to create and initialize scoping objects.
/// </summary>
public static partial class BdoScoping
{
    /// <summary>
    /// Creates and initializes a new instance of a BindOpen scope.
    /// </summary>
    /// <param name="appDomain">The application domain to associate with the new scope.</param>
    /// <returns>Returns a newly initialized <see cref="IBdoScope"/> instance.</returns>
    /// <remarks>
    /// This method automatically loads basic script extensions by scanning 
    /// the assembly containing <see cref="BdoScriptword"/>.
    /// </remarks>
    public static IBdoScope NewScope(AppDomain appDomain = null)
    {
        // Instantiate the scope with the provided AppDomain
        var scope = new BdoScope(appDomain);

        // Automatically load core script extensions to ensure the interpreter is functional
        scope.LoadExtensions(options => options.AddAssemblyFrom<BdoScriptword>());

        return scope;
    }
}