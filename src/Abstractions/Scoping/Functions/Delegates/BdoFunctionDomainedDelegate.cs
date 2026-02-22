using BindOpen.Scoping.Script;

namespace BindOpen.Scoping.Functions;

// --------------------------------------------------
// DELEGATES
// --------------------------------------------------

#region Delegates

/// <summary>
/// Represents a BindOpen function delegate that executes within a specific script domain.
/// </summary>
/// <param name="domain">The script domain providing access to the current execution context, 
/// including variables, scope, and parent objects.</param>
/// <returns>Returns the result of the function execution as an <see cref="object"/>.</returns>
/// <remarks>
/// This is the preferred delegate for functions that need to interact with the system 
/// state or resolve dynamic parameters during runtime.
/// </remarks>
public delegate object BdoFunctionDomainedDelegate(IBdoScriptDomain domain);

#endregion
