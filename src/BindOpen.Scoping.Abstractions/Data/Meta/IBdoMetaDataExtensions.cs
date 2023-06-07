using BindOpen.Scoping.Data.Helpers;
using BindOpen.Logging;
using BindOpen.Scoping.Scopes;
using System.Linq;

namespace BindOpen.Scoping.Data.Meta
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    public static partial class IBdoMetaDataExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        public static T WithDataMode<T>(
            this T meta,
            DataMode mode)
            where T : IBdoMetaData
        {
            if (meta != null)
            {
                meta.DataMode = mode;
            }

            return meta;
        }

        /// <summary>
        /// 
        /// </summary>
        public static T WithDataReference<T>(
            this T meta,
            IBdoReference reference)
            where T : IBdoMetaData
        {
            if (meta != null)
            {
                meta.WithDataMode(DataMode.Reference);
                meta.Reference = reference;
            }

            return meta;
        }

        /// <summary>
        /// 
        /// </summary>
        public static T WithData<T>(
            this T meta,
            object obj)
            where T : IBdoMetaData
        {
            meta?.SetData(obj);

            return meta;
        }

        /// <summary>
        /// 
        /// </summary>
        public static IBdoSpec GetSpec(
            this IBdoMetaData meta,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            IBdoSpec spec = null;

            if (meta != null)
            {
                spec = meta.Specs?.FirstOrDefault(
                    q => q?.Condition.Evaluate(scope, varSet, log) == true);

                spec ??= meta.Specs?.FirstOrDefault(q => q?.Condition == null);
            }

            return spec;
        }

        public static bool OfGroup(
            this IBdoMetaData meta,
            string groupId)
        {
            var spec = meta?.GetSpec();
            return
                meta != null &&
                (groupId == spec?.GroupId
                    || groupId == StringHelper.__Star
                    || groupId.BdoKeyEquals(spec?.GroupId));
        }
    }
}
