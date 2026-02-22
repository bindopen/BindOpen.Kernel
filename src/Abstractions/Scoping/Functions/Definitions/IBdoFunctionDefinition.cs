using BindOpen.Data;
using BindOpen.Data.Assemblies;
using BindOpen.Data.Schema;
using System;

namespace BindOpen.Scoping.Functions;

/// <summary>
/// Defines the structural blueprint and runtime logic for a BindOpen function.
/// </summary>
/// <remarks>
/// Functions are granular logic units used for data transformation and script execution.
/// This definition maps logical function names to physical C# delegates or class methods.
/// </remarks>
public interface IBdoFunctionDefinition : IBdoExtensionDefinition
{
    /// <summary>
    /// Gets or sets the collection of additional schemas required to validate 
    /// the inputs or outputs of this function.
    /// </summary>
    ITBdoSet<IBdoSchema> AdditionalSchemas { get; set; }

    /// <summary>
    /// Gets or sets the data type of the parent object (the object this function is called on).
    /// </summary>
    /// <remarks>Used for fluent scripting, similar to extension methods in C#.</remarks>
    IBdoDataType ParentDataType { get; set; }

    /// <summary>
    /// Gets or sets the expected data type of the value returned by this function.
    /// </summary>
    IBdoDataType OutputDataType { get; set; }

    /// <summary>
    /// Gets or sets the physical C# type where the function logic is implemented.
    /// </summary>
    Type RuntimeClassType { get; set; }

    /// <summary>
    /// Gets or sets the generic delegate associated with this function.
    /// </summary>
    Delegate RuntimeFunction { get; set; }

    /// <summary>
    /// Gets or sets the scoped delegate that has access to the execution domain.
    /// </summary>
    BdoFunctionDomainedDelegate RuntimeScopedFunction { get; set; }

    /// <summary>
    /// Gets or sets the basic delegate for standard function execution.
    /// </summary>
    BdoFunctionDelegate RuntimeBasicFunction { get; set; }
}