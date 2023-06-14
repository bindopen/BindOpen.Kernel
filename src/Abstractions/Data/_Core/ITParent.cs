using System;
using System.Collections.Generic;

namespace BindOpen.System.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITParent<T> where T : IReferenced
    {
        IList<T> _Children { get; set; }

        IEnumerable<T> Children(Predicate<T> filter = null);

        T Child(Predicate<T> filter = null, bool isRecursive = false);

        bool HasChild(Predicate<T> filter = null);

        T InsertChild(Action<T> updater);
    }
}