using BindOpen.Data;
using System;

namespace BindOpen.Scoping;

/// <summary>
/// Base class for attributes used to define and index BindOpen extensions via reflection.
/// </summary>
/// <remarks>
/// This attribute allows for the declarative definition of extensions directly on C# classes,
/// providing the necessary metadata for the discovery engine.
/// </remarks>
[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
public abstract class MetaExtensionAttribute : Attribute, INamed, ITitled, IDescribed
{
    // ------------------------------------------
    // PROPERTIES
    // ------------------------------------------

    #region Properties

    /// <summary>
    /// Gets or sets the unique name of the extension.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the display title of the extension.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the detailed description of the extension.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the creation date of the extension definition.
    /// </summary>
    /// <remarks>Stored as a string for cross-platform serialization compatibility.</remarks>
    public string CreationDate { get; set; }

    /// <summary>
    /// Gets or sets the last modification date of the extension definition.
    /// </summary>
    public string LastModificationDate { get; set; }

    #endregion

    // ------------------------------------------
    // CONSTRUCTORS
    // ------------------------------------------

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the MetaExtensionAttribute class.
    /// </summary>
    protected MetaExtensionAttribute() : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the MetaExtensionAttribute class with a specified name.
    /// </summary>
    /// <param name="name">The unique name of the extension.</param>
    protected MetaExtensionAttribute(string name) : base()
    {
        Name = name;
    }

    #endregion
}