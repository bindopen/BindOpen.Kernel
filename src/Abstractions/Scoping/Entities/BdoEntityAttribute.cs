using System;

namespace BindOpen.Scoping;

/// <summary>
/// This class represents a decorative attribute used to identify and configure 
/// BindOpen entities via reflection.
/// </summary>
/// <remarks>
/// Use this attribute to register business data models within the scoping system.
/// This allows the Kernel to manage the class as a defined entity.
/// </remarks>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class BdoEntityAttribute : MetaExtensionAttribute
{
    // ------------------------------------------
    // CONSTRUCTORS
    // ------------------------------------------

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="BdoEntityAttribute"/> class.
    /// </summary>
    public BdoEntityAttribute() : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BdoEntityAttribute"/> class with a specified name.
    /// </summary>
    /// <param name="name">The unique name/ID of the entity (e.g., "company.project$invoice").</param>
    public BdoEntityAttribute(string name) : base(name)
    {
    }

    #endregion
}