namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// This interface defines a configuration.
    /// </summary>
    public interface IBdoDefinition :
        ITBdoSet<IBdoSpec>,
        IBdoTitled, IBdoDescribed,
        INamed,
        IIndexed, IStorable, IBdoUsing
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="items"></param>
        /// <returns></returns>
        new IBdoDefinition Add(
            params IBdoSpec[] items);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="items"></param>
        /// <returns></returns>
        new IBdoDefinition With(
            params IBdoSpec[] items);
    }
}