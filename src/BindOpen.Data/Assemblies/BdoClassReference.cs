using BindOpen.Data.Helpers;

namespace BindOpen.Data.Assemblies
{
    /// <summary>
    /// This structure represents an class reference.
    /// </summary>
    public class BdoClassReference : BdoAssemblyReference,
        IBdoClassReference
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
            string assemblyVersion,
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
            AssemblyName + "$" + AssemblyVersion + "$" + ClassName;

        public override int GetHashCode()
        {
            return Key()?.GetHashCode() ?? 0;
        }

        public override bool Equals(object obj)
        {
            if (obj is BdoClassReference reference)
            {
                return AssemblyName.Equals(reference.AssemblyName)
                    && AssemblyVersion.Equals(reference.AssemblyVersion)
                    && ClassName.Equals(reference.ClassName);
            }

            return false;
        }

        public static bool operator ==(BdoClassReference left, BdoClassReference right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(BdoClassReference left, BdoClassReference right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return Key();
        }

        #endregion
    }
}
