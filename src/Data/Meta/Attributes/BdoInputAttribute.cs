using BindOpen.Scoping;
using System;

namespace BindOpen.Data.Meta;

/// <summary>
/// Represents a data element attribute used to identify input properties.
/// This attribute is typically used on task or component properties to signal 
/// that they should be populated from an input metadata set.
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class BdoInputAttribute : BdoPropertyAttribute
{
    // ------------------------------------------
    // CONSTRUCTORS
    // ------------------------------------------

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the BdoInputAttribute class.
    /// </summary>
    public BdoInputAttribute() : this(null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the BdoInputAttribute class with a specified name.
    /// </summary>
    /// <param name="name">The name of the input property.</param>
    public BdoInputAttribute(string name) : base(name)
    {
        // We automatically assign the Input token group ID to distinguish 
        // these properties from outputs or configuration settings.
        GroupId = IBdoTaskExtensions.__Token_Input;
    }

    #endregion
}