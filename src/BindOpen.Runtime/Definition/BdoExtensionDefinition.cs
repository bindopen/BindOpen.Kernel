using BindOpen.Meta;
using BindOpen.Meta.Items;
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
        // IIdentifiedPoco Implementation
        // ------------------------------------------

        #region IIdentifiedPoco

        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IBdoExtensionDefinition WithId(string id)
        {
            Id = id;
            return this;
        }

        #endregion

        // ------------------------------------------
        // INamedPoco Implementation
        // ------------------------------------------

        #region INamedPoco

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IBdoExtensionDefinition WithName(string name)
        {
            Name = BdoMeta.NewName(name, "spec_");
            return this;
        }

        #endregion

        // ------------------------------------------
        // IGloballyTitled Implementation
        // ------------------------------------------

        #region IGloballyTitled

        /// <summary>
        /// 
        /// </summary>
        public IBdoDictionary Title { get; set; }

        public IBdoExtensionDefinition AddTitle(KeyValuePair<string, string> item)
        {
            Title ??= BdoMeta.NewDictionary();
            Title.Add(item);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dico"></param>
        /// <returns></returns>
        public IBdoExtensionDefinition WithTitle(IBdoDictionary dico)
        {
            Title = dico;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultKey"></param>
        /// <returns></returns>
        public string GetTitleText(string key = "*", string defaultKey = "*")
        {
            return Title?[key, defaultKey];
        }

        #endregion

        // ------------------------------------------
        // IGloballyDescribed Implementation
        // ------------------------------------------

        #region IGloballyDescribed

        /// <summary>
        /// 
        /// </summary>
        public IBdoDictionary Description { get; set; }

        public IBdoExtensionDefinition AddDescription(KeyValuePair<string, string> item)
        {
            Description ??= BdoMeta.NewDictionary();
            Description.Add(item);
            return this;
        }

        public IBdoExtensionDefinition WithDescription(IBdoDictionary dico)
        {
            Description = dico;
            return this;
        }

        public string GetDescriptionText(string key = "*", string defaultKey = "*")
        {
            return Description?[key, defaultKey];
        }

        #endregion
    }
}
