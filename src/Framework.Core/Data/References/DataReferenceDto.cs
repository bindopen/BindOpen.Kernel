using System;
using System.Xml;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Elements.Carrier;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Elements.Source;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Data.References
{
    /// <summary>
    /// This class represents a data reference configuration.
    /// </summary>
    [Serializable()]
    [XmlType("DataReference", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "data.reference", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class DataReferenceDto : DataItem, IDataReferenceDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The dtaa handler unique name of this instance.
        /// </summary>
        [XmlAttribute("handler")]
        public string DataHandlerUniqueName { get; set; } = null;

        /// <summary>
        /// Source element of this instance.
        /// </summary>
        [XmlElement("carrier", typeof(CarrierElement))]
        [XmlElement("source", typeof(SourceElement))]
        public DataElement SourceElement { get; set; } = null;

        /// <summary>
        /// The path detail of this instance.
        /// </summary>
        [XmlElement("path")]
        public DataElementSet PathDetail { get; set; } = new DataElementSet();

        /// <summary>
        /// Specification of the PathDetail property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool PathDetailSpecified => PathDetail != null && (PathDetail.ElementsSpecified || PathDetail.DescriptionSpecified);

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DataReference class.
        /// </summary>
        public DataReferenceDto()
            : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the DataReference class.
        /// </summary>
        /// <param name="dataHandlerUniqueName">The handler unique name to consider.</param>
        /// <param name="sourceElement">The source element to consider.</param>
        /// <param name="dynamicObject">The dynamic object of this instance.</param>
        public DataReferenceDto(
            string dataHandlerUniqueName,
            IDataElement sourceElement,
            object dynamicObject) : this()
        {
            DataHandlerUniqueName = dataHandlerUniqueName;
            SourceElement = sourceElement as DataElement;
            PathDetail.Update(ElementFactory.CreateSet<DataElementSet>(dynamicObject));
        }

        /// <summary>
        /// Instantiates a new instance of the DataReference class.
        /// </summary>
        /// <param name="dataHandlerUniqueName">The handler unique name to consider.</param>
        /// <param name="sourceElement">The source element to consider.</param>
        /// <param name="pathDetail">The path detail to consider.</param>
        public DataReferenceDto(
            string dataHandlerUniqueName,
            IDataElement sourceElement = null,
            IDataElementSet pathDetail = null) : this()
        {
            DataHandlerUniqueName = dataHandlerUniqueName;
            SourceElement = sourceElement as DataElement;
            PathDetail = pathDetail as DataElementSet;
        }

        #endregion

        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        // Source ------------------------------------

        /// <summary>
        /// Gets the primary source of this instance.
        /// </summary>
        /// <returns>Returns the initial source of this instance.</returns>
        public IStoredDataItem GetPrimarySource() => SourceElement != null ? GetPrimarySource() : SourceElement;

        /// <summary>
        /// Gets the value type of the primary source of this instance.
        /// </summary>
        /// <returns>Returns the value type of the primary source of this instance.</returns>
        public DataValueType GetPrimarySourceValueType()
        {
            IStoredDataItem storedDataItem = GetPrimarySource();
            if (storedDataItem != null)
                return storedDataItem.GetValueType();

            return DataValueType.None;
        }

        /// <summary>
        /// Gets the initial data source of this instance.
        /// </summary>
        /// <returns>Returns the initial data source of this instance.</returns>
        public IDataSource GetDataSource()
        {
            return (SourceElement != null ? GetPrimarySource() : SourceElement) as IDataSource;
        }

        /// <summary>
        /// Gets the initial data source kind of this instance.
        /// </summary>
        /// <returns>Returns the initial data source kind of this instance.</returns>
        public DataSourceKind GetDataSourceKind()
        {
            IDataSource dataSource = GetDataSource();
            if (dataSource != null)
                return dataSource.Kind;

            return DataSourceKind.None;
        }

        #endregion

        // --------------------------------------------------
        // CLONING
        // --------------------------------------------------

        #region Cloning

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns a cloned instance.</returns>
        public override object Clone()
        {
            DataReferenceDto dto = base.Clone() as DataReferenceDto;
            if (SourceElement != null)
                dto.SourceElement = SourceElement.Clone() as DataElement;
            if (PathDetail != null)
                dto.PathDetail = PathDetail.Clone() as DataElementSet;
            return dto;
        }

        #endregion

        // --------------------------------------------------
        // SERIALIZATION
        // --------------------------------------------------

        #region Serialization

        /// <summary>
        /// Updates information for storage.
        /// </summary>
        /// <param name="log">The log to update.</param>
        public override void UpdateStorageInfo(ILog log = null)
        {
            if (PathDetail != null)
            {
                foreach (DataElement dataElement in PathDetail.Elements)
                {
                    dataElement.UpdateStorageInfo(log);
                }
            }

            SourceElement?.UpdateStorageInfo(log);
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null)
        {
            log = (log ?? new Log());

            if (PathDetail != null)
            {
                foreach (DataElement dataElement in PathDetail.Elements)
                {
                    dataElement.UpdateRuntimeInfo(appScope, scriptVariableSet, log);
                }
            }

            SourceElement?.UpdateRuntimeInfo(appScope, scriptVariableSet, log);
        }

        #endregion
    }
}