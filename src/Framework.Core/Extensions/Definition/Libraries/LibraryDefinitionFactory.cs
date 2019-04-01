using System;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items.Dictionary;
using BindOpen.Framework.Core.Extensions.Common;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Extensions.Definition.Libraries
{


    private static LibraryDefinition GetLibraryDefinition(Assembly assembly, ILog log = null)
        {
            log = log ?? new Log();
            LibraryDefinition libraryDefinition = LibraryDefinition.Load(assembly, null, log);
            if (libraryDefinition == null)
                log.AddError("Error while attempting to load the library definition in assembly '" + assembly.GetName().Name + "'");

            return libraryDefinition;
        }

        /// <summary>
        /// Loads the script dictionary from the web service.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        private static Assembly Load_WebService(out Log log)
        {
            log = new Log();
            return null;
        }


        // ------------------------------------------
        // CLASSES
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Gets the root namespace.
        /// </summary>
        /// <param name="className">The class name to consider.</param>
        /// <returns>Returns the root namspace.</returns>
        private static string GetClassNameWithoutAssembly(String className)
        {
            return className==null ? "" : (className.Contains(",") ?
                className.Substring(0, className.IndexOf(",")) : className);
        }

        /// <summary>
        /// Gets the root namespace.
        /// </summary>
        /// <returns>Returns the root namspace.</returns>
        private string GetRootNamespace()
        {
            return !string.IsNullOrEmpty(this._rootNamespace) ? this._rootNamespace :
                this._assemblyName.GetEndedString(".") + "extension";
        }

        /// <summary>
        /// Gets the default class name space of the specified item kind.
        /// </summary>
        /// <param name="extensionItemKind">The extension item kind to consider.</param>
        /// <returns>Returns the class of the specified dictionary.</returns>
        private string GetDefaultClassNameSpace(AppExtensionItemKind extensionItemKind)
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
        private string GetItemIndexResourceFullName(AppExtensionItemKind extensionItemKind)
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
        public static LibraryDefinition Load(Assembly assembly, string resourceFullName = null, ILog log = null)
        {
            LibraryDefinition libraryDefinition = null;

            if (assembly != null)
            {
                if (resourceFullName==null)
                    resourceFullName = Array.Find(assembly.GetManifestResourceNames(), p => p.EndsWith(LibraryDefinition.__DefaultResourceFileName, StringComparison.OrdinalIgnoreCase));
                //            {
                //    //String nameSpace = assembly.GetName().Name;
                //    //if (nameSpace.Contains('_'))
                //    //    nameSpace = nameSpace.Substring(0,nameSpace.LastIndexOf('_')).Replace('_','.').ToLower();

                //    //resourceFullName = nameSpace + "." + LibraryDefinition._DefaultResourceFileName;
                //}

                Stream stream = null;
                if (resourceFullName == null)
                {
                    log?.AddError("Could not find any library definition in assembly (default named '" + LibraryDefinition.__DefaultResourceFileName.ToLower() + "')");
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

    // ------------------------------------------
    // SERIALIZATION
    // ------------------------------------------

    #region Serialization

    /// <summary>
    /// Gets the central library indexation.
    /// </summary>
    /// <param name="appDomain">The application domain to consider.</param>
    /// <param name="assemblyName">The name of the assembly to consider.</param>
    /// <param name="resourceFullName">The full name of the resource to consider.</param>
    /// <returns>The central indexation of this instance.</returns>
    public static LibraryIndex Load(AppDomain appDomain, String assemblyName, String resourceFullName)
    {
        LibraryIndex libraryIndex = null;

        if ((appDomain == null) || (string.IsNullOrEmpty(assemblyName)) || (string.IsNullOrEmpty(resourceFullName))) return null;

        Assembly assembly = appDomain.Load(assemblyName);
        if (assembly != null)
        {
            Stream stream = null;
            try
            {
                stream = assembly.GetManifestResourceStream(resourceFullName);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(LibraryIndex));
                libraryIndex = (LibraryIndex)xmlSerializer.Deserialize(stream);
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
        }

        return libraryIndex;
    }

    #endregion

}
}
