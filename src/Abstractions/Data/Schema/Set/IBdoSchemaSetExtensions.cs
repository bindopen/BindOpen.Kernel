using BindOpen.Data.Meta;
using BindOpen.Logging;
using BindOpen.Scoping;
using System.Linq;

namespace BindOpen.Data.Schema;

/// <summary>
/// Provides extension methods for IBdoSchemaSet to handle dynamic schema resolution.
/// </summary>
public static class IBdoSchemaSetExtensions
{
    /// <summary>
    /// Resolves the appropriate child schema from the set based on evaluated conditions.
    /// </summary>
    /// <param name="specSet">The schema set to search within.</param>
    /// <param name="scope">The scope used to evaluate conditions via the interpreter.</param>
    /// <param name="varSet">The set of variables available for condition resolution.</param>
    /// <param name="log">The log for tracking evaluation details.</param>
    /// <returns>
    /// Returns the first schema whose condition evaluates to true. 
    /// If no conditional schema matches, returns the first schema without a condition (the default).
    /// </returns>
    public static IBdoSchema Child(
        this IBdoSchemaSet specSet,
        IBdoScope scope,
        IBdoMetaSet varSet = null,
        IBdoLog log = null)
    {
        IBdoSchema schema = null;

        if (specSet != null)
        {
            // 1. Try to find the first schema where the condition is met
            schema = specSet.FirstOrDefault(
                q => scope?.Interpreter?.Evaluate(q?.Condition, varSet, log) == true);

            // 2. Fallback to the first schema that has no condition (default schema)
            schema ??= specSet.FirstOrDefault(q => q?.Condition == null);
        }

        return schema;
    }
}