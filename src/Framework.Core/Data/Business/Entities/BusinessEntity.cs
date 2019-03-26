using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items;

namespace BindOpen.Framework.Core.Data.Business.Entities
{
    /// <summary>
    /// This class represents the business entity.
    /// </summary>
    [Serializable()]
    [XmlType("BusinessEntity", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "entity", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class BusinessEntity : DescribedDataItem
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private DataElementSet _Detail = new DataElementSet();
        private String _ImageFileName = null;

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The detail of this instance.
        /// </summary>
        [XmlElement("detail")]
        public DataElementSet Detail
        {
            get { return this._Detail; }
            set { this._Detail = value; }
        }

        /// <summary>
        /// The image file name of this instance.
        /// </summary>
        [XmlElement("imageFileName")]
        public String ImageFileName
        {
            get { return this._ImageFileName; }
            set { this._ImageFileName = value; }
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BusinessEntity class.
        /// </summary>
        public BusinessEntity() : base()
        {
        }

        #endregion
    }

}
