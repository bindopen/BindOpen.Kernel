using BindOpen.Scoping;
using System;

namespace BindOpen.Data.Meta;

/// <summary>
/// Represents a data element attribute used to identify output properties.
/// This attribute marks properties within a task or component that should be 
/// collected as output metadata after execution.
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class BdoOutputAttribute : BdoPropertyAttribute
{
    // ------------------------------------------
    // CONSTRUCTORS
    // ------------------------------------------

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the BdoOutputAttribute class.
    /// </summary>
    public BdoOutputAttribute() : this(null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the BdoOutputAttribute class with a specified name.
    /// </summary>
    /// <param name="name">The name of the output property.</param>
    public BdoOutputAttribute(string name) : base(name)
    {
        // We automatically assign the Output token group ID to ensure 
        // the execution engine identifies this as data to be exported.
        GroupId = IBdoTaskExtensions.__Token_Output;
    }

    #endregion
}