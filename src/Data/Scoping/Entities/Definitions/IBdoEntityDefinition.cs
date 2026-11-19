using BindOpen.Data;
using BindOpen.Data.Assemblies;
using BindOpen.Data.Objects.Interfaces;

namespace BindOpen.Scoping.Entities;

/// <summary>
/// Defines the structural blueprint for a BindOpen entity.
/// </summary>
/// <remarks>
/// Entity definitions map logical business objects to physical C# classes 
/// and provide metadata for visual representation (viewers).
/// </remarks>
public interface IBdoEntityDefinition : IBdoExtensionDefinition, IBdoRuntimeTyped
{
    /// <summary>
    /// Gets or sets the reference to the C# class associated with this entity.
    /// </summary>
    /// <value>A reference containing assembly and class name information.</value>
    IBdoClassReference ClassReference { get; set; }

    /// <summary>
    /// Gets or sets a dictionary of viewer keys used to identify specialized UI components 
    /// for this entity across different platforms.
    /// </summary>
    ITBdoDictionary<string> ViewerDictionary { get; set; }
}