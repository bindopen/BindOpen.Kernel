using System;
using System.Linq;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements._Object;
using BindOpen.Framework.Core.Data.Elements.Carrier;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Elements.Source;
using BindOpen.Framework.Core.Data.Specification.Design;
using BindOpen.Framework.Core.Extensions.Definition.Carriers;
using BindOpen.Framework.Core.Extensions.Definition.Connectors;
using BindOpen.Framework.Core.Extensions.Items.Routines;

namespace BindOpen.Framework.Core.Data.Elements.Factories
{
    /// <summary>
    /// This static class provides methods to create data elements.
    /// </summary>
    public static partial class ElementFactory
    {
        // Static creators -------------------------

        /// <summary>
        /// Creates a data element with specified items.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static IDataElement Create(
            string name,
            object[] items)
        {
            if (items == null) return null;

            DataValueType valueType = items.GetValueType();

            IDataElement element = null;
            if (valueType != DataValueType.Any)
            {
                element = Create(name, valueType);
                element?.SetItems(items);
            }

            return element;
        }

        /// <summary>
        /// Creates a data element of the specified kind.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="item">The item to consider.</param>
        public static IDataElement Create(
            string name,
            object item)
        {
            return Create(name, new[] { item });
        }

        /// <summary>
        /// Creates a data element of the specified kind.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="specification">The specification to consider.</param>
        public static IDataElement Create(
            string name,
            DataValueType valueType,
            IAppScope appScope = null,
            IDataElementSpec specification = null)
        {
            IDataElement element = null;

            if (valueType.IsScalar())
            {
                element = CreateScalar(name, null, valueType, specification);
            }
            else
            {
                string definitionUniqueId = null;
                switch (valueType)
                {
                    case DataValueType.Carrier:
                        definitionUniqueId = (specification as ICarrierElementSpec)?.DefinitionFilter.GetValues(
                            appScope?.AppExtension.GetItemDefinitionUniqueIds<ICarrierDefinition>()).FirstOrDefault();
                        element = CreateCarrier(name, null, definitionUniqueId, specification as ICarrierElementSpec);
                        break;
                    case DataValueType.DataSource:
                        definitionUniqueId = (specification as ISourceElementSpec)?.DefinitionFilter.GetValues(
                            appScope?.AppExtension.GetItemDefinitionUniqueIds<IConnectorDefinition>()).FirstOrDefault();
                        element = CreateSource(name, null, definitionUniqueId, specification as ISourceElementSpec);
                        break;
                    case DataValueType.Dictionary:
                        break;
                    case DataValueType.Document:
                        element = CreateDocument(name, null);
                        break;
                    case DataValueType.Object:
                        string classFullName = (specification as IObjectElementSpec)?.ClassFilter.GetValues().FirstOrDefault();
                        element = CreateObject(name, null, classFullName, specification as IObjectElementSpec);
                        break;
                    case DataValueType.Schema:
                        break;
                    case DataValueType.SchemaZone:
                        break;
                    case DataValueType.StringValued:
                        break;
                }
            }

            return element;
        }

        /// <summary>
        /// Creates a data element of the specified kind.
        /// </summary>
        /// <param name="type">The value type to consider.</param>
        /// <param name="name">The name to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="specification">The specification to consider.</param>
        public static IDataElement Create(
            string name,
            Type type,
            IAppScope appScope = null,
            IDataElementSpec specification = null)
        {
            if (type == null) return null;

            IDataElement element = Create(name, type.GetValueType(), appScope, specification);

            if (element?.Specification != null)
            {
                element.Specification.DesignStatement.ControlType = type.GetDefaultControlType();

                if (type.IsArray)
                {
                    element.Specification.MaximumItemNumber = -1;
                }
                else if (type.IsEnum)
                {
                    element.Specification.ConstraintStatement.AddConstraint(
                       null,
                       "standard$" + KnownRoutineKind.ItemMustBeInList,
                       new DataElementSet(
                           CreateScalar(DataValueType.Text, type.GetFields().Select(p => p.Name).ToList().Cast<Object>())));
                }
            }

            return element;
        }
    }
}
