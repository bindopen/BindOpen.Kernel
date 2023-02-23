using BindOpen.Data.Items;
using BindOpen.Data.Meta;

namespace BindOpen.Data.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public static class IBdoConfigurationSetExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        public static IBdoMetaData Descendant(
            this IBdoConfigurationSet list,
            string configName,
            params string[] names)
        {
            IBdoMetaData currentMeta = list?[configName];

            if (currentMeta is IBdoMetaSet currentMetaSet)
            {
                return currentMetaSet.Descendant<IBdoMetaData>(names);
            }

            return null;
        }
    }
}