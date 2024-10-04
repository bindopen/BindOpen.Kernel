using BindOpen.Data.Helpers;

namespace BindOpen.Data.Assemblies;

/// <summary>
/// This static class provides methods to create extension items.
/// </summary>
public static class DataTypeDbExtensions
{
    public static IBdoDataType Repair(IBdoDataType poco)
    {
        if (poco != null)
        {
            poco.Identifier ??= StringHelper.NewGuid();
        }

        return poco;
    }
}
