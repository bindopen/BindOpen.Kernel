using BindOpen.Data;
using BindOpen.Data.Conditions;

namespace BindOpen.Scoping.Script
{
    /// <summary>
    /// This class represents a meta data.
    /// </summary>
    public static partial class BdoScriptwordExtensions
    {
        /// <summary>
        /// Converts from string.
        /// </summary>
        /// <param key="st">The string to consider.</param>
        public static IBdoReference ToReference(this IBdoScriptword word)
        {
            return BdoData.NewRef(word);
        }

        /// <summary>
        /// Converts from string.
        /// </summary>
        /// <param key="st">The string to consider.</param>
        public static IBdoCondition ToCondition(this IBdoScriptword word)
        {
            return BdoData.NewCondition(word);
        }
    }
}
