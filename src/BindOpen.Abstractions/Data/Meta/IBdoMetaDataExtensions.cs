using BindOpen.Data.Items;
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
        public static T WithValueType<T>(
            this T meta,
            DataValueTypes valueType)
            where T : IBdoMetaData
        {
            if (meta != null)
            {
                meta.ValueType = valueType;
            }

            return meta;
        }

        /// <summary>
        /// 
        /// </summary>
        public static T WithReference<T>(
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

        /// <summary>
        /// 
        /// </summary>
        public static T WithExpression<T>(
            this T meta,
            IBdoExpression exp)
            where T : IBdoMetaData
        {
            if (meta != null)
            {
                meta.Expression = exp;
            }

            return meta;
        }
    }
}
