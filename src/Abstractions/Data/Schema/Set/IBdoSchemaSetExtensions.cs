using BindOpen.Data.Meta;
using BindOpen.Logging;
using BindOpen.Scoping;
using System.Linq;

namespace BindOpen.Data.Schema;

/// <summary>
/// 
/// </summary>
public static class IBdoSchemaSetExtensions
{
    /// <summary>
    /// 
    /// </summary>
    public static IBdoSchema Child(
        this IBdoSchemaSet specSet,
        IBdoScope scope,
        IBdoMetaSet varSet = null,
        IBdoLog log = null)
    {
        IBdoSchema schema = null;

        if (specSet != null)
        {
            schema = specSet.FirstOrDefault(
                q => scope?.Interpreter?.Evaluate(q?.Condition, varSet, log) == true);

            schema ??= specSet?.FirstOrDefault(q => q?.Condition == null);
        }

        return schema;
    }
}