using System;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents a data element specification.
    /// </summary>
    public struct BdoDataType
    {
        // --------------------------------------------------
        // Properties
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The value type of this instance.
        /// </summary>
        public DataValueTypes ValueType { get; set; } = DataValueTypes.Any;

        /// <summary>
        /// The value type of this instance.
        /// </summary>
        public Type ClassType { get; set; }

        #endregion

        // --------------------------------------------------
        // Constructors
        // --------------------------------------------------

        #region Constructors

        public BdoDataType()
        {

        }

        #endregion

        // --------------------------------------------------
        // Converts
        // --------------------------------------------------

        #region Converts

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <param key="meta">The meta to consider.</param>
        public static explicit operator DataValueTypes(BdoDataType dataType)
        {
            return dataType.ValueType;
        }

        public static explicit operator BdoDataType(DataValueTypes valueType)
        {
            return new BdoDataType() { ValueType = valueType };
        }

        #endregion

        // --------------------------------------------------
        // Operators
        // --------------------------------------------------

        #region Operators

        public static bool operator ==(BdoDataType type1, BdoDataType type2)
        {
            return (
                    type1.ValueType == DataValueTypes.Any
                    || type2.ValueType == DataValueTypes.Any
                    || type1.ValueType == type2.ValueType)
                && type1.ClassType == type2.ClassType;
        }

        public static bool operator !=(BdoDataType type1, BdoDataType type2)
        {
            return !(type1 == type2);
        }

        public static bool operator ==(BdoDataType type1, DataValueTypes valueType)
        {
            return type1.ValueType == DataValueTypes.Any
                || type1.ValueType == valueType;
        }

        public static bool operator !=(BdoDataType type1, DataValueTypes valueType)
        {
            return !(type1 == valueType);
        }

        public static bool operator ==(BdoDataType type1, Type type)
        {
            return type1.ValueType == DataValueTypes.Any
                || (type1.ValueType == DataValueTypes.Object
                && type1.ClassType == type);
        }

        public static bool operator !=(BdoDataType type1, Type type)
        {
            return !(type1 == type);
        }

        public static bool operator <=(BdoDataType type1, BdoDataType type2)
        {
            return type1.ValueType == DataValueTypes.Any
                || (type1.ValueType != DataValueTypes.Entity && type1.ValueType != DataValueTypes.Object && type1.ValueType == type2.ValueType)
                || ((type1.ValueType == DataValueTypes.Entity || type1.ValueType == DataValueTypes.Object) &&
                    (!type2.IsScalar()
                    || (type1.ValueType == type2.ValueType && type1.ClassType.IsAssignableFrom(type2.ClassType))));
        }

        public static bool operator >=(BdoDataType type1, BdoDataType type2)
        {
            return !(type1 <= type2);
        }

        public static bool operator <=(BdoDataType type1, Type type2)
        {
            return type1 <= new BdoDataType() { ValueType = type2.GetValueType(), ClassType = type2 };
        }

        public static bool operator >=(BdoDataType type1, Type type2)
        {
            return !(type1 <= type2);
        }

        public bool IsCompatibleWith(BdoDataType dataType)
        {
            return this <= dataType;
        }

        public bool IsCompatibleWith(Type type)
        {
            return this <= type;
        }

        public override bool Equals(object obj)
        {
            if (obj is BdoDataType dataType)
            {
                return this == dataType;
            }
            else if (obj is DataValueTypes valueType)
            {
                return this == valueType;
            }
            else if (obj is Type type)
            {
                return this == type;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return (ValueType + "_" + ClassType?.ToString())?.GetHashCode() ?? 0;
        }

        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param key="number"></param>
        public BdoDataType WithValueType(DataValueTypes valueType)
        {
            ValueType = valueType;

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="number"></param>
        public BdoDataType WithClassType(Type type)
        {
            ClassType = type;

            return this;
        }
    }
}
