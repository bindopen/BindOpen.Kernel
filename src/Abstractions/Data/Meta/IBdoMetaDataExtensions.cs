using BindOpen.Data.Meta;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    public static partial class IBdoMetaDataExtensions
    {
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

        public static bool OfGroup(
            this IBdoMetaData meta,
            string groupId)
        {
            var schema = meta?.Schema;
            return schema == null || schema.OfGroup(groupId);
        }
    }
}
