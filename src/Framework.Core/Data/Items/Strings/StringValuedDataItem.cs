using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Data.Items.Strings
{

    /// <summary>
    /// This class represents the string valued data item.
    /// </summary>
    [Serializable()]
    [XmlType("StringValuedDataItem", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "item", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class StringValuedDataItem : DataItem, INamedDataItem
    {
        // --------------------------------------------------
        // VARIABLES
        // --------------------------------------------------

        #region Variables

        /// <summary>
        /// Values of this instance.
        /// </summary>
        protected List<string> _values = new List<string>();

        #endregion

        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The name of this instance.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Values of this instance. This can be a single value or a list of values.
        /// </summary>
        /// <remarks>Items must be allowed and must not be forbidden. Otherwise, the items will be the default ones..</remarks>
        [XmlArray("values")]
        [XmlArrayItem("add.value")]
        public List<string> Values
        {
            get
            {
                return _values ?? (_values = new List<string>());
            }
            set { this._values = value; }
        }

        /// <summary>
        /// Specification of the Values property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean ValuesSpecified => _values?.Count > 0;

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the StringValuedDataItem class.
        /// </summary>
        public StringValuedDataItem()
        {
        }

        /// <summary>
        /// Initializes a new instance of the StringValuedDataItem class.
        /// </summary>
        /// <param name="name">Name of this instance.</param>
        public StringValuedDataItem(String name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the StringValuedDataItem class.
        /// </summary>
        /// <param name="name">Name of this instance.</param>
        /// <param name="value">Value of this instance.</param>
        public StringValuedDataItem(
            String name,
            String value) : this(name)
        {
            this.SetValue(value);
        }

        #endregion

        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        /// <summary>
        /// Indicates whether this instance has a value.
        /// </summary>
        /// <returns>Returns true if this instance contains a value.</returns>
        public Boolean HasValue()
        {
            return _values?.Count > 0;
        }

        /// <summary>
        /// Returns the values of this instance.
        /// </summary>
        /// <returns>Returns the single value of this instance.</returns>
        public String GetValue()
        {
            return _values?.Count > 0 ? this._values[0] : "";
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns a cloned instance.</returns>
        public new StringValuedDataItem Clone()
        {
            StringValuedDataItem element = new StringValuedDataItem();
            element.Values = new List<string>(this.Values);

            return element;
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Clears the values of this instance.
        /// </summary>
        public void ClearValues()
        {
            this._values = new List<string>();
        }

        /// <summary>
        /// Initializes the value of this instance.
        /// </summary>
        public void InitializeValue()
        {
            this.ClearValues();
            this.Values.Add("");
        }

        /// <summary>
        /// Sets the single value of this instance.
        /// </summary>
        /// <remarks>Values of this instance must be allowed and must not be forbidden. Otherwise, the values will be the default ones..</remarks>
        /// <param name="value">The String single value of this instance.</param>
        public virtual void SetValue(String value)
        {
            this._values = new List<string>
            {
                value
            };
        }

        /// <summary>
        /// Sets the list of values of this instance.
        /// </summary>
        /// <param name="values">The String values of this instance.</param>
        public virtual void SetValues(List<string> values)
        {
            this._values = new List<string>();
            foreach (String value in values)
                this._values.Add(value);
        }

        #endregion

        // --------------------------------------------------
        // EXPORTING
        // --------------------------------------------------

        #region Exporting

        /// <summary>
        /// Returns a text node representing this instance.
        /// </summary>
        /// <param name="nodeName">Name of the tex node.</param>
        /// <param name="indent">Tabulation indent to include in the text.</param>
        /// <returns></returns>
        public String GetTextNode(String nodeName, String indent)
        {
            String st = "";

            st += indent + nodeName + "\n";
            st += "\t" + indent + nodeName + ":name=\"" + this.Name + "\"\n";
            st += "\t" + indent + nodeName + ":values" + "\n";
            foreach (String value in this.Values)
                st += "\t\t" + indent + nodeName + ":values:value=\"" + value + "\"\n";
            return st;
        }

        #endregion
    }

}
