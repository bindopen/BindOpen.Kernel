using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Dictionary;
using BindOpen.Framework.Core.Extensions.Common;

namespace BindOpen.Framework.Core.Extensions.Definition.Libraries
{
    /// <summary>
    /// This class represents the definition of a library.
    /// </summary>
    [Serializable()]
    [XmlType("LibraryDefinition", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "library.definition", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class LibraryDefinitionDto : DescribedDataItem, ILibraryDefinitionDto
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
                    _itemIndexFullNameDictionary.AvailableKeys = Enum.GetNames(typeof(AppExtensionItemKind)).ToList();
            }
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of LibraryDefinition class.
        /// </summary>
        public LibraryDefinitionDto()
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
        /// <param name="className">The class name to consider.</param>
        /// <returns>Returns the root namspace.</returns>
        public static string GetClassNameWithoutAssembly(string className)
        {
            return className==null ? "" : (className.Contains(",") ? className.Substring(0, className.IndexOf(",")) : className);
        }

        /// <summary>
        /// Gets the root namespace.
        /// </summary>
        /// <returns>Returns the root namspace.</returns>
        public string GetRootNamespace()
        {
            return !string.IsNullOrEmpty(_rootNamespace) ? _rootNamespace : _assemblyName.GetEndedString(".") + "extension";
        }

        /// <summary>
        /// Gets the default class name space of the specified item kind.
        /// </summary>
        /// <param name="extensionItemKind">The extension item kind to consider.</param>
        /// <returns>Returns the class of the specified dictionary.</returns>
        public string GetDefaultClassNameSpace(AppExtensionItemKind extensionItemKind)
        {
            string rootNamespace = GetRootNamespace();

            switch (extensionItemKind)
            {
                case AppExtensionItemKind.Carrier:
                    return rootNamespace.GetEndedString(".") + "Carriers";
                case AppExtensionItemKind.Connector:
                    return rootNamespace.GetEndedString(".") + "Connectors";
                case AppExtensionItemKind.ContextExtension:
                    return rootNamespace.GetEndedString(".") + "Context";
                case AppExtensionItemKind.Entity:
                    return rootNamespace.GetEndedString(".") + "Entities";
                case AppExtensionItemKind.Format:
                    return rootNamespace.GetEndedString(".") + "Formats";
                case AppExtensionItemKind.Handler:
                    return rootNamespace.GetEndedString(".") + "Handlers";
                case AppExtensionItemKind.Metrics:
                    return rootNamespace.GetEndedString(".") + "Metrics";
                case AppExtensionItemKind.Routine:
                    return rootNamespace.GetEndedString(".") + "Routines";
                case AppExtensionItemKind.Scriptword:
                    return rootNamespace.GetEndedString(".") + "Scriptwords";
                case AppExtensionItemKind.Task:
                    return rootNamespace.GetEndedString(".") + "Tasks";
            }

            return rootNamespace;
        }

        /// <summary>
        /// Gets the full name of the specified dictionary resource.
        /// </summary>
        /// <param name="extensionItemKind">The extension item kind to consider.</param>
        /// <returns>Returns the class of the specified dictionary.</returns>
        public string GetItemIndexResourceFullName(AppExtensionItemKind extensionItemKind)
        {
            string aClass = ItemIndexFullNameDictionary.GetContent(extensionItemKind.ToString());

            if (string.IsNullOrEmpty(aClass))
            {
                switch (extensionItemKind)
                {
                    case AppExtensionItemKind.Task:
                        aClass = GetDefaultClassNameSpace(extensionItemKind) + ".Tasks.index";
                        break;
                    case AppExtensionItemKind.Carrier:
                        aClass = GetDefaultClassNameSpace(extensionItemKind) + ".Carriers.index";
                        break;
                    case AppExtensionItemKind.Connector:
                        aClass = GetDefaultClassNameSpace(extensionItemKind) + ".Connectors.index";
                        break;
                    case AppExtensionItemKind.ContextExtension:
                        aClass = GetDefaultClassNameSpace(extensionItemKind) + ".context.index";
                        break;
                    case AppExtensionItemKind.Entity:
                        aClass = GetDefaultClassNameSpace(extensionItemKind) + ".Entities.index";
                        break;
                    case AppExtensionItemKind.Handler:
                        aClass = GetDefaultClassNameSpace(extensionItemKind) + ".Handlers.index";
                        break;
                    case AppExtensionItemKind.Routine:
                        aClass = GetDefaultClassNameSpace(extensionItemKind) + ".Routines.index";
                        break;
                    case AppExtensionItemKind.Metrics:
                        aClass = GetDefaultClassNameSpace(extensionItemKind) + ".Metrics.index";
                        break;
                    case AppExtensionItemKind.Scriptword:
                        aClass = GetDefaultClassNameSpace(extensionItemKind) + ".Scriptwords.index";
                        break;
                    default:
                        break;
                }
            }

            return aClass;
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
    }
}
