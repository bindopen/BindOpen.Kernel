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
        public static BdoMetaSpecSet NewSpecSet(params IBdoMetaDataSpec[] specs)
            => NewSpecSet<BdoMetaSpecSet>(specs);

        // Static T creators -------------------------

        /// <summary>
        /// Instantiates a new instance of the BdoElementSet class.
        /// </summary>
        /// <param name="elems">The elems to consider.</param>
        public static BdoMetaSpecSet NewSpecSet<T>(params IBdoMetaDataSpec[] specs)
            where T : class, IBdoMetaSpecSet, new()
        {
            var elemSpecSet = new BdoMetaSpecSet()
                .WithItems(specs) as BdoMetaSpecSet;

            return elemSpecSet;
        }
    }
}
