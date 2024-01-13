using BindOpen.Data.Assemblies;
using BindOpen.Data.Helpers;
using BindOpen.Logging;
using BindOpen.Scoping;
using System;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents a data element specification.
    /// </summary>
    public class BdoDataType : BdoClassReference, IBdoDataType
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
        /// The etension kind of this instance.
        /// </summary>
        public string DefinitionUniqueName { get; set; }

        public override string Key()
            => DefinitionUniqueName == null ?
            base.Key() :
            (
                ValueType.ToString()
                + ", Definition=" + DefinitionUniqueName
                + (AssemblyVersion == null ? null : ", Version=" + AssemblyVersion)
            );

        #endregion

        // --------------------------------------------------
        // Constructors
        // --------------------------------------------------

        #region Constructors

        public BdoDataType()
        {

        }

        public BdoDataType(IBdoClassReference reference)
        {
            AssemblyName = reference?.AssemblyName;
            AssemblyFileName = reference?.AssemblyFileName;
            AssemblyVersion = reference?.AssemblyVersion;
            ClassName = reference?.ClassName;
        }

        #endregion

        public override Type GetRuntimeType(IBdoScope scope = null, IBdoLog log = null)
        {
            if (!string.IsNullOrEmpty(DefinitionUniqueName))
            {
                var definition = scope?.ExtensionStore?.GetDefinition(
                    ValueType.GetExtensionKind(),
                    DefinitionUniqueName);

                if (definition is IBdoRuntimeTyped runtimeTyped)
                {
                    return runtimeTyped?.RuntimeType;
                }
            }
            else
            {
                var type = base.GetRuntimeType(scope);

                return type;
            }

            return null;
        }

        public override string ToString()
        {
            var baseKey = base.Key();

            return ValueType.ToString()
                .ConcatenateIf(!string.IsNullOrEmpty(baseKey), ", " + baseKey)
                .ConcatenateIf(!string.IsNullOrEmpty(DefinitionUniqueName), ", Definition=" + DefinitionUniqueName);
        }

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

        public static bool operator ==(BdoDataType left, BdoDataType right)
        {
            var leftValueType = left?.ValueType ?? DataValueTypes.Any;
            var rightValueType = right?.ValueType ?? DataValueTypes.Any;

            return
                left is not null && right is not null
                && (
                    leftValueType == DataValueTypes.Any
                    || rightValueType == DataValueTypes.Any
                    || leftValueType == DataValueTypes.Entity
                    || leftValueType == DataValueTypes.Object
                    || leftValueType == right.ValueType
                )
                && (
                    left?.DefinitionUniqueName != null
                    && left.DefinitionUniqueName?.BdoKeyEquals(right.DefinitionUniqueName) == true
                    && left.AssemblyVersion == right.AssemblyVersion
                )
                && (left?.DefinitionUniqueName == null && (BdoClassReference)left == (BdoClassReference)right);
        }

        public static bool operator !=(BdoDataType left, BdoDataType right)
        {
            return !(left == right);
        }

        public static bool operator ==(BdoDataType left, DataValueTypes valueType)
        {
            return left.ValueType == DataValueTypes.Any
                || left.ValueType == valueType;
        }

        public static bool operator !=(BdoDataType left, DataValueTypes valueType)
        {
            return !(left == valueType);
        }

        public static bool operator ==(BdoDataType left, Type type)
        {
            return left == BdoData.NewDataType(type);
        }

        public static bool operator !=(BdoDataType left, Type type)
        {
            return !(left == type);
        }

        public static bool operator >=(BdoDataType left, BdoDataType right)
        {
            return left.IsCompatibleWithType(right);
        }

        public static bool operator <=(BdoDataType left, BdoDataType right)
        {
            return right.IsCompatibleWithType(left);
        }

        public static bool operator <=(BdoDataType left, Type right)
        {
            return left <= BdoData.NewDataType(right);
        }

        public static bool operator >=(BdoDataType left, Type right)
        {
            return left >= BdoData.NewDataType(right);
        }

        public bool IsCompatibleWithType(IBdoDataType dataType)
        {
            var leftValueType = ValueType;
            var rightValueType = dataType?.ValueType ?? DataValueTypes.Any;

            var b = leftValueType == DataValueTypes.Any
                    || rightValueType == DataValueTypes.Any
                    || leftValueType == DataValueTypes.Entity
                    || leftValueType == DataValueTypes.Object
                    || rightValueType == DataValueTypes.Entity
                    || rightValueType == DataValueTypes.Object
                    || leftValueType == rightValueType;

            if (b && leftValueType != DataValueTypes.Any)
            {
                if (leftValueType.IsScalar())
                {
                    return rightValueType == DataValueTypes.Any
                        || leftValueType == rightValueType;
                }

                var rightRuntimeType = dataType?.GetRuntimeType();
                var leftRuntimeType = GetRuntimeType();

                b = rightValueType == DataValueTypes.Any
                    || rightRuntimeType?.IsAssignableFrom(leftRuntimeType) == true;
            }

            return b;
        }

        public bool IsCompatibleWithData(object data)
        {
            if (ValueType == DataValueTypes.Null)
            {
                return data == null;
            }

            return data == null || IsCompatibleWithType(BdoData.NewDataType(data.GetType()));
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
            return (ValueType + "_" + ToString())?.GetHashCode() ?? 0;
        }

        #endregion
    }
}
