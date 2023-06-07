namespace BindOpen.Scoping.Data.Meta
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
            params object[] tokens)
        {
            IBdoMetaData currentMeta = list?[configName];

            if (currentMeta is IBdoMetaSet currentMetaSet)
            {
                return currentMetaSet.Descendant<IBdoMetaData>(tokens);
            }

            return null;
        }
    }
}