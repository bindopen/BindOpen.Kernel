namespace BindOpen.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoMetaScalar :
        ITBdoMetaData<IBdoMetaScalar, IBdoMetaScalarSpec, object>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objs"></param>
        new IBdoMetaScalar WithItems(
            params object[] objs);
    }
}