using System.Linq;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// 
    /// </summary>
    public static class ITBdoItemSetExtensions
    {
        /// <summary>
        /// Removes the item with the specified name.
        /// </summary>
        /// <param name="keys">The keys of the item to remove.</param>
        public static ITBdoItemSet<T> Remove<T>(
            this ITBdoItemSet<T> obj,
            params string[] keys)
            where T : IReferenced
        {
            if (obj?.Items != null && keys != null)
            {
                obj.Items.RemoveAll(p => keys.Any(q => p.BdoKeyEquals(q)));
            }

            return obj;
        }
    }
}