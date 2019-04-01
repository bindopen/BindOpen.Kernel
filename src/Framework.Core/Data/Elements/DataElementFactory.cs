using System;
using System.Collections.Generic;
using System.Linq;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements._Object;
using BindOpen.Framework.Core.Data.Elements.Carrier;
using BindOpen.Framework.Core.Data.Elements.Document;
using BindOpen.Framework.Core.Data.Elements.Entity;
using BindOpen.Framework.Core.Data.Elements.Scalar;
using BindOpen.Framework.Core.Data.Elements.Schema;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Elements.Source;
using BindOpen.Framework.Core.Data.Specification.Design;
using BindOpen.Framework.Core.Extensions.Configuration.Routines;
using BindOpen.Framework.Core.Extensions.Definition.Carriers;
using BindOpen.Framework.Core.Extensions.Definition.Connectors;
using BindOpen.Framework.Core.Extensions.Definition.Entities;

namespace BindOpen.Framework.Core.Data.Elements
{
    /// <summary>
    /// This static class provides methods to create data elements.
    /// </summary>
    public static class DataElementFactory
    {
        // Static creators -------------------------

        /// <summary>
        /// Creates a data element of the specified kind.
        /// </summary>
        /// <param name="objects">The objets to consider.</param>
        /// <param name="dataValueType">The data value type to consider.</param>
        /// <param name="name">The name to consider.</param>
        public static DataElement Create(
            List<object> objects,
            DataValueType dataValueType = DataValueType.Any,
            string name = null)
        {
            if (objects == null) return null;

            if (dataValueType == DataValueType.Any)
                dataValueType = objects.GetValueType();

            DataElement dataElement = null;
            if (dataValueType != DataValueType.Any)
            {
                dataElement = DataElement.Create(dataValueType, name);
                if (dataElement != null)
                    dataElement.SetItems(objects);
            }

            return dataElement;
        }

        /// <summary>
        /// Creates a data element of the specified kind.
        /// </summary>
        /// <param name="object1">The objet to consider.</param>
        /// <param name="dataValueType">The data value type to consider.</param>
        /// <param name="name">The name to consider.</param>
        public static DataElement Create(
            Object object1 = null,
            DataValueType dataValueType = DataValueType.Any,
            string name = null)
        {
            if (dataValueType == DataValueType.Any)
                dataValueType = object1.GetValueType();

            DataElement dataElement = DataElement.Create(dataValueType, name);
            if (dataElement != null && object1 != null)
                dataElement.SetItem(object1);

            return dataElement;
        }

        /// <summary>
        /// Creates a data element of the specified kind.
        /// </summary>
        /// <param name="valueType">The value type to consider.</param>
        /// <param name="name">The name to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="specification">The specification to consider.</param>
        public static DataElement Create(
            DataValueType valueType,
            string name = null,
            IAppScope appScope = null,
            DataElementSpec specification = null)
        {
            DataElement dataElement = null;

            if (valueType.IsScalar())
            {
                dataElement = new ScalarElement(name, "", valueType, specification as ScalarElementSpec);
            }
            else
            {
                string definitionUniqueName = null;
                switch (valueType)
                {
                    case DataValueType.CarrierConfiguration:
                        definitionUniqueName = (specification as CarrierElementSpec)?.DefinitionFilter.GetValues(
                            appScope?.AppExtension.GetItemDefinitionUniqueIds<CarrierDefinition>()).FirstOrDefault();
                        dataElement = new CarrierElement(
                            name, "", definitionUniqueName,
                            specification as CarrierElementSpec);
                        break;
                    case DataValueType.DataSource:
                        definitionUniqueName = (specification as SourceElementSpec)?.ConnectorFilter.GetValues(
                            appScope?.AppExtension.GetItemDefinitionUniqueIds<ConnectorDefinition>()).FirstOrDefault();
                        dataElement = new SourceElement(name, null);
                        break;
                    case DataValueType.Dictionary:
                        //dataElement = new SourceElement(name, namePreffix);
                        break;
                    case DataValueType.Document:
                        dataElement = new DocumentElement(name, null as CarrierElement, null);
                        break;
                    case DataValueType.Entity:
                        definitionUniqueName = (specification as EntityElementSpec)?.EntityFilter.GetValues(
                            appScope?.AppExtension.GetItemDefinitionUniqueIds<EntityDefinition>()).FirstOrDefault();
                        dataElement = new EntityElement(name, "", definitionUniqueName, specification as EntityElementSpec);
                        break;
                    case DataValueType.Object:
                        definitionUniqueName = (specification as ObjectElementSpec)?.ClassFilter.GetValues().FirstOrDefault();
                        dataElement = new ObjectElement(name, "", definitionUniqueName, specification as ObjectElementSpec);
                        break;
                    case DataValueType.Schema:
                        dataElement = new SchemaElement(name);
                        break;
                    case DataValueType.SchemaZone:
                        dataElement = new SchemaZoneElement(name);
                        break;
                    case DataValueType.StringValued:
                        //dataElement = new StringValuedElement(name, namePreffix);
                        break;
                }
            }

            return dataElement;
        }

