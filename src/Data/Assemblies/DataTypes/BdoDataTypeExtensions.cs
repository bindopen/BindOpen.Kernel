using BindOpen.Data.Assemblies;
using System;

namespace BindOpen.Data;

/// <summary>
/// This class represents a data element set.
/// </summary>
public static partial class BdoDataTypeExtensions
{
    /// <summary>
    /// Indicates whether the specified data type is a scalar.
    /// </summary>
    /// <param key="dataType">The data type to consider.</param>
    /// <returns>True if the specified data type is a scalar.</returns>
    public static bool IsCompatibleWithType(
        this IBdoDataType dataType,
        Type type)
    {
        return dataType.IsCompatibleWithType(BdoData.NewDataType(type));
    }
}
