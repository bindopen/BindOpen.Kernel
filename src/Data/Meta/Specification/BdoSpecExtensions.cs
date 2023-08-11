using BindOpen.System.Data.Assemblies;
using BindOpen.System.Logging;
using BindOpen.System.Scoping;

namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    public static partial class BdoSpecExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="isAllocatable"></param>
        /// <returns></returns>
        public static T AsAllocatable<T>(
            this T spec,
            bool isAllocatable = true)
            where T : IBdoSpec
        {
            if (spec != null)
            {
                spec.IsAllocatable = isAllocatable;
            }
            return spec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsCompatibleWith(
            this ITBdoSet<IBdoSpec> specs,
            ITBdoSet<IBdoMetaData> metas,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            if (specs == null || metas == null) return false;

            var b = true;

            var iObj = 0;
            var iSpec = 0;
            while (iSpec < specs.Count && iObj < metas.Count)
            {
                var spec = specs[iSpec];

                var obj = metas[iObj].GetData(scope, varSet, log);
                b &= spec?.IsCompatibleWithData(obj) ?? true;

                iSpec++;

                iObj++;
            }

            return b;
        }

        public static IBdoSpec ToSpec(
            this IBdoMetaData meta,
            string name = null,
            bool onlyMetaAttributes = true)
        {
            IBdoSpec spec = null;

            if (meta != null)
            {
                if (meta is IBdoMetaComposite metaComposite)
                {
                    spec = BdoData.NewSpec<BdoAggregateSpec>();

                    var aggreagateSpec = spec as IBdoAggregateSpec;

                    foreach (var subMeta in metaComposite)
                    {
                        var subSpec = subMeta.ToSpec(null, onlyMetaAttributes);
                        aggreagateSpec.AddChildren(subSpec);
                    }
                }
                else
                {
                    spec = BdoData.NewSpec();
                }

                spec.Update(meta.Spec);
                spec.Name = name;
                spec.Name ??= meta.Name;
                spec.DataType = meta.DataType;
            }

            return spec;
        }
        public static T ToSpec<T>(
            this IBdoMetaData meta,
            string name = null,
            bool onlyMetaAttributes = true)
            where T : class, IBdoSpec, new()
        {
            var spec = AssemblyHelper.CreateInstance<T>();

            var metaSpec = meta.ToSpec(name, onlyMetaAttributes);
            spec.Update(metaSpec);

            if (spec is IBdoAggregateSpec aggregateSpec
                && metaSpec is IBdoAggregateSpec metaAggregateSpec)
            {
                aggregateSpec._Children = metaAggregateSpec._Children;
            }

            return spec;
        }
    }
}
