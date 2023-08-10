using BindOpen.System.Data.Meta;
using BindOpen.System.Scoping.Script;

namespace BindOpen.System.Data
{
    /// <summary>
    /// This interface defines a storable data item.
    /// </summary>
    public static class DataReferencedExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        public static T WithDataReference<T>(
            this T meta,
            IBdoExpression exp)
            where T : IBdoDataReferenced
        {
            return meta.WithDataReference(BdoData.NewRef(exp));
        }

        /// <summary>
        /// 
        /// </summary>
        public static T WithDataReference<T>(
            this T meta,
            IBdoScriptword word)
            where T : IBdoDataReferenced
        {
            return meta.WithDataReference(BdoData.NewRef(word));
        }

        /// <summary>
        /// 
        /// </summary>
        public static T WithDataReference<T>(
            this T meta,
            string identifier)
            where T : IBdoDataReferenced
        {
            return meta.WithDataReference(BdoData.NewRef(identifier));
        }

        /// <summary>
        /// 
        /// </summary>
        public static T WithDataReference<T>(
            this T meta,
            IBdoMetaData target)
            where T : IBdoDataReferenced
        {
            return meta.WithDataReference(BdoData.NewRef(target));
        }
    }
}
