using BindOpen.Framework.Core.Data.Conditions;
using BindOpen.Framework.Core.Data.Items;
using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Data.Business.Cases
{
    /// <summary>
    /// This class represents a business case.
    /// </summary>
    [Serializable()]
    [XmlType("BusinessCase", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "businessCase", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class BusinessCase : DescribedDataItem
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Business condition of this instance.
        /// </summary>
        [XmlElement("businessCondition")]
        public Condition BusinessCondition
        {
            get;
            set;
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BusinessCase class.
        /// </summary>
        public BusinessCase() : base()
        {
        }

        #endregion
    }
}