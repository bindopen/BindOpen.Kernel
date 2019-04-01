using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Xml;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Elements.Carrier;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Elements.Source;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Definition.Handlers;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Data.References
{
    /// <summary>
    /// This class represents a data reference.
    /// </summary>
    [Serializable()]
    [XmlType("DataReference", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "data.reference", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class DataReference : DataItem, IDataReference
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private HandlerDefinition _handlerDefinition = null;

        #endregion

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
        public IDataElement SourceElement { get; set; } = null;

        /// <summary>
        /// The path detail of this instance.
        /// </summary>
        [XmlElement("path")]
        public IDataElementSet PathDetail { get; set; } = new DataElementSet();

        /// <summary>
        /// Specification of the PathDetail property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool PathDetailSpecified => PathDetail != null && (PathDetail.ElementsSpecified || PathDetail.DescriptionSpecified);

        /// <summary>
        /// Source item of this instance.
        /// </summary>
        [XmlIgnore()]
        public object Source => SourceElement?.FirstItem;

        /// <summary>
        /// Target item of this instance.
        /// </summary>
        [XmlIgnore()]
        public object TargetItem { get; set; } = null;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DataReference class.
        /// </summary>
        public DataReference()
            : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the DataReference class.
        /// </summary>
        /// <param name="dataHandlerUniqueName">The handler unique name to consider.</param>
        /// <param name="sourceElement">The source element to consider.</param>
        /// <param name="dynamicObject">The dynamic object of this instance.</param>
        public DataReference(
            string dataHandlerUniqueName,
            IDataElement sourceElement,
            DynamicObject dynamicObject) : this()
        {
            DataHandlerUniqueName = dataHandlerUniqueName;
            SourceElement = sourceElement;
            PathDetail.Update(DataElementSet.Create(dynamicObject));
        }

        /// <summary>
        /// Instantiates a new instance of the DataReference class.
        /// </summary>
        /// <param name="dataHandlerUniqueName">The handler unique name to consider.</param>
        /// <param name="sourceElement">The source element to consider.</param>
        /// <param name="pathDetail">The path detail to consider.</param>
        public DataReference(
            string dataHandlerUniqueName,
            IDataElement sourceElement = null,
            IDataElementSet pathDetail = null) : this()
        {
            DataHandlerUniqueName = dataHandlerUniqueName;
            SourceElement = sourceElement;
            PathDetail = pathDetail;
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

        // Items ------------------------------------

        /// <summary>
        /// Gets the items from the source of this instance and update the target items.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the retrieved items.</returns>
        public object Get(
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null)
        {
            Object item = null;

            log = (log ?? new Log());

            if (!log.Append(appScope.Check(true, true)).HasErrorsOrExceptions())
            {
                if ((_handlerDefinition = appScope.AppExtension.GetItemDefinitionWithUniqueId<HandlerDefinition>(DataHandlerUniqueName)) == null)
                {
                    log.AddError(title: "Data handler definition '" + DataHandlerUniqueName + "' not found");
                }
                else if (_handlerDefinition.RuntimeFunction_Get == null)
                    log.AddError(title: "Get function missing in handler definition '" + DataHandlerUniqueName + "'");
                else
                {
                    log.AddEvents(Check<DataReference>());

                    if (!log.HasErrorsOrExceptions())
                    {
                        item = TargetItem = _handlerDefinition.RuntimeFunction_Get(
                           SourceElement, PathDetail, appScope, scriptVariableSet, log).GetObjectAtIndex(0);
                    }
                }
            }

            return item;
        }

        /// <summary>
        /// Posts the items to the source of this instance.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the posted items.</returns>
        public List<object> Post(
            List<object> items,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null)
        {
            List<object> dataItems = new List<object>();

            log = (log ?? new Log());

            if (!log.Append(appScope.Check(true, true)).HasErrorsOrExceptions())
            {
                if ((_handlerDefinition = appScope.AppExtension.GetItemDefinitionWithUniqueId<HandlerDefinition>(DataHandlerUniqueName)) == null)
                {
                    log.AddError(title: "Data reference definition not found");
                }
                else if (_handlerDefinition.RuntimeFunction_Post == null)
                {
                    log.AddError(title: "Post function missing in handler definition");
                }
                else if (items != null)
                {
                    log.AddEvents(Check<DataReference>());

                    IDataElement sourceElement = null;
                    if (!log.HasErrorsOrExceptions())
                    {
                        foreach (object item in items)
                        {
                            dataItems.AddRange(_handlerDefinition.RuntimeFunction_Post(
                                item, ref sourceElement, appScope, scriptVariableSet, log));
                        }
                    }
                }
            }

            return dataItems;
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
            DataReference dataReference = base.Clone() as DataReference;
            if (SourceElement != null)
                dataReference.SourceElement = SourceElement.Clone() as DataElement;
            if (PathDetail != null)
                dataReference.PathDetail = PathDetail.Clone() as DataElementSet;
            dataReference.TargetItem = TargetItem;
            return dataReference;
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
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(IAppScope appScope = null,  ILog log = null)
        {
            log = (log ?? new Log());

            if (PathDetail != null)
            {
                foreach (DataElement dataElement in PathDetail.Elements)
                {
                    dataElement.UpdateRuntimeInfo(appScope, log);
                }
            }

            if (SourceElement != null)
            {
                SourceElement.UpdateRuntimeInfo(appScope, log);
            }
        }

        #endregion
    }
}