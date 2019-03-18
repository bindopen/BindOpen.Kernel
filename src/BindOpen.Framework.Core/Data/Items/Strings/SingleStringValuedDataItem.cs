using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Data.Items.Strings
{
    /// <summary>
    /// This class represents a single string valued data item.
    /// </summary>
    [Serializable()]
    [XmlType("SingleStringValuedDataItem", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot("item", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class SingleStringValuedDataItem : DataItem
    {

        // -------------------------------------------------------------
        // PROPERTIES
        // -------------------------------------------------------------

        #region Properties

        /// <summary>
        /// Value of this instance.
        /// </summary>      
        [XmlElement("value")]
        public String Value
        {
            get;
            set;
        }

        #endregion


        // -------------------------------------------------------------
        // CONSTRUCTORS
        // -------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the SingleStringValuedDataItem class.
        /// </summary>
        public SingleStringValuedDataItem()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the SingleStringValuedDataItem class.
        /// </summary>
        /// <param name="value">The value to consider.</param>
        public SingleStringValuedDataItem(string value)
        {
            this.Value = value;
        }

        #endregion

    }
}