        /// <summary>
        /// Creates a data element of the specified kind.
        /// </summary>
        /// <param name="type">The value type to consider.</param>
        /// <param name="name">The name to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="specification">The specification to consider.</param>
        public static DataElement Create(
            Type type,
            string name = null,
            IAppScope appScope = null,
            DataElementSpec specification = null)
        {
            if (type == null) return null;

            DataElement dataElement = DataElement.Create(type.GetValueType(), name, appScope, specification);

            if (dataElement?.Specification != null)
            {
                dataElement.Specification.DesignStatement.ControlType = type.GetDefaultControlType();

                if (type.IsArray)
                {
                    dataElement.Specification.MaximumItemNumber = -1;
                }
                else if (type.IsEnum)
                {
                    dataElement.Specification.ConstraintStatement.AddConstraint(
                       null, "standard$" + BasicRoutineKind.ItemMustBeInList, new DataElementSet(
                           DataElement.Create(type.GetFields().Select(p => p.Name).ToList().Cast<Object>(), DataValueType.Text)));
                }
            }

            return dataElement;
        }

        // Specification ---------------------

        /// <summary>
        /// Gets a new specification.
        /// </summary>
        /// <returns>Returns the new specifcation.</returns>
        public virtual DataElementSpec CreateSpecification()
        {
            return null;
        }

        /// <summary>
        /// Creates a new data element respecting this instance.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="detail">The detail to consider.</param>
        /// <returns>Returns a new data element respecting this instance.</returns>
        public virtual DataElement NewElement(IAppScope appScope = null, IDataElementSet detail = null)
        {
            return null;
        }


        /// <summary>
        /// Creates a new specification of this instance.
        /// </summary>
        /// <returns>Returns True .</returns>
        public bool NewSpecification()
        {
            return (Specification = CreateSpecification())!=null;
        }

        /// <summary>
        /// Creates a new data element respecting this instance.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="detail">The detail to consider.</param>
        /// <returns>Returns a new data element respecting this instance.</returns>
        public override DataElement NewElement(
            IAppScope appScope = null,
            IDataElementSet detail = null)
        {
            IDataSource dataSource = new DataSource(null, DataSourceKind);

            if (appScope != null && ConnectorFilter != null)
            {
                List<string> connectorUniqueNames = ConnectorFilter.GetValues(
                    appScope.AppExtension.GetItemDefinitions<ConnectorDefinition>().Select(p => p.Key()).ToList());
                if (connectorUniqueNames.Count > 0)
                    dataSource.AddConfiguration(appScope, null, connectorUniqueNames[0], detail);
            }

            SourceElement dataSourceElement = new SourceElement(Name, dataSource)
            {
                Title = Title.Clone() as DictionaryDataItem,
                Description = Description.Clone() as DictionaryDataItem,
            };

            return dataSourceElement;
        }

        /// <summary>
        /// Creates a new element set from this instance.
        /// </summary>
        /// <returns>Returns the element set.</returns>
        public DataElementSet NewElementSet()
        {
            DataElementSet dataElementSet = new DataElementSet();
            foreach (DataElementSpec dataElementSpec in this._items)
                dataElementSet.Add(dataElementSpec.NewElement());

            return dataElementSet;
        }

        /// <summary>
        /// Adds the specified element.
        /// </summary>
        /// <param name="elementUniqueName">The key of the element to add.</param>
        /// <param name="item">The item of the element to add.</param>
        /// <param name="valueType">The value type of the element to add.</param>
        /// <param name="referenceElementSet">The reference set of elements to consider.</param>
        /// <returns>Returns the new element that has been added.
        /// Returns null if the new element is null or else its name is null.</returns>
        /// <remarks>The new element must have a name.</remarks>
        public IDataElement AddElement(
            string elementUniqueName,
            object item,
            DataValueType valueType = DataValueType.Any,
            IDataElementSet referenceElementSet = null)
        {
            return AddElement(elementUniqueName, new List<object> { item }, valueType, referenceElementSet);
        }

        /// <summary>
        /// Adds the specified element.
        /// </summary>
        /// <param name="elementUniqueName">The key of the element to add.</param>
        /// <param name="items">The items of the element to add.</param>
        /// <param name="valueType">The value type of the element to add.</param>
        /// <param name="referenceElementSet">The reference set of elements to consider.</param>
        /// <returns>Returns the new element that has been added.
        /// Returns null if the new element is null or else its name is null.</returns>
        /// <remarks>The new element must have a name.</remarks>
        public IDataElement AddElement(
            string elementUniqueName,
            List<object> items,
            DataValueType valueType = DataValueType.Any,
            IDataElementSet referenceElementSet = null)
        {
            IDataElement element = null;
            if (referenceElementSet == null)
            {
                AddElement(element = DataElement.Create(items, valueType, elementUniqueName));
            }
            else
            {
                IDataElement referenceElement = referenceElementSet.GetItem(elementUniqueName);
                if (referenceElement is DataElement)
                {
                    element = referenceElement.Clone() as DataElement;
                    element.SetItems(items);
                }
            }

            return element;
        }

        /// <summary>
        /// Check the specified item.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>The log of check log.</returns>
        public ILog CheckItem(
            object item = null,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null)
        {
            IILog log = new Log();

            if ((aItem == null) && (Specification != null) && (Specification.IsValueList))
                aItem = GetItem();

            if (Specification != null)
                log = Specification.CheckItem(aItem, this, appScope, scriptVariableSet);

            return log;
        }

        /// <summary>
        /// Indicates whether this instance is compatible with the specified element.
        /// </summary>
        /// <param name="specification">The data element specification to consider.</param>
        /// <returns>True if this instance is compatible with the specified data elements.</returns>
        public bool IsCompatibleWith(DataElementSpec specification)
        {
            if (specification == null)
                return true;

            return specification.IsCompatibleWith(this);
        }

        Log CheckItem(object item = null, IAppScope appScope = null, IScriptVariableSet scriptVariableSet = null);

    }
}
