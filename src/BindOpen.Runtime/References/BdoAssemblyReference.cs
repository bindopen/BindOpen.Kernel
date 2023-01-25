using BindOpen.Data;
using BindOpen.Data.Items;

namespace BindOpen.Runtime.References
{
    /// <summary>
    /// This class represents the BindOpen library reference.
    /// </summary>
    public class BdoAssemblyReference : BdoItem, IBdoAssemblyReference
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The library name of this instance.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The library version of this instance.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// The file name of this instance.
        /// </summary>
        public string FileName { get; set; }

        #endregion

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
            Name = name;
            Version = version;
        }

        #endregion

        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        public string Key()
            => Name == StringHelper.__Star ? StringHelper.__Star : Name + "$" + Version;

        #endregion

        // --------------------------------------------------
        // MUTATORS
        // --------------------------------------------------

        #region Mutators

        /// <summary>
        /// Adds the specified file name.
        /// </summary>
        /// <param name="fileName">The file name to consider.</param>
        public IBdoAssemblyReference WithFileName(string fileName)
        {
            FileName = fileName;
            return this;
        }

        #endregion
    }
}
