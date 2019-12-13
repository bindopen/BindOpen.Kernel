using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Definition.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Extensions.Definition.Extensions
{
    /// <summary>
    /// This class represents the definition of a library.
    /// </summary>
    [XmlType("BdoExtensionDefinition", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "extension", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class BdoExtensionDefinitionDto : DescribedDataItem, IBdoExtensionDefinitionDto
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private string _assemblyName = null;
        private string _rootNamespace = null;
        private string _fileName = null;
        private string _groupName = null;
        private DictionaryDataItem _itemIndexFullNameDictionary = null;

        /// <summary>
        /// The names of the using assembly files of this instance.
        /// </summary>
        protected List<string> _usingAssemblyFileNames = null;

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Name of the group of this instance.
        /// </summary>
        [XmlElement("groupName")]
        public string GroupName
        {
            get => _groupName ?? "";
            set { _groupName = value; }
        }

        /// <summary>
        /// Name of the assembly where the library can be loaded.
        /// </summary>
        [XmlElement("assembly")]
        public string AssemblyName
        {
            get => _assemblyName ?? "";
            set { _assemblyName = value; }
        }

        /// <summary>
        /// Root name space of this intance.
        /// </summary>
        [XmlElement("rootNamespace")]
        public string RootNamespace
        {
            get => _rootNamespace ?? "";
            set { _rootNamespace = value; }
        }

        // Files -------------------------------------

        /// <summary>
        /// File name of this instance.
        /// </summary>
        [XmlElement("fileName")]
        public string FileName
        {
            get => _fileName ?? "";
            set { _fileName = value; }
        }

        /// <summary>
        /// Names of the using assembly files of this instance.
        /// </summary>
        [XmlArray("using")]
        [XmlArrayItem("add")]
        public List<string> UsingAssemblyFileNames
        {
            get => _usingAssemblyFileNames ?? (_usingAssemblyFileNames = new List<string>());
            set { _usingAssemblyFileNames = value; }
        }

        // Dictionary full names -------------------------------------

        /// <summary>
        /// Dictionary full names of this instance.
        /// </summary>
        [XmlElement("indexes")]
        public DictionaryDataItem ItemIndexFullNameDictionary
        {
            get => _itemIndexFullNameDictionary ?? (_itemIndexFullNameDictionary = new DictionaryDataItem());
            set
            {
                _itemIndexFullNameDictionary = value;
                if (_itemIndexFullNameDictionary != null)
                    _itemIndexFullNameDictionary.AvailableKeys = Enum.GetNames(typeof(BdoExtensionItemKind)).ToList();
            }
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of BdoExtensionDefinition class.
        /// </summary>
        public BdoExtensionDefinitionDto()
        {
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Gets the root namespace.
        /// </summary>
        /// <returns>Returns the root namspace.</returns>
        public string GetRootNamespace()
        {
            return !string.IsNullOrEmpty(_rootNamespace) ? _rootNamespace : _assemblyName.GetEndedString(".") + "extension";
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize()
        {
            if (_itemIndexFullNameDictionary != null)
            {
                foreach (DataKeyValue dataKeyValue in _itemIndexFullNameDictionary.Values)
                {
                    dataKeyValue.Content = _rootNamespace.GetEndedString(".").Concatenate(dataKeyValue.Content, ".");
                }
            }
        }

        #endregion

        // ------------------------------------------
        // IDISPOSABLE METHODS
        // ------------------------------------------

        #region IDisposable_Methods

        /// <summary>
        /// Disposes this instance. 
        /// </summary>
        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);

            if (isDisposing)
            {
                _itemIndexFullNameDictionary?.Dispose();
            }
        }

        #endregion
    }
}
