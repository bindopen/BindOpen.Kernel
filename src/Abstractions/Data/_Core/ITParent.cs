using System;

namespace BindOpen.System.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITParent<T> : IReferenced where T : IReferenced
    {
        ITBdoSet<T> _Children { get; set; }

        T InsertChild(Action<T> updater);

        void RemoveChildren(Predicate<T> filter = null, bool isRecursive = false);
    }
}