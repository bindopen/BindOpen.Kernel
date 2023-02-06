using BindOpen.Data.Meta;

namespace BindOpen.Data
{
    /// <summary>
    /// This static class provides methods to create element specifications.
    /// </summary>
    public static partial class BdoMeta
    {
        // Static creators -------------------------

        /// <summary>
        /// Instantiates a new instance of the BdoElementSet class.
        /// </summary>
        /// <param name="specs">The elems to consider.</param>
        public static BdoMetaSpecList NewSpecList(params IBdoMetaSpec[] specs)
            => NewSpecList<BdoMetaSpecList>(specs);

        // Static T creators -------------------------

        /// <summary>
        /// Instantiates a new instance of the BdoElementSet class.
        /// </summary>
        /// <param name="elems">The elems to consider.</param>
        public static BdoMetaSpecList NewSpecList<T>(params IBdoMetaSpec[] specs)
            where T : class, IBdoMetaSpecList, new()
        {
            var elemSpecSet = new BdoMetaSpecList()
                .With(specs) as BdoMetaSpecList;

            return elemSpecSet;
        }
    }
}
