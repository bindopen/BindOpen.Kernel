using System;

namespace BindOpen.Data
{
    /// <summary>
    /// This interface defines a parent object.
    /// </summary>
    public interface ITParent<T> : IReferenced where T : IReferenced
    {
        /// <summary>
        /// The children of this object.
        /// </summary>
        ITBdoSet<T> _Children { get; set; }

        /// <summary>
        /// Inserts a new child using the specified updater.
        /// </summary>
        /// <param name="updater">The updater action used to update the new child object.</param>
        /// <returns>Returns the new child object.</returns>
        Q InsertChild<Q>(Action<Q> updater) where Q : T, new();

        /// <summary>
        /// Removes the specified child.
        /// </summary>
        /// <param name="filter">The filter to consider.</param>
        /// <param name="isRecursive">Indicates whether the search is recursive.</param>
        void RemoveChildren(Predicate<T> filter = null, bool isRecursive = false);
    }
}