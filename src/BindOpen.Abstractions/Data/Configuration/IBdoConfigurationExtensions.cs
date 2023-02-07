using BindOpen.Data.Configuration;
using System.Linq;

namespace BindOpen.Data
{
    /// <summary>
    /// 
    /// </summary>
    public static class IBdoConfigurationExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static T Using<T>(
            this T obj,
            params string[] ids)
            where T : IBdoConfiguration
        {
            if (obj != null)
            {
                obj.UsedItemIds = ids?.ToList();
            }
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="detail"></param>
        public static T WithDefinitionUniqueId<T>(
            this T obj,
            string definitionUniqueId)
            where T : IBdoConfiguration
        {
            if (obj != null)
            {
                obj.DefinitionUniqueId = definitionUniqueId;
            }
            return obj;
        }
    }
}