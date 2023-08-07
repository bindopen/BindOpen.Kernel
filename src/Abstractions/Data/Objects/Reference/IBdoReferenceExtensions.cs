using BindOpen.System.Data.Meta;
using BindOpen.System.Scoping.Script;

namespace BindOpen.System.Data
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
                reference.Kind = BdoReferenceKind.Word;
                reference.Word = word;
            }

            return reference;
        }

        /// <summary>
        /// Removes the item with the specified name.
        /// </summary>
        /// <param key="keys">The keys of the item to remove.</param>
        public static T WithExpression<T>(
            this T reference,
            IBdoExpression exp)
            where T : IBdoReference
        {
            if (reference != null)
            {
                reference.Kind = BdoReferenceKind.Expression;
                reference.Expression = exp;
            }

            return reference;
        }

        /// <summary>
        /// Removes the item with the specified name.
        /// </summary>
        /// <param key="keys">The keys of the item to remove.</param>
        public static T WithMetaData<T>(
            this T reference,
            IBdoMetaData meta)
            where T : IBdoReference
        {
            if (reference != null)
            {
                reference.Kind = BdoReferenceKind.MetaData;
                reference.MetaData = meta;
            }

            return reference;
        }

        /// <summary>
        /// Removes the item with the specified name.
        /// </summary>
        /// <param key="keys">The keys of the item to remove.</param>
        public static T WithIdentifier<T>(
            this T reference,
            string identifier)
            where T : IBdoReference
        {
            if (reference != null)
            {
                reference.Kind = BdoReferenceKind.Identifier;
                reference.Identifier = identifier;
            }

            return reference;
        }
    }
}