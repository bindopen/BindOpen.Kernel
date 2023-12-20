using BindOpen.Data.Meta;
using BindOpen.Scoping.Script;

namespace BindOpen.Data
{
    /// <summary>
    /// This interface defines a storable data item.
    /// </summary>
    public static class ReferencedExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        public static T WithReference<T>(
            this T meta,
            IBdoExpression exp)
            where T : IBdoReferenced
        {
            return meta.WithReference(BdoData.NewRef(exp));
        }

        /// <summary>
        /// 
        /// </summary>
        public static T WithReference<T>(
            this T meta,
            IBdoScriptword word)
            where T : IBdoReferenced
        {
            return meta.WithReference(BdoData.NewRef(word));
        }

        /// <summary>
        /// 
        /// </summary>
        public static T WithReference<T>(
            this T meta,
            string identifier)
            where T : IBdoReferenced
        {
            return meta.WithReference(BdoData.NewRef(identifier));
        }

        /// <summary>
        /// 
        /// </summary>
        public static T WithReference<T>(
            this T meta,
            IBdoMetaData target)
            where T : IBdoReferenced
        {
            return meta.WithReference(BdoData.NewRef(target));
        }
    }
}
