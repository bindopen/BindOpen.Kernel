using System;
using System.Collections.Generic;

namespace BindOpen.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITParent<T> where T : IReferenced
    {
        IEnumerable<T> Children(Predicate<T> filter = null);

        T Child(Predicate<T> filter = null, bool isRecursive = false);

        bool HasChild(Predicate<T> filter = null);

        T InsertChild(T child);

        T InsertChild(Action<T> updater);
    }
}