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
        public static T WithValueMode<T>(
            this T meta,
            DataValueMode mode)
            where T : IBdoMetaData
        {
            if (meta != null)
            {
                meta.ValueMode = mode;
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
                meta.DataValueType = valueType;
            }

            return meta;
        }

        /// <summary>
        /// 
        /// </summary>
        public static T WithDataReference<T>(
            this T meta,
            IBdoExpression reference)
            where T : IBdoMetaData
        {
            if (meta != null)
            {
                meta.DataReference = reference;
            }

            return meta;
        }
    }
}
