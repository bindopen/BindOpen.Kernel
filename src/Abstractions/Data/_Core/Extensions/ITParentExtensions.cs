using BindOpen.System.Data.Helpers;

namespace BindOpen.System.Data
{
    /// <summary>
    /// This interface represents an described data item.
    /// </summary>
    public static class ITParentExtensions
    {
        public static T Child<T>(
            this ITParent<T> parent,
            string id, bool isRecursive = false)
            where T : class, IReferenced
            => parent == null ? default : parent.Child(q => q.BdoKeyEquals(id), isRecursive);

        public static T Descendant<T>(
            this ITParent<T> parent,
            params object[] tokens)
            where T : class, IReferenced
        {
            if (parent != null)
            {
                IReferenced current = parent;
                foreach (object token in tokens)
                {
                    if (token is string key)
                    {
                        current = parent.Child(q => q.BdoKeyEquals(key));
                        if (current == null) break;
                    }
                    else if (token is int index)
                    {
                        current = parent._Children?[index];
                        if (current == null) break;
                    }
                }

                return current as T;
            }

            return default;
        }
    }
}
