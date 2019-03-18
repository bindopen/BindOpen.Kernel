using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Dictionary;
using BindOpen.Framework.Core.Extensions.Common;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Extensions.Libraries
{
    /// <summary>
    /// This class represents the definition of a library.
    /// </summary>
    [Serializable()]
    [XmlType("LibraryDefinition", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "library.definition", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class LibraryDefinition : DescribedDataItem
    {
        // ------------------------------------------
        // CONSTANTS
        // ------------------------------------------

        #region Constants

        private const string _DefaultResourceFileName = "Library.Definition";

        #endregion

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
        protected List<String> _usingAssemblyFileNames = null;

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
            get { return this._groupName ?? ""; }
            set { this._groupName = value; }
        }

        /// <summary>
        /// Name of the assembly where the library can be loaded.
        /// </summary>
        [XmlElement("assembly")]
        public string AssemblyName
        {
            get { return this._assemblyName ?? ""; }
            set { this._assemblyName = value; }
        }

        /// <summary>
        /// Root name space of this intance.
        /// </summary>
        [XmlElement("rootNamespace")]
        public string RootNamespace
        {
            get { return this._rootNamespace ?? ""; }
            set { this._rootNamespace = value; }
        }

        // Files -------------------------------------

        /// <summary>
        /// File name of this instance.
        /// </summary>
        [XmlElement("fileName")]
        public string FileName
        {
            get { return this._fileName ?? ""; }
            set { this._fileName = value; }
        }

        /// <summary>
        /// Names of the using assembly files of this instance.
        /// </summary>
        [XmlArray("using")]
        [XmlArrayItem("add")]
        public List<string> UsingAssemblyFileNames
        {
            get {
                return this._usingAssemblyFileNames ?? (_usingAssemblyFileNames = new List<string>());
            }
            set { this._usingAssemblyFileNames = value; }
        }

        // Dictionary full names -------------------------------------

        /// <summary>
        /// Dictionary full names of this instance.
        /// </summary>
        [XmlElement("indexes")]
        public DictionaryDataItem ItemIndexFullNameDictionary
        {
            get
            {
                return this._itemIndexFullNameDictionary ?? (_itemIndexFullNameDictionary = new DictionaryDataItem());
            }
            set
            {
                this._itemIndexFullNameDictionary = value;
                if (this._itemIndexFullNameDictionary != null)
                    this._itemIndexFullNameDictionary.AvailableKeys = Enum.GetNames(typeof(AppExtensionItemKind)).ToList();
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
        public LibraryDefinition()
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
        public static string GetClassNameWithoutAssembly(String className)
        {
            return className==null ? "" : (className.Contains(",") ?
                className.Substring(0, className.IndexOf(",")) : className);
        }

        /// <summary>
        /// Gets the root namespace.
        /// </summary>
        /// <returns>Returns the root namspace.</returns>
        public string GetRootNamespace()
        {
            return !string.IsNullOrEmpty(this._rootNamespace) ? this._rootNamespace :
                this._assemblyName.GetEndedString(".") + "extension";
        }

        /// <summary>
        /// Gets the default class name space of the specified item kind.
        /// </summary>
        /// <param name="extensionItemKind">The extension item kind to consider.</param>
        /// <returns>Returns the class of the specified dictionary.</returns>
        public string GetDefaultClassNameSpace(AppExtensionItemKind extensionItemKind)
        {
            string rootNamespace = this.GetRootNamespace();

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
                case AppExtensionItemKind.RoutineConfiguration:
                    return rootNamespace.GetEndedString(".") + "Routines";
                case AppExtensionItemKind.ScriptWord:
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
            string aClass = this.ItemIndexFullNameDictionary.GetContent(extensionItemKind.ToString());

            if (string.IsNullOrEmpty(aClass))
            {
                switch (extensionItemKind)
                {
                    case AppExtensionItemKind.Task:
                        aClass = this.GetDefaultClassNameSpace(extensionItemKind) + ".Tasks.index";
                        break;
                    case AppExtensionItemKind.Carrier:
                        aClass = this.GetDefaultClassNameSpace(extensionItemKind) + ".Carriers.index";
                        break;
                    case AppExtensionItemKind.Connector:
                        aClass = this.GetDefaultClassNameSpace(extensionItemKind) + ".Connectors.index";
                        break;
                    case AppExtensionItemKind.ContextExtension:
                        aClass = this.GetDefaultClassNameSpace(extensionItemKind) + ".context.index";
                        break;
                    case AppExtensionItemKind.Entity:
                        aClass = this.GetDefaultClassNameSpace(extensionItemKind) + ".Entities.index";
                        break;
                    case AppExtensionItemKind.Handler:
                        aClass = this.GetDefaultClassNameSpace(extensionItemKind) + ".Handlers.index";
                        break;
                    case AppExtensionItemKind.RoutineConfiguration:
                        aClass = this.GetDefaultClassNameSpace(extensionItemKind) + ".Routines.index";
                        break;
                    case AppExtensionItemKind.Metrics:
                        aClass = this.GetDefaultClassNameSpace(extensionItemKind) + ".Metrics.index";
                        break;
                    case AppExtensionItemKind.ScriptWord:
                        aClass = this.GetDefaultClassNameSpace(extensionItemKind) + ".Scriptwords.index";
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
            if (this._itemIndexFullNameDictionary != null)
            {
                foreach (DataKeyValue dataKeyValue in this._itemIndexFullNameDictionary.Values)
                {
                    dataKeyValue.Content = this._rootNamespace.GetEndedString(".").Concatenate(dataKeyValue.Content, ".");
                }
            }
        }

        #endregion

        // ------------------------------------------
        // SERIALIZATION
        // ------------------------------------------

        #region Serialization

        /// <summary>
        /// Loads a definition from the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly to consider.</param>
        /// <param name="resourceFullName">The full name of the resouce to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The created library.</returns>
        public static LibraryDefinition Load(Assembly assembly, string resourceFullName = null, Log log = null)
        {
            LibraryDefinition libraryDefinition = null;

            if (assembly != null)
            {
                if (resourceFullName==null)
                    resourceFullName = Array.Find(assembly.GetManifestResourceNames(), p => p.EndsWith(LibraryDefinition._DefaultResourceFileName, StringComparison.OrdinalIgnoreCase));
                //            {
                //    //String nameSpace = assembly.GetName().Name;
                //    //if (nameSpace.Contains('_'))
                //    //    nameSpace = nameSpace.Substring(0,nameSpace.LastIndexOf('_')).Replace('_','.').ToLower();

                //    //resourceFullName = nameSpace + "." + LibraryDefinition._DefaultResourceFileName;
                //}

                Stream stream = null;
                if (resourceFullName == null)
                {
                    log?.AddError("Could not find any library definition in assembly (default named '" + LibraryDefinition._DefaultResourceFileName.ToLower() + "')");
                }
                else
                {
                    try
                    {
                        stream = assembly.GetManifestResourceStream(resourceFullName);
                        if (stream == null)
                        {
                            log?.AddError("Could not find the library definition named '" + resourceFullName + "' in assembly");
                        }
                        else
                        {
                            XmlSerializer xmlSerializer = new XmlSerializer(typeof(LibraryDefinition));
                            libraryDefinition = (LibraryDefinition)xmlSerializer.Deserialize(stream);

                            libraryDefinition?.Initialize();
                        }
                    }
                    catch (Exception ex)
                    {
                        log?.AddException(ex);
                    }
                    finally
                    {
                        stream?.Close();
                    }
                }
            }

            return libraryDefinition;
        }

        #endregion
    }
}
