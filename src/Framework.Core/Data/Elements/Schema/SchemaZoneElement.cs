using BindOpen.Framework.Core.Data.Helpers.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Data.Elements.Schema
{
    /// <summary>
    /// This class represents a schema zone element.
    /// </summary>
    [XmlType("SchemaZoneElement", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot("element", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class SchemaZoneElement : SchemaElement, INotifyPropertyChanged
    {
        //------------------------------------------
        // VARIABLES
        //-----------------------------------------

        #region Variables

        private ObservableCollection<SchemaElement> _SubElements = new ObservableCollection<SchemaElement>();

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
                return this._SubElements;
            }
            set
            {
                if (this._SubElements != value)
                {
                    this._SubElements = value;
                    this._SubSchemaElements = new List<SchemaElement>(this._SubElements);
                    this.RaizePropertyChanged("SubElements");
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
            this.Specification = new SchemaElementSpec();

            foreach (object item in items)
                this.AddItem(item);
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
            this._SubElements = new ObservableCollection<SchemaElement>();

            if (this._SubSchemaElements != null)
                foreach (SchemaElement aSchemaElement in this._SubSchemaElements)
                {
                    aSchemaElement.ParentZone = this;
                    this._SubElements.Add(aSchemaElement);

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
                this.SubElements.Add(aSchemaElement);
                if (this.SubElements != null)
                    this.SubElements = new ObservableCollection<SchemaElement>(
                        (this.SubElements.OrderBy(p => (p is SchemaZoneElement ? "A_" : "B_") + p.Name)));
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
                    if ((aSchemaElement = this.GetElementWithId(id, aSubSchemaElement)) != null)
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
        /// <returns>Returns a cloned instance.</returns>
        public override object Clone()
        {
            return this.Clone(null);
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <param name="parentZoneElement">The parent schema element group to consider.</param>
        /// <returns>Returns a cloned instance.</returns>
        public override object Clone(SchemaZoneElement parentZoneElement)
        {
            if (parentZoneElement == null)
                parentZoneElement = this.ParentZone;

            SchemaZoneElement aSchemaZoneElement = base.Clone(parentZoneElement) as SchemaZoneElement;

            foreach (SchemaElement aSubSchemaElement in this.SubElements)
                aSchemaZoneElement.AddSubElement(aSubSchemaElement.Clone(aSchemaZoneElement) as SchemaElement);

            return aSchemaZoneElement;
        }

        #endregion
    }
}
