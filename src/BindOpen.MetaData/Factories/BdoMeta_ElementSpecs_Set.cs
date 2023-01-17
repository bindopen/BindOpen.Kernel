using BindOpen.MetaData.Elements;

namespace BindOpen.MetaData
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
        /// <param name="specs">The elements to consider.</param>
        public static BdoMetaElementSpecSet NewSpecSet(params IBdoMetaElementSpec[] specs)
            => NewSpecSet<BdoMetaElementSpecSet>(specs);

        // Static T creators -------------------------

        /// <summary>
        /// Instantiates a new instance of the BdoElementSet class.
        /// </summary>
        /// <param name="elements">The elements to consider.</param>
        public static BdoMetaElementSpecSet NewSpecSet<T>(params IBdoMetaElementSpec[] specs)
            where T : class, IBdoMetaElementSpecSet, new()
        {
            var elemSpecSet = new BdoMetaElementSpecSet()
                .WithItems(specs) as BdoMetaElementSpecSet;

            return elemSpecSet;
        }
    }
}
