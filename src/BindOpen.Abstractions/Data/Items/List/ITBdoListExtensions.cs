using BindOpen.Data.Helpers;
using BindOpen.Data.Items;
using System.Linq;

namespace BindOpen.Data
{
    /// <summary>
    /// 
    /// </summary>
    public static class ITBdoListExtensions
    {
        /// <summary>
        /// Removes the item with the specified name.
        /// </summary>
        /// <param name="keys">The keys of the item to remove.</param>
        public static ITBdoList<T> Remove<T>(
            this ITBdoList<T> obj,
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