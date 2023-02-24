namespace BindOpen.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITBdoMetaData<TItem, TSpec> : ITBdoMetaData<TItem>
        where TSpec : IBdoSpec
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        new TSpec NewSpec();

        /// <summary>
        /// 
        /// </summary>
        new TSpec GetSpec(string name = null);

        /// <summary>
        /// 
        /// </summary>
        ITBdoMetaData<TItem, TSpec> WithSpecs(params TSpec[] specs);
    }
}