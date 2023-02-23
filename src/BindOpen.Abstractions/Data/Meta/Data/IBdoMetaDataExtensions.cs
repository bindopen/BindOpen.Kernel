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
        public static T WithDataExpression<T>(
            this T meta,
            IBdoExpression expression)
            where T : IBdoMetaData
        {
            if (meta != null)
            {
                meta.DataExpression = expression;
            }

            return meta;
        }

        /// <summary>
        /// 
        /// </summary>
        public static T WithParent<T>(
            this T meta,
            IBdoMetaData parent)
            where T : IBdoMetaData
        {
            if (meta != null)
            {
                meta.Parent = parent;
            }

            return meta;
        }
    }
}
