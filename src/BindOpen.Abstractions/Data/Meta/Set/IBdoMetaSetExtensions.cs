using BindOpen.Data.Helpers;
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

        public static IBdoMetaData GetFromGroup(
            this IBdoMetaSet set,
            string key = null,
            string groupId = StringHelper.__Star)
        {
            IBdoMetaData meta = null;

            if (set != null)
            {
                if (key == null && groupId == StringHelper.__Star)
                    return set.FirstOrDefault();

                meta = set.FirstOrDefault(p =>
                    (groupId == p.GroupId
                    || groupId == StringHelper.__Star
                    || groupId.BdoKeyEquals(p.GroupId))
                    && p.BdoKeyEquals(key));
            }

            return meta;
        }

        public static bool Has(
            this IBdoMetaSet set,
            string key,
            string groupId = StringHelper.__Star)
        {
            return set?.Any(p =>
                (groupId == p.GroupId
                || groupId == StringHelper.__Star
                || groupId.BdoKeyEquals(p.GroupId))
                && p.BdoKeyEquals(key)) == true;
        }
    }
}