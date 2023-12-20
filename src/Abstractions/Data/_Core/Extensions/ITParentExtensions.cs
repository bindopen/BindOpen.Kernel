using BindOpen.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data
{
    /// <summary>
    /// This interface represents an described data item.
    /// </summary>
    public static class ITParentExtensions
    {

        public static IEnumerable<T> Children<T>(
            this ITParent<T> parent,
            Predicate<T> filter = null,
            bool isRecursive = false)
            where T : class, IReferenced
        {
            var children = new List<T>();

            var parentChildren = parent?._Children;
            if (parentChildren != null)
            {
                foreach (var child in parentChildren)
                {
                    if (filter?.Invoke(child) != false)
                        children.Add(child);

                    if (isRecursive && child is ITParent<T> parentChild)
                    {
                        children.AddRange(parentChild.Children(filter, isRecursive));
                    }
                }
            }

            return children;
        }

        public static T Child<T>(
            this ITParent<T> parent,
            Predicate<T> filter = null,
            bool isRecursive = false)
            where T : class, IReferenced
        {
            var parentChildren = parent?._Children;
            if (parentChildren != null)
            {
                foreach (var child in parentChildren)
                {
                    if (filter?.Invoke(child) != false)
                        return child;

                    if (isRecursive && child is ITParent<T> parentChild)
                    {
                        var subChild = parentChild?.Child(filter, true);
                        if (subChild != null) return subChild;
                    }
                }
            }

            return null;
        }

        public static bool HasChild<T>(
            this ITParent<T> parent,
            Predicate<T> filter = null,
            bool isRecursive = false)
            where T : class, IReferenced
            => parent?._Children?.Any(q => filter?.Invoke(q) != false || (isRecursive && q is ITParent<T> parentQ && parentQ.HasChild(filter, true))) == true;


        public static IEnumerable<T> Children<T>(
            this ITParent<T> parent,
            bool isRecursive)
            where T : class, IReferenced
            => parent == null ? default : parent.Children(null, isRecursive);

        public static T FirstChild<T>(
            this ITParent<T> parent)
            where T : class, IReferenced
            => parent == null ? default : parent.Child(null as Predicate<T>, false);

        public static T Child<T>(
            this ITParent<T> parent,
            string id,
            bool isRecursive = false)
            where T : class, IReferenced
            => parent == null ? default : parent.Child(q => q.BdoKeyEquals(id), isRecursive);
    }
}
