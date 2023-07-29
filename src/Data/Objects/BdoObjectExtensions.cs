using BindOpen.System.Data.Meta;
using BindOpen.System.Data.Meta.Reflection;
using BindOpen.System.Scoping.Functions;

namespace BindOpen.System.Data
{
    /// <summary>
    /// This class represents a data item.
    /// </summary>
    /// <remarks>The data item has only an ID, a creation and a last-modification dates.</remarks>
    public static class BdoObjectExtensions
    {
        /// <summary>
        /// Returns the item TItem of this instance.
        /// </summary>
        /// <param key="log">The log to populate.</param>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="varSet">The variable meta set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        [BdoFunction("prop")]
        public static object Property(
            this IBdoObject obj,
            string propName)
        {
            return obj?.GetPropertyValue(propName, typeof(BdoPropertyAttribute));
        }
    }
}
