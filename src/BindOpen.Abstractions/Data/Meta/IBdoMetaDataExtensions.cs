using BindOpen.Data.Items;

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
        /// <param name="objs"></param>
        public static T WithData<T>(
            this T meta,
            object obj)
            where T : IBdoMetaData
        {
            if (meta != null)
            {
                obj = obj.ToBdoData(meta?.GetSpec());
                meta.WithDataList(obj);
            }
            return meta;
        }

        /// <summary>
        /// 
        /// </summary>
        public static T WithItemizationMode<T>(
            this T meta,
            DataItemizationMode mode)
            where T : IBdoMetaData
        {
            if (meta != null)
            {
                meta.ItemizationMode = mode;
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
                meta.DataReference = reference;
            }

            return meta;
        }

        /// <summary>
        /// 
        /// </summary>
        public static T WithDataExpression<T>(
            this T meta,
            IBdoExpression exp)
            where T : IBdoMetaData
        {
            if (meta != null)
            {
                meta.DataExpression = exp;
            }

            return meta;
        }
    }
}
