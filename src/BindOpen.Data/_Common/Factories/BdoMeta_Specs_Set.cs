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
        /// <param key="specs">The elems to consider.</param>
        public static BdoSpecSet NewSpecSet(params IBdoSpec[] specs)
            => NewSpecSet<BdoSpecSet>(specs);

        // Static T creators -------------------------

        /// <summary>
        /// Instantiates a new instance of the BdoElementSet class.
        /// </summary>
        /// <param key="elems">The elems to consider.</param>
        public static BdoSpecSet NewSpecSet<T>(params IBdoSpec[] specs)
            where T : class, IBdoSpecSet, new()
        {
            var elemSpecSet = new BdoSpecSet()
                .With(specs) as BdoSpecSet;

            return elemSpecSet;
        }
    }
}
