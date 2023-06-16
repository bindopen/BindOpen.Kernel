using BindOpen.System.Logging;
using BindOpen.System.Scoping;
using BindOpen.System.Scoping.Script;
using System;

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
        public static T WithDataType<T>(
            this T meta,
            BdoDataType dataType)
            where T : IBdoMetaData
        {
            meta?.GetOrAddSpec()
                    .WithDataType(dataType);

            return meta;
        }

        public static T WithDataType<T>(
            this T meta,
            DataValueTypes valueType,
            Type type = null)
            where T : IBdoMetaData
        {
            return WithDataType<T>(meta, BdoData.NewDataType(valueType, type));
        }

        public static IBdoSpec GetOrAddSpec(this IBdoMetaData meta)
        {
            var spec = meta.GetSpec();
            if (meta != null && spec == null)
            {
                meta.Specs ??= BdoMeta.NewSpecSet();
                spec = BdoMeta.NewSpec();
                meta.Specs.Add(spec);
            }

            return spec;
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
                varSet ??= BdoMeta.NewSet();
                varSet.Add((BdoData.__This, meta));

                var exp = meta.GetOrAddSpec().Label.ToExpression();
                var label = scope?.Interpreter?.Evaluate<string>(exp, varSet, log);
                return label;
            }

            return null;
        }
    }
}
