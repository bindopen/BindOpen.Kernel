using BindOpen.Kernel.Data.Helpers;

namespace BindOpen.Kernel.Data
{
    /// <summary>
    /// This class extends the IGrouped interface.
    /// </summary>
    public static class IGroupedExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="groupId"></param>
        public static T WithGroupId<T>(
            this T obj,
            string groupId)
            where T : IGrouped
        {
            if (obj != null)
            {
                obj.GroupId = groupId;
            }

            return obj;
        }

        public static bool OfGroup<T>(
            this T obj,
            string groupId)
            where T : IGrouped
        {
            return
                obj != null &&
                (groupId == obj.GroupId
                    || groupId == StringHelper.__Star
                    || groupId.BdoKeyEquals(obj?.GroupId));
        }
    }
}