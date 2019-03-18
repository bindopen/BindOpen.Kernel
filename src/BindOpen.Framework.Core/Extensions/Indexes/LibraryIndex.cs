using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Libraries;

namespace BindOpen.Framework.Core.Extensions.Indexes
{
    /// <summary>
    /// This class represents a library index.
    /// </summary>
    [Serializable()]
    [XmlType("LibraryIndex", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "library.index", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class LibraryIndex : DescribedDataItem
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private List<LibraryDefinition> _definitions = new List<LibraryDefinition>();

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Definitions of this instance.
        /// </summary>
        [XmlArray("libraries")]
        [XmlArrayItem("add.definition")]
        public List<LibraryDefinition> Definitions
        {
            get
            {
                if (this._definitions == null) this._definitions = new List<LibraryDefinition>();
                return this._definitions;
            }
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of LibraryIndex class.
        /// </summary>
        public LibraryIndex()
        {
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Gets the names of the libraries of this instance.
        /// </summary>
        /// <returns>Returns the names of the libraries of this instance.</returns>
        public List<String> GetLibraryNames()
        {
            List<String> loadedLibraryNames = new List<String>();
            return this._definitions.Select(p=>p.Name).Distinct().ToList();
        }

        /// <summary>
        /// Gets the specified library definition with the specified name.
        /// </summary>
        /// <param name="name">The name of the library to consider.</param>
        /// <returns>Returns the library definition with the specified name.</returns>
        public LibraryDefinition GetDefinition(String name)
        {
            return name == null ? null :this._definitions.FirstOrDefault(p => p.KeyEquals(name));
        }

        /// <summary>
        /// Gets the specified library definitions.
        /// </summary>
        /// <param name="names">The names of the libraries to consider.</param>
        /// <returns>Returns the library definitions with the specified names.</returns>
        public List<LibraryDefinition> GetDefinitions(List<String> names=null)
        {
            return names == null ? null : this._definitions.Where(p => p.Name != null && names.Any(q => p.KeyEquals(q))).ToList();
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

            if ((appDomain == null) || (String.IsNullOrEmpty(assemblyName)) || (String.IsNullOrEmpty(resourceFullName))) return null;

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
