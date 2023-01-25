namespace BindOpen.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoMetaObject :
        ITBdoMetaData<IBdoMetaObject, IBdoMetaObjectSpec, object>
    {
        /// <summary>
        /// 
        /// </summary>
        IBdoMetaSet SubSet { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoMetaObject WithSubSet(IBdoMetaSet set);

        /// <summary>
        /// 
        /// </summary>
        IBdoMetaObject WithSubSet(params IBdoMetaData[] metas);

        /// <summary>
        /// 
        /// </summary>
        string ClassFullName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoMetaObject WithClassFullName(string classFullName);

        /// <summary>
        /// 
        /// </summary>
        string DefinitionUniqueId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoMetaObject WithDefinitionUniqueId(string definitionUniqueId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objs"></param>
        new IBdoMetaObject WithItems(
            params object[] objs);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IBdoMetaObject UpdateTree();
    }
}