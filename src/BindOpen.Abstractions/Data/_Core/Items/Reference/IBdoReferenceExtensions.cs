using BindOpen.Script;

namespace BindOpen.Data
{
    /// <summary>
    /// 
    /// </summary>
    public static class IBdoReferenceExtensions
    {
        /// <summary>
        /// Removes the item with the specified name.
        /// </summary>
        /// <param key="keys">The keys of the item to remove.</param>
        public static T WithWord<T>(
            this T reference,
            IBdoScriptword word)
            where T : IBdoReference
        {
            if (reference != null)
            {
                reference.Word = word;
            }

            return reference;
        }
    }
}