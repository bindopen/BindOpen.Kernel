using BindOpen.Data;
using BindOpen.Data.Items;
using System.Collections.Generic;

namespace BindOpen.Runtime.Definition
{
    /// <summary>
    /// This class represents the definition of a library.
    /// </summary>
    public class BdoExtensionDefinition : BdoItem, IBdoExtensionDefinition
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of BdoExtensionDefinition class.
        /// </summary>
        public BdoExtensionDefinition()
        {
        }

        #endregion

        // ------------------------------------------
        // IBdoExtensionDefinition Implementation
        // ------------------------------------------

        #region IBdoExtensionDefinition

        /// <summary>
        /// The unique ID of this instance.
        /// </summary> 
        public string UniqueId { get => Name; }

        /// <summary>
        /// Name of the group of this instance.
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Name of the assembly where the library can be loaded.
        /// </summary>
        public string AssemblyName { get; set; }

        /// <summary>
        /// Root name space of this intance.
        /// </summary>
        public string RootNamespace { get; set; }

        // Files -------------------------------------

        /// <summary>
        /// File name of this instance.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Names of the using assembly files of this instance.
        /// </summary>
        public List<string> UsingAssemblyFileNames { get; set; }

        // Dictionary full names -------------------------------------

        /// <summary>
        /// Dictionary full names of this instance.
        /// </summary>
        public IBdoDictionary ItemIndexFullNameDictionary { get; set; }

        /// <summary>
        /// Gets the root namespace.
        /// </summary>
        /// <returns>Returns the root namspace.</returns>
        public string GetRootNamespace()
        {
            return !string.IsNullOrEmpty(RootNamespace) ? RootNamespace : AssemblyName.EndingWith(".") + "extension";
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize()
        {
            if (ItemIndexFullNameDictionary != null)
            {
                foreach (var pair in ItemIndexFullNameDictionary)
                {
                    ItemIndexFullNameDictionary[pair.Key] =
                        RootNamespace.EndingWith(".").Concatenate(pair.Value, ".");
                }
            }
        }

        #endregion

        // ------------------------------------------
        // IDISPOSABLE METHODS
        // ------------------------------------------

        #region IDisposable_Methods

        private bool _isDisposed = false;

        /// <summary>
        /// Disposes this instance. 
        /// </summary>
        /// <param name="isDisposing">Indicates whether this instance is disposing</param>
        protected override void Dispose(bool isDisposing)
        {
            if (_isDisposed)
            {
                return;
            }

            ItemIndexFullNameDictionary?.Dispose();

            _isDisposed = true;

            base.Dispose(isDisposing);
        }

        #endregion

        // ------------------------------------------
        // IReferenced Implementation
        // ------------------------------------------

        #region IReferenced

        /// <summary>
        /// 
        /// </summary>
        public string Key() => Name;

        #endregion

        // ------------------------------------------
        // IIdentified Implementation
        // ------------------------------------------

        #region IIdentified

        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        #endregion

        // ------------------------------------------
        // INamed Implementation
        // ------------------------------------------

        #region INamed

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        #endregion

        // ------------------------------------------
        // IGloballyTitled Implementation
        // ------------------------------------------

        #region IGloballyTitled

        /// <summary>
        /// 
        /// </summary>
        public IBdoDictionary Title { get; set; }

        #endregion

        // ------------------------------------------
        // IGloballyDescribed Implementation
        // ------------------------------------------

        #region IGloballyDescribed

        /// <summary>
        /// 
        /// </summary>
        public IBdoDictionary Description { get; set; }

        #endregion
    }
}
