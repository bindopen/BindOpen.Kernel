using System;
using System.ComponentModel;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Data.Elements.Scalar
{

    /// <summary>
    /// This class represents a scalar element that is an element whose items are scalars.
    /// </summary>

    [Serializable()]
    [XmlType("ScalarElement", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "scalar", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class ScalarElement : DataElement
    {

        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The value of this instance.
        /// </summary>
        [XmlAttribute("value")]
        public String Value
        {
            get
            {
                if (this.ItemXElement != null){
                    XElement element = this.ItemXElement.Elements().FirstOrDefault();
                    if (element != null)
                        return element.Value;
                }

                return null;
            }
            set
            {
                this.SetItem(value);
            }
        }

        /// <summary>
        /// Specification of the Value property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean ValueSpecified
        {
            get
            {
                return base.Items != null && base.Items.Count == 1;
            }
        }

        /// <summary>
        /// The value type of this instance.
        /// </summary>
        [XmlAttribute("valueType")]
        [DefaultValue(DataValueType.Text)]
        public new DataValueType ValueType
        {
            get { return base.ValueType; }
            set { base.ValueType = value; }
        }

        /// <summary>
        /// Specification of the ValueType property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean ValueTypeSpecified
        {
            get
            {
                return base.ValueType != DataValueType.Text;
            }
        }

        /// <summary>
        /// The specification of this instance.
        /// </summary>
        [XmlElement("specification")]
        public new ScalarElementSpecification Specification
        {
            get { return base.Specification as ScalarElementSpecification; }
            set { base.Specification = value; }
        }

        #endregion


        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new scalar element.
        /// </summary>
        public ScalarElement() : base()
        {
        }

        /// <summary>
        /// Initializes a new scalar element.
        /// </summary>
        /// <param name="dataValueType">The value type to consider.</param>
        public ScalarElement(DataValueType dataValueType)
            : this(null, dataValueType)
        {
        }

        /// <summary>
        /// Initializes a new scalar element.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        public ScalarElement(String name)
            : this(name, DataValueType.Any)
        {
        }

        /// <summary>
        /// Initializes a new scalar element.
        /// </summary>
        /// <param name="valueType">The value type to consider.</param>
        /// <param name="items">The items to consider.</param>
        public ScalarElement(
            DataValueType valueType,
            params Object[] items)
            : this(null, null, valueType, null, items)
        {
        }

        /// <summary>
        /// Initializes a new scalar element.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        /// <param name="items">The items to consider.</param>
        public ScalarElement(
            String name,
            DataValueType valueType,
            params Object[] items)
            : this(name, null, valueType, null, items)
        {
        }

        /// <summary>
        /// Initializes a new scalar element.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="id">The ID to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        /// <param name="specification">The specification to consider.</param>
        /// <param name="items">The items to consider.</param>
        public ScalarElement(
            String name,
            String id,
            DataValueType valueType,
            ScalarElementSpecification specification,
            params Object[] items)
            : base(name, "scalarElement_", id)
        {
            this.ValueType = valueType;
            this.Specification = specification;

            foreach (Object aItem in items)
                this.AddItem(aItem);

            //if (this.Specification != null)
            //    this.Specification.MaximumItemNumber = (items.Length == 1 ? 1 : -1);
        }

        #endregion


        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        // Specification ---------------------

        /// <summary>
        /// Gets a new specification.
        /// </summary>
        /// <returns>Returns the new specifcation.</returns>
        public override DataElementSpecification CreateSpecification()
        {
            return new ScalarElementSpecification();
        }

        #endregion


        // --------------------------------------------------
        // ITEMS
        // --------------------------------------------------

        #region Items

        /// <summary>
        /// Gets a new item of this instance.
        /// </summary>
          /// <param name="appScope">The application scope to consider.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>Returns a new object of this instance.</returns>
        public override Object NewItem(IAppScope appScope = null, Log log = null)
        {
            switch (this.ValueType)
            {
                case DataValueType.Boolean:
                    return false;
                case DataValueType.Date:
                    return DateTime.Now;
                case DataValueType.Integer:
                case DataValueType.Number:
                case DataValueType.Long:
                    return -1;
                case DataValueType.Text:
                    return "";
                case DataValueType.Time:
                    return new TimeSpan();
            };

            return null;
        }

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="indexItem">The index item to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>Returns the specified item of this instance.</returns>
        public override Object GetItem(
            Object indexItem = null,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null,
            Log log = null)
        {
            if ((indexItem == null) || (indexItem is int))
                return base.GetItem(indexItem, appScope, scriptVariableSet, log);
            else if (indexItem is String)
            {
                Object item = this.GetObjectFromString(indexItem as String);
                return this.Items.First(p => p.KeyEquals(item));
            }

            return null;
        }

        /// <summary>
        /// Indicates whether this instance contains the specified scalar item or the specified entity name.
        /// </summary>
        /// <param name="indexItem">The item to consider.</param>
        /// <param name="isCaseSensitive">Indicates whether the verification is case sensitive.</param>
        /// <returns>Returns true if this instance contains the specified scalar item or the specified entity name.</returns>
        public override Boolean HasItem(Object indexItem, Boolean isCaseSensitive = false)
        {
            //String aStringItem = this.GetStringFromObject(indexItem);
            return this.Items.Any(p => p == indexItem);
        }

        /// <summary>
        /// Returns a text node representing this instance.
        /// </summary>
        /// <returns></returns>
        public override String ToString()
        {
            return String.Join("|", this.Items.Select(p => p == null ? "" : p.ToString()).ToArray());
        }

        // Conversion ---------------------------

        /// <summary>
        /// Returns the string value from an object based on this instance's specification.
        /// </summary>
        /// <param name="object1">The object value to convert.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>The result string.</returns>
        public override String GetStringFromObject(
            Object object1,
            Log log = null)
        {
            String stringValue = "";

            if (object1 != null)
                stringValue = object1.GetString(this.ValueType);

            return stringValue;
        }

        /// <summary>
        /// Returns the object value from a based on this instance's specification.
        /// </summary>
        /// <param name="stringValue">The string value to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>The result object.</returns>
        public override Object GetObjectFromString(
            String stringValue,
            IAppScope appScope = null,
            Log log = null)
        {
            Object object1 = null;

            String format = null;
            if (this.Specification != null && this.Specification.ConstraintStatement != null)
                format = this.Specification.ConstraintStatement.GetConstraintParameterValue("Format") as String;
            if (stringValue != null)
                object1 = stringValue.ToObject(this.ValueType, format);

            return object1;
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
        public override Object Clone()
        {
            ScalarElement scalarElement = this.MemberwiseClone() as ScalarElement;
            return scalarElement;
        }

        #endregion

    }

}
