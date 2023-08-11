using BindOpen.System.Logging;
using BindOpen.System.Scoping;

namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// This class represents a meta data.
    /// </summary>
    public static partial class BdoMetaDataExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        public static T WithGroupId<T>(
            this T meta,
            string groupId)
            where T : IBdoMetaData
        {
            if (meta != null)
            {
                meta.GetOrAddSpec()
                    .WithGroupId(groupId);
            }

            return meta;
        }

        public static IBdoSpec GetOrAddSpec(this IBdoMetaData meta)
        {
            if (meta != null)
            {
                var spec = meta.Spec ??= BdoData.NewSpec();
                return spec;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="modes"></param>
        public static T WithSpec<T>(
            this T meta,
            IBdoSpec spec)
            where T : IBdoMetaData
        {
            if (meta != null)
            {
                meta.Spec = spec;
            }
            return meta;
        }

        /// <summary>
        /// 
        /// </summary>
        public static T WithLabel<T>(
            this T meta,
            string label)
            where T : IBdoMetaData
        {
            if (meta != null)
            {
                meta.GetOrAddSpec().Label = label;
            }

            return meta;
        }

        /// <summary>
        /// 
        /// </summary>
        public static T WithLabel<T>(
            this T meta,
            LabelFormats label)
            where T : IBdoMetaData
        {
            if (meta != null)
            {
                meta.GetOrAddSpec().Label = label.GetScript();
            }

            return meta;
        }

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
                varSet ??= BdoData.NewMetaComposite();
                varSet.Add((BdoData.__This, meta));

                var exp = meta.GetOrAddSpec().Label.ToExpression();
                var label = scope?.Interpreter?.Evaluate<string>(exp, varSet, log);
                return label;
            }

            return null;
        }
    }
}
