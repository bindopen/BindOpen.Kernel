using System.Xml.Serialization;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a catalog element that is an element whose elements are carriers.
    /// </summary>
    [XmlType("MetaObject", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
    [XmlRoot(ElementName = "object", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel", IsNullable = false)]
    public class MetaObjectDto : MetaNodeDto
    {
        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the MetaObjectDto class.
        /// </summary>
        public MetaObjectDto()
        {
        }

        #endregion
    }
}
