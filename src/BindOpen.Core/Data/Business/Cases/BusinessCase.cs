using BindOpen.Data.Conditions;
using BindOpen.Data.Items;
using System.Xml.Serialization;

namespace BindOpen.Data.Business
{
    /// <summary>
    /// This class represents a business case.
    /// </summary>
    [XmlType("BusinessCase", Namespace = "https://storage.bindopen.org/pgrkhpym/docs/code/xsd/bindopen")]
    [XmlRoot(ElementName = "businessCase", Namespace = "https://storage.bindopen.org/pgrkhpym/docs/code/xsd/bindopen", IsNullable = false)]
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