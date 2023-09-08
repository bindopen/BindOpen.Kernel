using BindOpen.Kernel.Data.Helpers;
using BindOpen.Kernel.Logging;
using BindOpen.Kernel.Scoping;
using System;

namespace BindOpen.Kernel.Data.Assemblies
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
        public string ClassName { get; set; }

        public override string Key()
            => AssemblyName == StringHelper.__Star ?
            StringHelper.__Star :
            (ClassName == null ? null : (ClassName + ", " + base.Key()));

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

        public virtual Type GetRuntimeType(IBdoScope scope = null, IBdoLog log = null)
        {
            var fullName = Key();
            var type = fullName == null ? null : Type.GetType(fullName);

            return type;
        }

        public static bool operator ==(BdoClassReference left, IBdoClassReference right)
        {
            return left?.Key() == right?.Key();
        }

        public static bool operator !=(BdoClassReference left, IBdoClassReference right)
        {
            return !(left == right);
        }

        public static bool operator >=(BdoClassReference left, IBdoClassReference right)
        {
            return
                left is not null && right != null
                && left.AssemblyName?.Equals(right.AssemblyName, StringComparison.OrdinalIgnoreCase) == true
                && left.ClassName?.Equals(right.ClassName, StringComparison.OrdinalIgnoreCase) == true
                && (
                    left.AssemblyVersion == null
                    || right.AssemblyVersion == null
                    || left.AssemblyVersion >= right.AssemblyVersion
                );
        }

        public static bool operator <=(BdoClassReference left, IBdoClassReference right)
        {
            return
                left is not null && right != null
                && left.AssemblyName?.Equals(right.AssemblyName, StringComparison.OrdinalIgnoreCase) == true
                && left.ClassName?.Equals(right.ClassName, StringComparison.OrdinalIgnoreCase) == true
                && (
                    left.AssemblyVersion == null
                    || right.AssemblyVersion == null
                    || left.AssemblyVersion <= right.AssemblyVersion
                );
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
