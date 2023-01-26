namespace BindOpen.Abstractions.Data._Common.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ITBdoItemSetExtensions
    {
        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="items">The items of the item to add.</param>
        /// <returns>Returns the new item that has been added.
        /// Returns null if the new item is null or else its name is null.</returns>
        /// <remarks>The new item must have a name.</remarks>
        public static ITBdoItemSet<T> Add<T>(
            this ITBdoItemSet<T> obj,
            params T[] items)
            where T : IReferenced
        {
            if (obj != null && items != null)
            {
                foreach (var item in items)
                {
                    obj.Insert(item);
                }
            }

            return obj;
        }

        /// <summary>
        /// Sets the specified single item of this instance.
        /// </summary>
        /// <param name="items">The items to apply to this instance.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the values will be the default ones..</remarks>
        public static ITBdoItemSet<T> WithItems<T>(
            this ITBdoItemSet<T> obj,
            params T[] items)
            where T : IReferenced
        {
            if (obj != null)
            {
                obj.ClearItems();
                Add(obj, items);
            }

            return obj;
        }
    }
}