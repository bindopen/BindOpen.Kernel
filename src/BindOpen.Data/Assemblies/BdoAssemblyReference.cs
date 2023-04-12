using BindOpen.Data.Helpers;
using System;

namespace BindOpen.Data.Assemblies
{
    /// <summary>
    /// This class represents the BindOpen library reference.
    /// </summary>
    public class BdoAssemblyReference :
        BdoObject, IBdoAssemblyReference
    {
        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoAssemblyReference class.
        /// </summary>
        public BdoAssemblyReference() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the BdoAssemblyReference class.
        /// </summary>
        /// <param key="name">The library name to consider.</param>
        /// <param key="version">The library version to consider.</param>
        public BdoAssemblyReference(
            string name,
            Version version = null) : this()
        {
            AssemblyName = name;
            AssemblyVersion = version;
        }

        #endregion

        // --------------------------------------------------
        // IBdoAssemblyReference Implementation
        // --------------------------------------------------

        #region IBdoAssemblyReference

        /// <summary>
        /// The library name of this instance.
        /// </summary>
        public string DefinitionUniqueName { get; set; }

        /// <summary>
        /// The library name of this instance.
        /// </summary>
        public string AssemblyName { get; private set; }

        /// <summary>
        /// The library version of this instance.
        /// </summary>
        public Version AssemblyVersion { get; private set; }

        /// <summary>
        /// The file name of this instance.
        /// </summary>
        public string AssemblyFileName { get; set; }

        public virtual string Key()
            => AssemblyName == StringHelper.__Star ?
            StringHelper.__Star :
            AssemblyName + ";" + AssemblyVersion;

        public override bool Equals(object obj)
        {
            if (obj is IBdoAssemblyReference reference)
            {
                return this == reference;
            }

            return false;
        }

        public bool IsCompatibleWith(IBdoAssemblyReference reference)
        {
            return this >= reference;
        }


        public override int GetHashCode()
        {
            return Key()?.GetHashCode() ?? 0;
        }

        public static bool operator ==(BdoAssemblyReference left, IBdoAssemblyReference right)
        {
            return left?.AssemblyName.Equals(right?.AssemblyName, StringComparison.OrdinalIgnoreCase) == true
                && left?.AssemblyVersion.Equals(right?.AssemblyVersion) == true;
        }

        public static bool operator !=(BdoAssemblyReference left, IBdoAssemblyReference right)
        {
            return !left.Equals(right);
        }

        public static bool operator >=(BdoAssemblyReference left, IBdoAssemblyReference right)
        {
            return left?.AssemblyName.Equals(right?.AssemblyName, StringComparison.OrdinalIgnoreCase) == true
                && (
                    left?.AssemblyVersion == null
                    || right?.AssemblyVersion == null
                    || left?.AssemblyVersion >= right?.AssemblyVersion
                );
        }

        public static bool operator <=(BdoAssemblyReference left, IBdoAssemblyReference right)
        {
            return left?.AssemblyName.Equals(right?.AssemblyName, StringComparison.OrdinalIgnoreCase) == true
                && (
                    left?.AssemblyVersion == null
                    || right?.AssemblyVersion == null
                    || left?.AssemblyVersion <= right?.AssemblyVersion
                );
        }

        public override string ToString()
        {
            return Key();
        }

        public virtual bool IsEmpty()
        {
            return string.IsNullOrEmpty(AssemblyName);
        }

        #endregion
    }
}
