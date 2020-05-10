using BindOpen.Data.Helpers.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Serialization;

namespace BindOpen.Data.Elements.Schema
{
    /// <summary>
    /// This class represents a schema zone element.
    /// </summary>
    [XmlType("SchemaZoneElement", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot("element", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class SchemaZoneElement : SchemaElement
    {
        //------------------------------------------
        // VARIABLES
        //-----------------------------------------

        #region Variables

        private ObservableCollection<SchemaElement> _subElements = new ObservableCollection<SchemaElement>();

        #endregion

        //------------------------------------------
        // PROPERTIES
        //-----------------------------------------

        #region Properties

        /// <summary>
        /// The serialized schema elements of this instance.
        /// </summary>
        [XmlArray("subElements")]
        [XmlArrayItem("subElement")]
        public List<SchemaElement> _SubSchemaElements
        {
            get;
            set;
        }

        /// <summary>
        /// The sub elements of this instance.
        /// </summary>
        [XmlIgnore()]
        public ObservableCollection<SchemaElement> SubElements
        {
            get
            {
                return _subElements;
            }
            set
            {
                if (_subElements != value)
                {
                    _subElements = value;
                    _SubSchemaElements = new List<SchemaElement>(_subElements);
                }
            }
        }

        #endregion

        //------------------------------------------
        // CONSTRUCTORS
        //-----------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the SchemaZoneElement class.
        /// </summary>
        public SchemaZoneElement()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the SchemaZoneElement class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public SchemaZoneElement(
            String name,
            params object[] items)
            : base(name, "schemaZoneElement_")
        {
            Specification = new SchemaElementSpec();

            foreach (object item in items)
                Add(item);
        }

        #endregion

        //------------------------------------------
        // MUTATORS
        //-----------------------------------------

        #region Mutators

        /// <summary>
        /// Builds the tree of this instance.
        /// </summary>
        public virtual void BuildTree()
        {
            _subElements = new ObservableCollection<SchemaElement>();

            if (_SubSchemaElements != null)
                foreach (SchemaElement aSchemaElement in _SubSchemaElements)
                {
                    aSchemaElement.ParentZone = this;
                    _subElements.Add(aSchemaElement);

                    if (aSchemaElement is SchemaZoneElement)
                        ((SchemaZoneElement)aSchemaElement).BuildTree();
                }
        }

        /// <summary>
        /// Adds the specified sub schema element.
        /// </summary>
        /// <returns>Returns the added sub schema element.</returns>
        public void AddSubElement(SchemaElement aSchemaElement)
        {
            if (aSchemaElement != null)
            {
                SubElements.Add(aSchemaElement);
                if (SubElements != null)
                    SubElements = new ObservableCollection<SchemaElement>(
                        (SubElements.OrderBy(p => (p is SchemaZoneElement ? "A_" : "B_") + p.Name)));
            }
        }

        #endregion

        //------------------------------------------
        // ACCESSORS
        //-----------------------------------------

        #region Accessors

        /// <summary>
        /// Gets the schema element with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the schema element to consider.</param>
        /// <param name="parentSchemaElement">The parent schema element to consider.</param>
        /// <returns>The bschema element with the specified ID.</returns>
        public SchemaElement GetElementWithId(String id, SchemaElement parentSchemaElement = null)
        {
            if (id == null) return null;

            if (parentSchemaElement == null)
                parentSchemaElement = this;

            SchemaElement aSchemaElement = null;

            if (parentSchemaElement.Id.KeyEquals(id))
                return parentSchemaElement;

            if (parentSchemaElement is SchemaZoneElement)
                foreach (SchemaElement aSubSchemaElement in ((SchemaZoneElement)parentSchemaElement).SubElements)
                    if ((aSchemaElement = GetElementWithId(id, aSubSchemaElement)) != null)
                        break;

            return aSchemaElement;
        }

        #endregion

        // --------------------------------------------------
        // CLONING
        // --------------------------------------------------

        #region Cloning

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <param name="parent">The parent schema element group to consider.</param>
        /// <returns>Returns a cloned instance.</returns>
        public override object Clone(SchemaZoneElement parent, params string[] areas)
        {
            if (parent == null)
            {
                parent = ParentZone;
            }

            SchemaZoneElement element = base.Clone(parent, areas) as SchemaZoneElement;

            foreach (SchemaElement subElement in SubElements)
            {
                element.AddSubElement(subElement.Clone(element) as SchemaElement);
            }

            return element;
        }

        #endregion
    }
}
