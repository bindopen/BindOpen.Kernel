using BindOpen.System.Data.Helpers;
using System.Collections.Generic;

namespace BindOpen.System.Data
{
    /// <summary>
    /// This interface represents an described data item.
    /// </summary>
    public static class ITParentExtensions
    {
        public static IEnumerable<T> Children<T>(
            this ITParent<T> parent,
            bool isRecursive)
            where T : class, IReferenced
            => parent == null ? default : parent.Children(null, isRecursive);

        public static T FirstChild<T>(
            this ITParent<T> parent)
            where T : class, IReferenced
            => parent == null ? default : parent.Child(null, false);

        public static T Child<T>(
            this ITParent<T> parent,
            string id,
            bool isRecursive = false)
            where T : class, IReferenced
            => parent == null ? default : parent.Child(q => q.BdoKeyEquals(id), isRecursive);
    }
}
