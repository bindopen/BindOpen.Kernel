using BindOpen.Data.Configuration;

namespace BindOpen.Data
{
    /// <summary>
    /// This interface represents an named data item.
    /// </summary>
    public static class IConfigurableExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public static T WithConfig<T>(
            this T obj,
            IBdoConfiguration config)
            where T : IConfigurable
        {
            if (obj != null)
            {
                obj.Config = config;
            }

            return obj;
        }
    }
}