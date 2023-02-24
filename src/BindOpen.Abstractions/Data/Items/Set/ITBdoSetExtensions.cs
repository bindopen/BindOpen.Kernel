using BindOpen.Data.Helpers;
using BindOpen.Data.Items;
using System.Linq;

namespace BindOpen.Data
{
    /// <summary>
    /// 
    /// </summary>
    public static class ITBdoSetExtensions
    {
        /// <summary>
        /// Removes the item with the specified name.
        /// </summary>
        /// <param key="keys">The keys of the item to remove.</param>
        public static ITBdoSet<T> Remove<T>(
            this ITBdoSet<T> obj,
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