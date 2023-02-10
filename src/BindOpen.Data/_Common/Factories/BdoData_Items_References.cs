using BindOpen.Data.Items;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents a data exp that can contain a literal and script texts.
    /// </summary>
    public static partial class BdoData
    {
        /// <summary>
        /// Creates the exp.
        /// </summary>
        /// <param name="kind">The kind of exp to consider.</param>
        /// <param name="text">The text to consider.</param>
        /// <returns>Returns the created exp.</returns>
        public static BdoReference NewReference()
            => new();
    }
}