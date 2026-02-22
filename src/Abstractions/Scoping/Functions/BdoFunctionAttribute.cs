using System;

namespace BindOpen.Scoping;

/// <summary>
/// This class represents a decorative attribute used to identify and register 
/// methods or properties as BindOpen functions.
/// </summary>
/// <remarks>
/// Apply this attribute to C# methods to allow the discovery engine to expose them 
/// as scriptwords within the BindOpen Scripting Language (BOSL).
/// </remarks>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true)]
public class BdoFunctionAttribute : MetaExtensionAttribute
{
    // ------------------------------------------
    // CONSTRUCTORS
    // ------------------------------------------

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="BdoFunctionAttribute"/> class.
    /// </summary>
    public BdoFunctionAttribute() : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BdoFunctionAttribute"/> class with a specified name.
    /// </summary>
    /// <param name="name">The logical name of the function (e.g., "text:concat", "math:sum").</param>
    public BdoFunctionAttribute(string name) : base(name)
    {
    }

    #endregion
}