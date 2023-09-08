using BindOpen.Kernel.Data;
using System;

namespace BindOpen.Kernel
{
    /// <summary>
    /// This interface represents a parent object.
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
        T InsertChild(Action<T> updater);

        /// <summary>
        /// Removes the specified child.
        /// </summary>
        /// <param name="filter">The filter to consider.</param>
        /// <param name="isRecursive">Indicates whether the search is recursive.</param>
        void RemoveChildren(Predicate<T> filter = null, bool isRecursive = false);
    }
}