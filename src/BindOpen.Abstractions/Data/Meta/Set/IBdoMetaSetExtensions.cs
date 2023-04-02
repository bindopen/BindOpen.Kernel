using BindOpen.Data.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public static class IBdoMetaSetExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        public static IBdoMetaData Descendant(
            this IBdoMetaSet set,
            params string[] names)
        {
            return set?.Descendant<IBdoMetaData>(names);
        }

        public static IBdoMetaData GetOfGroup(
            this IBdoMetaSet set,
            string key,
            string groupId)
        {
            IBdoMetaData meta = null;

            if (set != null)
            {
                meta = set.FirstOrDefault(p =>
                    p.OfGroup(groupId)
                    && (key == null || p.BdoKeyEquals(key)));
            }

            return meta;
        }

        public static IEnumerable<IBdoMetaData> GetOfGroup(
            this IBdoMetaSet set,
            string groupId)
        {
            IEnumerable<IBdoMetaData> metas = new List<IBdoMetaData>();

            if (set != null)
            {
                metas = set.Where(p => p.OfGroup(groupId));
            }

            return metas;
        }

        public static bool HasGroup(
            this IBdoMetaSet set,
            string groupId = null)
        {
            return set?.Any(p =>
                groupId == null && p.GroupId != null
                || groupId != null && p.OfGroup(groupId)) == true;
        }

        public static bool Has(
            this IBdoMetaSet set,
            string key,
            string groupId = StringHelper.__Star)
        {
            return set?.Any(p =>
                p.OfGroup(groupId)
                && p.BdoKeyEquals(key)) == true;
        }
    }
}