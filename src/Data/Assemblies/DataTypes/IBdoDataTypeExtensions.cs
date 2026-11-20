using BindOpen.Data.Assemblies;
using BindOpen.Scoping;

namespace BindOpen.Data;

/// <summary>
/// Provides extension methods for IBdoDataType to facilitate fluent configuration 
/// and type classification.
/// </summary>
public static class IBdoDataTypeExtensions
{
    /// <summary>
    /// Sets the fundamental value type for this data type and returns the instance for chaining.
    /// </summary>
    /// <typeparam name="T">The type implementing IBdoDataType.</typeparam>
    /// <param name="dataType">The data type instance to update.</param>
    /// <param name="valueType">The target value type (e.g., Number, Text, Boolean).</param>
    /// <returns>Returns the updated instance.</returns>
    public static T WithValueType<T>(
        this T dataType,
        DataValueTypes valueType)
        where T : IBdoDataType
    {
        if (dataType != null)
        {
            dataType.ValueType = valueType;
        }

        return dataType;
    }

    /// <summary>
    /// Configures the data type based on a specific extension kind and unique name.
    /// </summary>
    /// <typeparam name="T">The type implementing IBdoDataType.</typeparam>
    /// <param name="dataType">The data type instance to update.</param>
    /// <param name="definitionExtensionKind">The extension kind used to infer the value type.</param>
    /// <param name="definitionUniqueName">The unique name of the definition to reference.</param>
    /// <returns>Returns the updated instance.</returns>
    public static T WithDefinition<T>(
        this T dataType,
        BdoExtensionKinds definitionExtensionKind,
        string definitionUniqueName = null)
        where T : IBdoDataType
    {
        if (dataType != null)
        {
            // Infers the fundamental ValueType from the extension kind logic
            dataType.ValueType = definitionExtensionKind.GetValueType();

            if (definitionUniqueName != null)
            {
                dataType.WithDefinition(definitionUniqueName);
            }
        }
        return dataType;
    }

    /// <summary>
    /// Checks if the data type represents a scalar value (e.g., Text, Number, Date) 
    /// rather than a set or a complex object.
    /// </summary>
    /// <param name="dataType">The data type to evaluate.</param>
    /// <returns>True if the type is a scalar; otherwise, false.</returns>
    public static bool IsScalar(this IBdoDataType dataType)
    {
        return dataType?.ValueType.IsScalar() ?? false;
    }
}