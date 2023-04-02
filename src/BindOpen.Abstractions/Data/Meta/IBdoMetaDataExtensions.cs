using BindOpen.Scopes;
using BindOpen.Data.Helpers;
using BindOpen.Logging;
using System.Linq;

namespace BindOpen.Data.Meta
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
        public static T WithDataValueType<T>(
            this T meta,
            DataValueTypes valueType)
            where T : IBdoMetaData
        {
            if (meta != null)
            {
                meta.DataValueType = valueType;
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
                meta.Reference = reference;
            }

            return meta;
        }

        public static T WithGroupId<T>(
            this T meta,
            string groupId)
            where T : IBdoMetaData
        {
            if (meta != null)
            {
                meta.GroupId = groupId;
            }

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
            }

            return spec;
        }

        public static bool OfGroup(
            this IBdoMetaData meta,
            string groupId)
        {
            return
                meta != null &&
                (groupId == meta.GroupId
                    || groupId == StringHelper.__Star
                    || groupId.BdoKeyEquals(meta.GroupId));
        }
    }
}
