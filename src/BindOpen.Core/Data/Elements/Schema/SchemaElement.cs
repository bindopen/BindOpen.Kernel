using BindOpen.Data.Common;
using BindOpen.Data.Entities;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BindOpen.Data.Elements.Schema
{
    /// <summary>
    /// This class represents a schema element.
    /// </summary>
    [XmlType("SchemaElement", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot("schema", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    [XmlInclude(typeof(SchemaZoneElement))]
    public class SchemaElement : DataElement
    {
        //------------------------------------------
        // VARIABLES
        //-----------------------------------------

        #region Variables

        private SchemaZoneElement _parentZone = null;
        private string _imageFileName = null;

        private DataEntity _entity = null;

        #endregion

        //------------------------------------------
        // PROPERTIES
        //-----------------------------------------

        #region Properties

        /// <summary>
        /// The parent zone of this instance.
        /// </summary>
        [XmlIgnore()]
        public SchemaZoneElement ParentZone
        {
            get { return _parentZone; }
            set
            {
                _parentZone = value;
            }
        }

        /// <summary>
        /// The image file name of this instance.
        /// </summary>
        [XmlElement("imageFileName")]
        public string ImageFileName
        {
            get { return _imageFileName; }
            set
            {
                _imageFileName = value;
            }
        }

        /// <summary>
        /// The business entity unique ID of this instance.
        /// </summary>
        [XmlElement("entityUniqueName")]
        public string EntityUniqueName
        {
            get;
            set;
        }

        /// <summary>
        /// The entity of this instance.
        /// </summary>
        [XmlIgnore()]
        public DataEntity Entity
        {
            get { return _entity; }
            set
            {
                _entity = value;
                EntityUniqueName = _entity?.Id;
            }
        }

        /// <summary>
        /// The specification of this instance.
        /// </summary>
        [XmlElement("specification")]
        public new SchemaElementSpec Specification
        {
            get { return base.Specification as SchemaElementSpec; }
            set { base.Specification = value; }
        }

        #endregion

        //------------------------------------------
        // CONSTRUCTORS
        //-----------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new schema element.
        /// </summary>
        public SchemaElement()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the SchemaElement class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public SchemaElement(
            String name = null,
            params object[] items)
            : base(name, "schemaElement_")
        {
            ValueType = DataValueTypes.Schema;
            Specification = new SchemaElementSpec();

            foreach (object item in items)
                Add(item);
        }

        #endregion

        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        /// <summary>
        /// Indicates whether this instance is a descendant of the specified parent schema element zone.
        /// </summary>
        /// <param name="parentZoneElement">The parent schema element zone to consider.</param>
        /// <returns>True if this instance is a descendant of the specified parent schema element.</returns>
        public bool IsDescendantOf(
            SchemaZoneElement parentZoneElement)
        {
            SchemaElement currentSchemaZoneElement = _parentZone;
            while (currentSchemaZoneElement != null)
            {
                if (currentSchemaZoneElement == parentZoneElement)
                    return true;
                currentSchemaZoneElement = currentSchemaZoneElement.ParentZone;
            }
            return false;
        }

        // Specification ---------------------

        /// <summary>
        /// Creates a new specification.
        /// </summary>
        /// <returns>Returns the new specifcation.</returns>
        public override IDataElementSpec NewSpecification()
        {
            return (Specification = new SchemaElementSpec());
        }

        // Schema elements

        // Deletion / Removing -------------------------------------------

        /// <summary>
        /// Deletes the specified schema element.
        /// </summary>
        /// <param name="aElement">The schema element to consider.</param>
        /// <returns>True if the operation has succeded ; false otherwise.</returns>
        public bool DeleteElement(SchemaElement aElement)
        {
            if (aElement == null) return true;

            // we delete the object
            if (aElement.ParentZone != null)
                return aElement.ParentZone.SubElements.Remove(aElement);
            else
                return false;
        }

        /// <summary>
        /// Deletes the specified schema elements.
        /// </summary>
        /// <param name="elements">The schema elements to consider.</param>
        public void DeleteElements(
            List<SchemaElement> elements)
        {
            if (elements == null)
                return;

            foreach (SchemaElement currentElement in elements)
                DeleteElement(currentElement);
        }

        // Duplication / Copy / Move -------------------------------------------

        /// <summary>
        /// MergeDbQuerys the specified schema element to the specified parent schema element.
        /// </summary>
        /// <param name="aElement">The schema element to consider.</param>
        /// <param name="aSchemaZoneElement">The parent schema element zone to consider.</param>
        /// <returns>The duplicated schema element.</returns>
        public SchemaElement MergeDbQueryElement(
            SchemaElement aElement,
            SchemaZoneElement aSchemaZoneElement = null)
        {
            if (aElement == null) return null;

            return aElement.Clone(aSchemaZoneElement) as SchemaElement;
        }

        /// <summary>
        /// MergeDbQuerys the specified schema element to the specified parents schema element.
        /// </summary>
        /// <param name="aElement">The schema element to consider.</param>
        /// <param name="parentElements">The parents schema element to consider.</param>
        /// <returns>The duplicated schema element.</returns>
        public List<SchemaElement> MergeDbQueryElement(
            SchemaElement aElement,
            List<SchemaZoneElement> parentElements)
        {
            List<SchemaElement> duplicatedElements = new List<SchemaElement>();

            if (aElement == null)
                return duplicatedElements;
            foreach (SchemaZoneElement parentElement in parentElements)
                duplicatedElements.Add(MergeDbQueryElement(aElement, parentElement));

            return duplicatedElements;
        }

        /// <summary>
        /// MergeDbQuerys the specified schema elements to the specified parent schema element.
        /// </summary>
        /// <param name="elements">The schema elements to consider.</param>
        /// <param name="parentZoneElement">The parent schema element zone object to consider.</param>
        public List<SchemaElement> MergeDbQueryElements(
            List<SchemaElement> elements,
            SchemaZoneElement parentZoneElement = null)
        {
            List<SchemaElement> duplicatedElements = new List<SchemaElement>();

            if (elements == null)
                return duplicatedElements;

            foreach (SchemaElement currentElement in elements)
                duplicatedElements.Add(MergeDbQueryElement(currentElement, parentZoneElement));

            return duplicatedElements;
        }

        /// <summary>
        /// Moves the specified schema element to the specified parent schema element.
        /// </summary>
        /// <param name="aElement">The schema element to consider.</param>
        /// <param name="parentZoneElement">The parent schema element zone to consider.</param>
        public bool MoveElement(
            SchemaElement aElement,
            SchemaZoneElement parentZoneElement)
        {
            if ((aElement == null) || (aElement.ParentZone == null) | (parentZoneElement == null))
                return true;
            if ((parentZoneElement == aElement) |
                ((aElement is SchemaZoneElement) && (parentZoneElement.IsDescendantOf((SchemaZoneElement)aElement))))
                return false;

            aElement.ParentZone.SubElements.Remove(aElement);
            aElement.ParentZone = parentZoneElement;
            if (!parentZoneElement.SubElements.Contains(parentZoneElement))
                parentZoneElement.SubElements.Add(aElement);
            return true;
        }

        /// <summary>
        /// Moves the specified schema elements to the specified parent schema element.
        /// </summary>
        /// <param name="elements">The schema elements to consider.</param>
        /// <param name="parentZoneElement">The parent schema element zone to consider.</param>
        public void MoveElements(
            List<SchemaElement> elements,
            SchemaZoneElement parentZoneElement)
        {
            if (elements == null)
                return;

            foreach (SchemaElement currentElement in elements)
                MoveElement(currentElement, parentZoneElement);
        }

        // Adding -------------------------------------------

        /// <summary>
        /// Add a new schema element zone to the specified parent schema element zone.
        /// </summary>
        /// <param name="parentZoneElement">The parent schema element zone to consider.</param>
        /// <returns>The created schema element zone.</returns>
        public SchemaZoneElement CreateSchemaZoneElement(
            SchemaZoneElement parentZoneElement)
        {
            if (parentZoneElement == null)
                return null;

            SchemaZoneElement schemaZoneElement = new SchemaZoneElement
            {
                ParentZone = parentZoneElement
            };
            parentZoneElement.AddSubElement(schemaZoneElement);
            return schemaZoneElement;
        }

        /// <summary>
        /// Add a new object to the specified parent schema element zone.
        /// </summary>
        /// <param name="parentZoneElement">The parent schema element zone to consider.</param>
        /// <returns>The created schema element.</returns>
        public SchemaElement CreateElement(
            SchemaZoneElement parentZoneElement)
        {
            if (parentZoneElement == null)
                return null;

            SchemaElement aElement = new SchemaElement();
            aElement.ParentZone = parentZoneElement;
            parentZoneElement.AddSubElement(aElement);
            return aElement;
        }

        /// <summary>
        /// Add a new schema element zone to the specified parent schema element zone.
        /// </summary>
        /// <param name="parentZoneElement">The parent schema element zone to consider.</param>
        /// <param name="aElement">The schema element to consider.</param>
        public void AddElement(
            SchemaZoneElement parentZoneElement,
            SchemaElement aElement)
        {
            if ((parentZoneElement == null) | (aElement == null))
                return;

            aElement.ParentZone = parentZoneElement;
            parentZoneElement.AddSubElement(aElement);
        }

        #endregion

        // -------------------------------------------------------------
        // PROTECTION
        // -------------------------------------------------------------

        #region Protection

        /// <summary>
        /// Apply the specified visibility to this instance.
        /// </summary>
        /// <param name="accessibilityLevel">The visibility to apply.</param>
        /// <param name="isRecursive">Indicates whether the protection is applied to sub schema elements.</param>
        public void ApplyVisibility(AccessibilityLevels accessibilityLevel, bool isRecursive = true)
        {
            if ((this is SchemaZoneElement) && (isRecursive))
                foreach (SchemaElement aElement in ((SchemaZoneElement)this).SubElements)
                    aElement.ApplyVisibility(accessibilityLevel, isRecursive);
        }

        #endregion

        // --------------------------------------------------
        // CHECK, UPDATE, REPAIR
        // --------------------------------------------------

        #region Check_Update_Repair


        #endregion

        // --------------------------------------------------
        // CLONING
        // --------------------------------------------------

        #region Cloning

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns a cloned instance.</returns>
        public override object Clone(params string[] areas)
        {
            return Clone(null, Array.Empty<string>());
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <param name="parent">The parent schema element zone to consider.</param>
        /// <returns>Returns a cloned instance.</returns>
        public virtual object Clone(SchemaZoneElement parent, params string[] areas)
        {
            if (parent == null)
            {
                parent = ParentZone;
            }

            SchemaZoneElement element = base.Clone(areas) as SchemaZoneElement;
            element.Entity = _entity;
            element.ParentZone = parent;
            if (parent != null)
            {
                parent.SubElements.Add(element);
            }

            return element;
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

            _parentZone?.Dispose();
            _entity?.Dispose();

            _isDisposed = true;

            base.Dispose(isDisposing);
        }

        #endregion
    }
}
