using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data;

/// <summary>
/// Provides extension methods for the IBdoMerger interface to facilitate collection merging.
/// </summary>
public static class IBdoMergerExtensions
{
    /// <summary>
    /// Merges the specified values with the additions and removals defined in the merger.
    /// </summary>
    /// <param name="values">The base list of values to process.</param>
    /// <param name="merger">The merger containing the delta (additions and removals).</param>
    /// <returns>Returns a new list representing the result of the merge operation.</returns>
    public static IList<string> Merge(
        this IList<string> values,
        IBdoMerger merger)
    {
        var all = new List<string>();

        if (values != null)
        {
            all.AddRange(values);

            // 1. Process Additions
            if (merger?.AddedValues != null)
            {
                all.AddRange(merger.AddedValues);
                // Ensure uniqueness after adding new values
                all = all.Distinct()?.ToList();
            }

            // 2. Process Removals
            if (merger?.RemovedValues != null)
            {
                all.RemoveAll(p => merger.RemovedValues.Contains(p));
            }
        }

        return all ?? new List<string>();
    }

    /// <summary>
    /// Determines whether a value is present in the collection after the merge operation is applied.
    /// </summary>
    /// <param name="values">The base list of values.</param>
    /// <param name="value">The value to locate.</param>
    /// <param name="merger">The merger defining the delta.</param>
    /// <returns>True if the value exists in the resulting merged set; otherwise, false.</returns>
    public static bool Contains(
        this IList<string> values,
        string value,
        IBdoMerger merger)
    {
        // We evaluate the full merge to check for existence
        return values?.Merge(merger).Contains(value) ?? false;
    }
}