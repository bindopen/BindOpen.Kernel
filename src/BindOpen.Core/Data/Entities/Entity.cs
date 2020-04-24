using BindOpen.Data.Elements;
using BindOpen.Data.Items;
using System.Xml.Serialization;

namespace BindOpen.Data.Entities
{
    /// <summary>
    /// This class represents the data entity.
    /// </summary>
    [XmlType("DataEntity", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "entity", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class DataEntity : DescribedDataItem
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The detail of this instance.
        /// </summary>
        [XmlElement("detail")]
        public DataElementSet Detail { get; set; } = new DataElementSet();

        /// <summary>
        /// The image file name of this instance.
        /// </summary>
        [XmlElement("imageFileName")]
        public string ImageFileName { get; set; } = null;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BusinessEntity class.
        /// </summary>
        public DataEntity() : base()
        {
        }

        #endregion
    }
}
