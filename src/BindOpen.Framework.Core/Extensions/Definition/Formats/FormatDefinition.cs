using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Items.Source;

namespace BindOpen.Framework.Core.Extensions.Definition.Formats
{

    /// <summary>
    /// This class represents the format definition.
    /// </summary>
    [Serializable()]
    [XmlType("FormatDefinition", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "format.definition", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class FormatDefinition : AppExtensionItemDefinition
    {
        // --------------------------------------------------
        // VARIABLES
        // --------------------------------------------------

        #region Variables

        private DataSourceKind _DataSourceKind = DataSourceKind.Memory;

        #endregion


        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// Item class of this instance.
        /// </summary>
        [XmlElement("itemClass")]
        public String ItemClass
        {
            get;
            set;
        }

        /// <summary>
        /// Viewer class of this instance.
        /// </summary>
        /// <remarks>Class using the following format: winForm=xxx.xxx.xxx;webForm=xxx.xxx.xxx</remarks>
        [XmlElement("viewerClass")]
        public String ViewerClass
        {
            get;
            set;
        }

        /// <summary>
        /// Data source kind of this instance.
        /// </summary>
        [XmlElement("dataSourceKind")]
        public DataSourceKind DataSourceKind
        {
            get { return this._DataSourceKind; }
            set { this._DataSourceKind = value; }
        }

        #endregion


        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the FormatDefinition class.
        /// </summary>
        public FormatDefinition()
        {
        }

        #endregion

    }

}
