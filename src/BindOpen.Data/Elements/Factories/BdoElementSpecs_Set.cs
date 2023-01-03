namespace BindOpen.Data.Elements
{
    /// <summary>
    /// This static class provides methods to create element specifications.
    /// </summary>
    public static partial class BdoElements
    {
        // Static creators -------------------------

        /// <summary>
        /// Instantiates a new instance of the BdoElementSet class.
        /// </summary>
        /// <param name="specs">The elements to consider.</param>
        public static BdoElementSpecSet NewSpecSet(params IBdoElementSpec[] specs)
            => NewSpecSet<BdoElementSpecSet>(specs);

        // Static T creators -------------------------

        /// <summary>
        /// Instantiates a new instance of the BdoElementSet class.
        /// </summary>
        /// <param name="elements">The elements to consider.</param>
        public static BdoElementSpecSet NewSpecSet<T>(params IBdoElementSpec[] specs)
            where T : class, IBdoElementSpecSet, new()
        {
            var elementSpecSet = new BdoElementSpecSet()
                .WithItems(specs) as BdoElementSpecSet;

            return elementSpecSet;
        }
    }
}
