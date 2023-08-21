using System;
using System.Collections.Generic;

namespace BindOpen.System.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITParent<T> : IReferenced where T : IReferenced
    {
        ITBdoSet<T> _Children { get; set; }

        IEnumerable<T> Children(Predicate<T> filter = null, bool isRecursive = false);

        T Child(Predicate<T> filter, bool isRecursive = false);

        bool HasChild(Predicate<T> filter = null, bool isRecursive = false);

        T InsertChild(Action<T> updater);

        void RemoveChildren(Predicate<T> filter = null, bool isRecursive = false);
    }
}