using BindOpen.Framework.Data.Items;
using BindOpen.Framework.Data.References;
using BindOpen.Framework.Runtime.Application.Products;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace BindOpen.Framework.Runtime.Application.Limitations
{
    /// <summary>
    /// This class represents a product limitation.
    /// </summary>
    [XmlType("ProductLimitation", Namespace = "http://www.w3.org/2001/dkm.xsd")]
    [XmlRoot("productLimitation", Namespace = "http://www.w3.org/2001/dkm.xsd", IsNullable = false)]
    public class ProductLimitation : DataItem
    {

        // -------------------------------------------------------------
        // VARIABLES
        // -------------------------------------------------------------

        #region Variables

        private string _RegistrationMode = ProductRegistrationMode.Registered.ToString();
        private List<DataReference> _TargetElementReferences = new List<DataReference>();

        #endregion


        // -------------------------------------------------------------
        // PROPERTIES
        // -------------------------------------------------------------

        #region Properties

        /// <summary>
        /// The registration mode of this instance.
        /// </summary>
        [XmlElement("registrationMode")]
        public string RegistrationMode
        {
            get
            {
                return (this._RegistrationMode ?? "");
            }
            set
            {
                this._RegistrationMode = value;
            }
        }

        /// <summary>
        /// The business metrics unique ID of this instance.
        /// </summary>
        [XmlElement("businessMetricsUniqueName")]
        public string MetricsConfigurationUniqueName
        {
            get;
            set;
        }

        /// <summary>
        /// Displayed metrics title of this instance.
        /// </summary>
        /// <remarks>It is used if the business metrics cannot be found.</remarks>
        [XmlElement("displayedTitle")]
        public DictionaryDataItem DisplayedTitle
        {
            get;
            set;
        }

        /// <summary>
        /// Displayed metrics description of this instance.
        /// </summary>
        /// <remarks>It is used if the business metrics cannot be found.</remarks>
        [XmlElement("description")]
        public DictionaryDataItem Description
        {
            get;
            set;
        }

        /// <summary>
        /// The limit value of this instance.
        /// </summary>
        [XmlElement("limitValue")]
        public int LimitValue
        {
            get;
            set;
        }

        /// <summary>
        /// The warning threshold value of this instance.
        /// </summary>
        [XmlElement("warningThresholdValue")]
        public int WarningThresholdValue
        {
            get;
            set;
        }

        /// <summary>
        /// Indicates whether this instance is during evaluation only.
        /// </summary>
        [XmlElement("isOnlyDuringEvaluation")]
        public Boolean IsOnlyDuringEvaluation
        {
            get;
            set;
        }

        /// <summary>
        /// Indicates whether this instance is tracked.
        /// </summary>
        [XmlElement("isTracked")]
        public Boolean IsTracked
        {
            get;
            set;
        }

        /// <summary>
        /// Indicates whether this instance is critical.
        /// </summary>
        [XmlElement("isCritical")]
        public Boolean IsCritical
        {
            get;
            set;
        }

        /// <summary>
        /// The extra purchase URL of this instance.
        /// </summary>
        [XmlElement("extraPurchaseUrl")]
        public string ExtraPurchaseUrl
        {
            get;
            set;
        }

        /// <summary>
        /// The target element references of this instance.
        /// </summary>
        [XmlArray("targetElementReferences")]
        [XmlArrayItem("targetElementReference")]
        public List<DataReference> TargetElementReferences
        {
            get
            {
                return this._TargetElementReferences;
            }
            set
            {
                this._TargetElementReferences = value;
            }
        }

        #endregion


        // -------------------------------------------------------------
        // CONSTRUCTORS
        // -------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ProductLimitation class.
        /// </summary>
        public ProductLimitation()
        {
        }

        #endregion

    }
}
