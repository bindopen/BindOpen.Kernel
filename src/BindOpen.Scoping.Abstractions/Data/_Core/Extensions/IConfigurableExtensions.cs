using BindOpen.Scoping.Data.Meta;

namespace BindOpen.Scoping.Data
{
    /// <summary>
    /// This interface represents an named data item.
    /// </summary>
    public static class IConfigurableExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="name"></param>
        public static T WithConfig<T>(
            this T obj,
            IBdoConfiguration config)
            where T : IBdoConfigurable
        {
            if (obj != null)
            {
                obj.Config = config;
            }

            return obj;
        }
    }
}