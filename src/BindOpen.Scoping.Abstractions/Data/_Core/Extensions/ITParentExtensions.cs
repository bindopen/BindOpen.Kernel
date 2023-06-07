using BindOpen.Scoping.Data.Helpers;

namespace BindOpen.Scoping.Data
{
    /// <summary>
    /// This interface represents an described data item.
    /// </summary>
    public static class ITParentExtensions
    {
        public static T Child<T>(
            this ITParent<T> obj,
            string id, bool isRecursive = false)
            where T : IReferenced
            => obj == null ? default : obj.Child(q => q.BdoKeyEquals(id), isRecursive);
    }
}
