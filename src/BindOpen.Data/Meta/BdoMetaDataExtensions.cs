using BindOpen.Logging;
using BindOpen.Scopes;
using BindOpen.Script;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a meta data.
    /// </summary>
    public static partial class BdoMetaDataExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        public static string GetLabel(
            this IBdoMetaData meta,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            if (meta != null)
            {
                varSet ??= BdoMeta.NewSet();
                varSet.Add((BdoData.__This, meta));

                var label = scope?.Interpreter?.Evaluate<string>(meta.Label.ToExpression(), varSet, log);
                return label;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        public static T WithDataReference<T>(
            this T meta,
            IBdoExpression exp)
            where T : IBdoMetaData
        {
            return meta.WithDataReference(BdoData.NewRef(exp));
        }

        /// <summary>
        /// 
        /// </summary>
        public static T WithDataReference<T>(
            this T meta,
            IBdoScriptword word)
            where T : IBdoMetaData
        {
            return meta.WithDataReference(BdoData.NewRef(word));
        }

        /// <summary>
        /// 
        /// </summary>
        public static T WithDataReference<T>(
            this T meta,
            string identifier)
            where T : IBdoMetaData
        {
            return meta.WithDataReference(BdoData.NewRef(identifier));
        }

        /// <summary>
        /// 
        /// </summary>
        public static T WithDataReference<T>(
            this T meta,
            IBdoMetaData target)
            where T : IBdoMetaData
        {
            return meta.WithDataReference(BdoData.NewRef(target));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="modes"></param>
        public static T WithSpecs<T>(
            this T meta,
            params IBdoSpec[] specs)
            where T : IBdoMetaData
        {
            if (meta != null)
            {
                meta.Specs = BdoMeta.NewSpecSet(specs);
            }
            return meta;
        }
    }
}
