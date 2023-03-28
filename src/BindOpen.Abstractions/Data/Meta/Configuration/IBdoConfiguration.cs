namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This interface defines a configuration.
    /// </summary>
    public interface IBdoConfiguration :
        IBdoMetaSet,
        IBdoTitled, IBdoDescribed,
        IStorable, IBdoDefinable, IBdoUsing
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="items"></param>
        /// <returns></returns>
        new IBdoConfiguration Add(
            params IBdoMetaData[] items);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="items"></param>
        /// <returns></returns>
        new IBdoConfiguration With(
            params IBdoMetaData[] items);
    }
}