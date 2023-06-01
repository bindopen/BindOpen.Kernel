using System;
using System.Collections.Generic;

namespace BindOpen.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITParent<T> where T : IReferenced
    {
        IList<T> Children { get; set; }

        IEnumerable<T> GetChildren(Predicate<T> filter);

        T Child(Predicate<T> filter = null, bool isRecursive = false);

        bool HasChild(Predicate<T> filter = null);

        T InsertChild(Action<T> updater);
    }
}