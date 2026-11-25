using BindOpen.Data;
using BindOpen.Data.Assemblies;
using BindOpen.Data.Objects;
using BindOpen.Data.Schema;

namespace BindOpen.Scoping.Tasks;

/// <summary>
/// Defines the metadata and structural requirements for a BindOpen task.
/// </summary>
/// <remarks>
/// Task definitions map logical execution units to physical classes and define 
/// the expected data schemas for their outputs.
/// </remarks>
public interface IBdoTaskDefinition : IBdoExtensionDefinition, IBdoRuntimeTyped
{
    /// <summary>
    /// Gets or sets the reference to the C# class that implements the task logic.
    /// </summary>
    IBdoClassReference ClassReference { get; set; }

    /// <summary>
    /// Gets or sets the collection of schemas defining the structure of the data 
    /// produced by this task.
    /// </summary>
    ITBdoSet<IBdoSchema> Outputs { get; set; }

    /// <summary>
    /// Gets or sets the maximum index or priority level for this task.
    /// </summary>
    /// <remarks>Often used by schedulers to determine execution order or complexity limits.</remarks>
    float MaximumIndex { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this task is currently executable in the host environment.
    /// </summary>
    bool IsExecutable { get; set; }
}