using System;

namespace BindOpen.Scoping;

/// <summary>
/// This class represents a decorative attribute used to identify and register 
/// classes as BindOpen tasks via reflection.
/// </summary>
/// <remarks>
/// Apply this attribute to classes implementing <see cref="IBdoTask"/> to 
/// enable the discovery engine to index them as executable units within the kernel.
/// </remarks>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class BdoTaskAttribute : MetaExtensionAttribute
{
    // ------------------------------------------
    // CONSTRUCTORS
    // ------------------------------------------

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="BdoTaskAttribute"/> class.
    /// </summary>
    public BdoTaskAttribute() : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BdoTaskAttribute"/> class with a specified name.
    /// </summary>
    /// <param name="name">The unique name/identifier of the task (e.g., "company.tools$data-importer").</param>
    public BdoTaskAttribute(string name) : base(name)
    {
    }

    #endregion
}