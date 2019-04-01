using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Items.Source;

namespace BindOpen.Framework.Core.Extensions.Indexes.Connectors
{
    /// <summary>
    /// This class represents an attribute of connector index.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ConnectorIndexAttribute : Attribute
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The filters of this instance.
        /// </summary>
        [XmlArray("filters")]
        [XmlArrayItem("add")]
        public List<IAppExtensionFilter> Filters { get; set; } = new List<IAppExtensionFilter>();

        /// <summary>
        /// The source kinds of this instance.
        /// </summary>
        [XmlArray("sourceKinds")]
        [XmlArrayItem("add")]
        public List<DataSourceKind> DefaultSourceKinds { get; set; } = null;

        /// <summary>
        /// The path of the folder of this instance.
        /// </summary>
        [XmlElement("defaultFolderPath")]
        public string DefaultFolderPath { get; set; } = null;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ConnectorIndexAttribute class.
        /// </summary>
        public ConnectorIndexAttribute() : base()
        {
        }

        #endregion
    }
}
