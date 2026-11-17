using System.Collections.Generic;

namespace BindOpen.Data.Objects.Interfaces;

/// <summary>
/// Defines a data merger used to manage the addition and removal of values.
/// A merger tracks changes to a collection, allowing for sophisticated 
/// synchronization and delta-processing.
/// </summary>
public interface IBdoMerger : IBdoObjectNotMetable, IIdentified, IReferenced
{
    /// <summary>
    /// Gets or sets the list of values to be added.
    /// </summary>
    IList<string> AddedValues { get; set; }

    /// <summary>
    /// Gets or sets the list of values to be removed.
    /// </summary>
    IList<string> RemovedValues { get; set; }

    /// <summary>
    /// Converts the resulting state of this merger into a list of strings.
    /// </summary>
    /// <returns>A list representing the merged result.</returns>
    IList<string> ToList();

    /// <summary>
    /// Converts the resulting state of this merger into an array of strings.
    /// </summary>
    /// <returns>An array representing the merged result.</returns>
    string[] ToArray();

    /// <summary>
    /// Determines whether the specified value is contained within the resulting merged set.
    /// </summary>
    /// <param name="value">The value to locate.</param>
    /// <returns>True if the value is present in the merged result; otherwise, false.</returns>
    bool Contains(string value);
}