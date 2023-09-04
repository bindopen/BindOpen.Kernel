using BindOpen.System.Logging;
using BindOpen.System.Scoping;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// This class represents a meta data.
    /// </summary>
    public static partial class BdoMetaDataExtensions
    {
        public static void Update<T>(
            this T meta,
            IBdoMetaData refItem,
            string[] areas = null,
            UpdateModes[] updateModes = null,
            IBdoLog log = null)
            where T : IBdoMetaData
        {
            if (meta != null)
            {
                areas ??= new[] { nameof(DataAreaKind.Any) };
                updateModes ??= new[] { UpdateModes.Incremental_AddMissingInTarget, UpdateModes.Incremental_UpdateCommon };

                if (updateModes.Has(UpdateModes.Incremental_UpdateCommon))
                {
                    meta.WithDataType(refItem.DataType?.Clone<BdoDataType>());
                    meta.WithReference(refItem.Reference?.Clone<BdoReference>());
                    meta.WithData(refItem.GetData());
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static T WithGroupId<T>(
            this T meta,
            string groupId)
            where T : IBdoMetaData
        {
            meta?.GetOrAddSpec()
                    .WithGroupId(groupId);

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
                varSet ??= BdoData.NewSet();
                varSet.Add((BdoData.__VarName_This, meta));

                var exp = meta.GetOrAddSpec().Label.ToExpression();
                var label = scope?.Interpreter?.Evaluate<string>(exp, varSet, log);
                return label;
            }

            return null;
        }

        /// <summary>
        /// Creates a meta data of the specified object.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static IBdoMetaNode AsMetaNode(
            this IBdoMetaData meta)
            => meta as IBdoMetaNode;

        /// <summary>
        /// Creates a data element list from a dynamic object.
        /// </summary>
        /// <param key="obj">The objet to consider.</param>
        public static IBdoMetaData[] ToArray(
            this IBdoMetaData meta)
            => meta.ToList()?.ToArray();

        /// <summary>
        /// Creates a data element list from a dynamic object.
        /// </summary>
        /// <param key="obj">The objet to consider.</param>
        public static IList<IBdoMetaData> ToList(
            this IBdoMetaData meta)
        {
            if (meta is IBdoMetaNode metaSet)
            {
                return metaSet.ToList();
            }

            return null;
        }
    }
}
