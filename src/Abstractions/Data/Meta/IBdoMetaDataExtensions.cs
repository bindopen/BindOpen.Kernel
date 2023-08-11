namespace BindOpen.System.Data.Meta
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
            var spec = meta?.Spec;
            return spec == null || spec.OfGroup(groupId);
        }
    }
}
