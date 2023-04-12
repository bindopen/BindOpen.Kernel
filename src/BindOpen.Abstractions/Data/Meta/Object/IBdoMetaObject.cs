namespace BindOpen.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoMetaObject : IBdoMetaSet
    {
        new void Clear();

        /// <summary>
        /// 
        /// </summary>
        /// <param key="objs"></param>
        IBdoMetaObject WithData(object obj);
    }
}