using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Extensions.Indexes
{
    /// <summary>
    /// This class represents an attribute of connector index.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ConnectorIndexAttribute : Attribute
    {

        // --------------------------------------------------
        // VARIABLES
        // --------------------------------------------------

        #region Variables

        private List<AppExtensionFilter> _Filters = new List<AppExtensionFilter>();

        private List<DataSourceKind> _DefaultSourceKinds = null;
        private string _DefaultFolderPath = null;

        #endregion


        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The filters of this instance.
        /// </summary>
        [XmlArray("filters")]
        [XmlArrayItem("add")]
        public List<AppExtensionFilter> Filters
        {
            get { return this._Filters; }
            set { this._Filters = value; }
        }

        /// <summary>
        /// The source kinds of this instance.
        /// </summary>
        [XmlArray("sourceKinds")]
        [XmlArrayItem("add")]
        public List<DataSourceKind> DefaultSourceKinds
        {
            get { return this._DefaultSourceKinds; }
            set { this._DefaultSourceKinds = value; }
        }

        /// <summary>
        /// The path of the folder of this instance.
        /// </summary>
        [XmlElement("defaultFolderPath")]
        public String DefaultFolderPath
        {
            get { return this._DefaultFolderPath; }
            set { this._DefaultFolderPath = value; }
        }

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
