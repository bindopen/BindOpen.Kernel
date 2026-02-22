namespace BindOpen.Scoping.Functions;

// --------------------------------------------------
// DELEGATES
// --------------------------------------------------

#region Delegates

/// <summary>
/// Represents the standard delegate for executing a BindOpen function.
/// </summary>
/// <returns>Returns the result of the function execution as an <see cref="object"/>.</returns>
/// <remarks>
/// This delegate is used by the scripting engine to invoke parameterless methods 
/// registered within the function dictionary.
/// </remarks>
public delegate object BdoFunctionDelegate();

#endregion
