using BindOpen.Data.Items;

namespace BindOpen.Data.Assemblies
{
    /// <summary>
    /// This class represents the BindOpen library reference.
    /// </summary>
    public class BdoAssemblyReference :
        BdoItem, IBdoAssemblyReference
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
        /// <param name="name">The library name to consider.</param>
        /// <param name="version">The library version to consider.</param>
        public BdoAssemblyReference(
            string name,
            string version = null) : this()
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
        public string DefinitionUniqueId { get; set; }

        /// <summary>
        /// The library name of this instance.
        /// </summary>
        public string AssemblyName { get; private set; }

        /// <summary>
        /// The library version of this instance.
        /// </summary>
        public string AssemblyVersion { get; private set; }

        /// <summary>
        /// The file name of this instance.
        /// </summary>
        public string AssemblyFileName { get; set; }

        public virtual string Key()
            => AssemblyName == StringHelper.__Star ?
            StringHelper.__Star :
            AssemblyName + "$" + AssemblyVersion;

        public override bool Equals(object obj)
        {
            if (obj is BdoAssemblyReference reference)
            {
                return AssemblyName.Equals(reference.AssemblyName)
                    && AssemblyVersion.Equals(reference.AssemblyVersion);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Key()?.GetHashCode() ?? 0;
        }

        public static bool operator ==(BdoAssemblyReference left, BdoAssemblyReference right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(BdoAssemblyReference left, BdoAssemblyReference right)
        {
            return !(left == right);
        }

        #endregion
    }
}
