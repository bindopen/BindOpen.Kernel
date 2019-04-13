using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Conditions;
using BindOpen.Framework.Core.Data.Items;

namespace BindOpen.Framework.Core.Data.Business.Cases
{
    /// <summary>
    /// This class represents a business case.
    /// </summary>
    [Serializable()]
    [XmlType("BusinessCase", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "businessCase", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class BusinessCase :DescribedDataItem
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private Condition _BusinessCondition = null;

        #endregion

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
            get { return this._BusinessCondition; }
            set { this._BusinessCondition = value; }
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