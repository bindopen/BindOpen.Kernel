namespace BindOpen.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public static class IBdoMetaObjectExtensions
    {

        /// <summary>
        /// 
        /// </summary>
        public static T WithProperties<T>(
            this T meta,
            IBdoMetaSet set)
            where T : IBdoMetaObject
        {
            if (meta != null)
            {
                meta.PropertySet = set;
            }
            //var item = set?.FirstOrDefault();
            //if (item is IBdoConfiguration config
            //    && !string.IsNullOrEmpty(item.DefinitionUniqueId))
            //{
            //    ClassReference = BdoData.ClassFromEntity(
            //        item?.DefinitionUniqueId);
            //}
            return meta;
        }
    }
}