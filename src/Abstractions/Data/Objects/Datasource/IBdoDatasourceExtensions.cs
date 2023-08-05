using BindOpen.System.Data.Meta;

namespace BindOpen.System.Data
{
    /// <summary>
    /// 
    /// </summary>
    public static class IBdoDatasourceExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="text"></param>
        /// <returns></returns>
        public static T WithInstanceName<T>(
            this T source,
            string instanceName)
            where T : IBdoDatasource
        {
            if (source != null)
            {
                source.InstanceName = instanceName;
            }

            return source;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="text"></param>
        /// <returns></returns>
        public static T WithKind<T>(
            this T source,
            DatasourceKind kind)
            where T : IBdoDatasource
        {
            if (source != null)
            {
                source.Kind = kind;
            }

            return source;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="text"></param>
        /// <returns></returns>
        public static T WithModuleName<T>(
            this T source,
            string moduleName)
            where T : IBdoDatasource
        {
            if (source != null)
            {
                source.ModuleName = moduleName;
            }

            return source;
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param key="items">The items of the item to add.</param>
        /// <returns>Returns the new item that has been added.
        /// Returns null if the new item is null or else its name is null.</returns>
        /// <remarks>The new item must have a name.</remarks>
        public static T Add<T>(
           this T set,
           params IBdoMetaObject[] items)
           where T : IBdoDatasource
        {
            if (set != null && items != null)
            {
                foreach (var item in items)
                {
                    set.Insert(item);
                }
            }

            return set;
        }

        /// <summary>
        /// Sets the specified single item of this instance.
        /// </summary>
        /// <param key="items">The items to apply to this instance.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the values will be the default ones..</remarks>
        public static T With<T>(
           this T set,
           params IBdoMetaObject[] items)
           where T : IBdoDatasource
        {
            if (set != null)
            {
                set.Clear();
                set.Add(items);
            }

            return set;
        }
    }
}