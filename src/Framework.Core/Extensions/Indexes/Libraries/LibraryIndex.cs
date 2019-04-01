using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Definition.Libraries;

namespace BindOpen.Framework.Core.Extensions.Indexes.Libraries
{
    /// <summary>
    /// This class represents a library index.
    /// </summary>
    [Serializable()]
    [XmlType("LibraryIndex", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "library.index", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class LibraryIndex : DescribedDataItem, ILibraryIndex
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private List<ILibraryDefinition> _definitions = new List<ILibraryDefinition>();

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
        public List<ILibraryDefinition> Definitions
        {
            get
            {
                if (this._definitions == null) this._definitions = new List<ILibraryDefinition>();
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
        public List<string> GetLibraryNames()
        {
            List<string> loadedLibraryNames = new List<string>();
            return this._definitions.Select(p=>p.Name).Distinct().ToList();
        }

        /// <summary>
        /// Gets the specified library definition with the specified name.
        /// </summary>
        /// <param name="name">The name of the library to consider.</param>
        /// <returns>Returns the library definition with the specified name.</returns>
        public ILibraryDefinition GetDefinition(String name)
        {
            return name == null ? null :this._definitions.FirstOrDefault(p => p.KeyEquals(name));
        }

        /// <summary>
        /// Gets the specified library definitions.
        /// </summary>
        /// <param name="names">The names of the libraries to consider.</param>
        /// <returns>Returns the library definitions with the specified names.</returns>
        public List<ILibraryDefinition> GetDefinitions(List<string> names=null)
        {
            return names == null ? null : this._definitions.Where(p => p.Name != null && names.Any(q => p.KeyEquals(q))).ToList();
        }

        #endregion
    }
}
