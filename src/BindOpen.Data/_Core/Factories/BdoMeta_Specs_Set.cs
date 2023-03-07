using BindOpen.Data.Items;
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
        public static TBdoSet<IBdoSpec> NewSpecSet(params IBdoSpec[] specs)
            => NewSpecSet<TBdoSet<IBdoSpec>>(specs);

        // Static T creators -------------------------

        /// <summary>
        /// Instantiates a new instance of the BdoElementSet class.
        /// </summary>
        /// <param key="elems">The elems to consider.</param>
        public static T NewSpecSet<T>(params IBdoSpec[] specs)
            where T : class, ITBdoSet<IBdoSpec>, new()
        {
            var elemSpecSet = new T()
                .With(specs) as T;

            return elemSpecSet;
        }
    }
}
