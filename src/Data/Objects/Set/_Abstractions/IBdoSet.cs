using System.Collections;

namespace BindOpen.Data.Objects;

/// <summary>
/// Defines a set of referenced and identified objects.
/// This interface serves as the base contract for indexed collections 
/// within the BindOpen framework.
/// </summary>
public interface IBdoSet :
    IBdoObject,
    IIdentified, IReferenced,
    IEnumerable
{
    /// <summary>
    /// Gets the item associated with the specified key.
    /// </summary>
    /// <param name="key">The key of the item to retrieve.</param>
    /// <returns>The referenced item associated with the key.</returns>
    IReferenced this[string key] { get; }

    /// <summary>
    /// Gets the item at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the item to retrieve.</param>
    /// <returns>The referenced item at the specified index.</returns>
    IReferenced this[int index] { get; }

    /// <summary>
    /// Retrieves a descendant item based on a sequence of tokens (path).
    /// </summary>
    /// <typeparam name="T">The type of the descendant to retrieve.</typeparam>
    /// <param name="tokens">The path tokens leading to the desired descendant.</param>
    /// <returns>The found descendant of the specified type.</returns>
    T Descendant<T>(
        params object[] tokens)
        where T : IReferenced;
}