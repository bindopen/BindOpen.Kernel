using BindOpen.System.Data.Helpers;
using System;

namespace BindOpen.System.Data.Assemblies
{
    /// <summary>
    /// This structure represents an class reference.
    /// </summary>
    public class BdoClassReference : BdoAssemblyReference, IBdoClassReference
    {
        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        public BdoClassReference()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="assemblyName"></param>
        /// <param key="assemblyVersion"></param>
        /// <param key="className"></param>
        public BdoClassReference(
            string assemblyName,
            Version assemblyVersion,
            string className)
            : base(assemblyName, assemblyVersion)
        {
            ClassName = className;
        }

        #endregion

        // --------------------------------------------------
        // IBdoAssemblyReference Implementation
        // --------------------------------------------------

        #region IBdoAssemblyReference

        /// <summary>
        /// The class name of this instance.
        /// </summary>
        public string ClassName { get; private set; }

        public override string Key()
            => AssemblyName == StringHelper.__Star ?
            StringHelper.__Star :
            AssemblyName + ";" + AssemblyVersion + ";" + ClassName;

        public override int GetHashCode()
        {
            return Key()?.GetHashCode() ?? 0;
        }

        public override bool Equals(object obj)
        {
            if (obj is IBdoClassReference reference)
            {
                return this == reference;
            }

            return false;
        }

        public bool IsCompatibleWith(IBdoClassReference reference)
        {
            return this >= reference;
        }

        public static bool operator ==(BdoClassReference left, IBdoClassReference right)
        {
            return (BdoAssemblyReference)left == (IBdoAssemblyReference)right
                && left?.ClassName.Equals(right?.ClassName, StringComparison.OrdinalIgnoreCase) == true;
        }

        public static bool operator !=(BdoClassReference left, IBdoClassReference right)
        {
            return !(left == right);
        }

        public static bool operator >=(BdoClassReference left, IBdoClassReference right)
        {
            return (BdoAssemblyReference)left >= (IBdoAssemblyReference)right
                && left?.ClassName.Equals(right?.ClassName) == true;
        }

        public static bool operator <=(BdoClassReference left, IBdoClassReference right)
        {
            return (BdoAssemblyReference)left <= (IBdoAssemblyReference)right
                && left?.ClassName.Equals(right?.ClassName) == true;
        }

        public static bool operator ==(BdoClassReference left, BdoDataType right)
        {
            return right.ValueType == DataValueTypes.Any
                || (right.ValueType == DataValueTypes.Object
                && left == right.ClassType);
        }

        public static bool operator !=(BdoClassReference left, BdoDataType right)
        {
            return !(left == right);
        }

        public static bool operator ==(BdoClassReference left, Type right)
        {
            return left == BdoData.Class(right);
        }

        public static bool operator !=(BdoClassReference left, Type right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return Key();
        }

        public override bool IsEmpty()
        {
            return base.IsEmpty()
                || string.IsNullOrEmpty(ClassName);
        }

        #endregion
    }
}
