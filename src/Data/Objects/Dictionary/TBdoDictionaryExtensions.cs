namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a meta data.
    /// </summary>
    public static partial class TBdoDictionaryExtensions
    {
        public static TMetaSet ToMetaset<TMetaSet, TItem>(
            this ITBdoDictionary<TItem> dictionary)
            where TMetaSet : class, IBdoMetaSet, new()
        {
            if (dictionary == null) return default;

            var set = BdoData.NewSet<TMetaSet>();

            foreach (var item in dictionary)
            {
                set.Add(item.Key, item.Value);
            }

            return set;
        }

        public static IBdoMetaSet ToMetaset<TItem>(
            this ITBdoDictionary<TItem> dictionary)
            => ToMetaset<BdoMetaSet, TItem>(dictionary);
    }
}
