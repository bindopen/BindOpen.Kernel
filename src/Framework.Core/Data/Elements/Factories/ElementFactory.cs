using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements._Object;
using BindOpen.Framework.Core.Data.Elements.Carrier;
using BindOpen.Framework.Core.Data.Elements.Scalar;
using BindOpen.Framework.Core.Data.Elements.Source;
using BindOpen.Framework.Core.Extensions.Runtime.Items;
using System.Linq;

namespace BindOpen.Framework.Core.Data.Elements
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
            return Create(name, DataValueType.Any, null, null, items);
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
            return Create(name, DataValueType.Any, null, null, new[] { item });
        }

        /// <summary>
        /// Creates a data element of the specified kind.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        /// <param name="specification">The specification to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static IDataElement Create(
            string name,
            DataValueType valueType = DataValueType.Any,
            IDataElementSpec specification = null,
            params object[] items)
        {
            IDataElement element = null;

            if (valueType == DataValueType.Any)
            {
                valueType = items.GetValueType();
            }

            if (valueType.IsScalar())
            {
                element = CreateScalar(name, null as string, valueType, specification as IScalarElementSpec, items);
            }
            else
            {
                string definitionUniqueId = null;
                switch (valueType)
                {
                    case DataValueType.Carrier:
                        definitionUniqueId = ((items.Length > 0 ? items[0] : null) as IBdoCarrierConfiguration)?.DefinitionUniqueId;
                        element = CreateCarrier(name, null, definitionUniqueId, specification as ICarrierElementSpec);
                        break;
                    case DataValueType.Datasource:
                        definitionUniqueId = ((items.Length > 0 ? items[0] : null) as IBdoConnectorConfiguration)?.DefinitionUniqueId;
                        element = CreateSource(name, null, definitionUniqueId, specification as ISourceElementSpec);
                        break;
                    case DataValueType.Dictionary:
                        break;
                    case DataValueType.Document:
                        element = CreateDocument(name, null);
                        break;
                    case DataValueType.Object:
                        string classFullName = (specification as IObjectElementSpec)?.ClassFilter.GetValues().FirstOrDefault();
                        if (string.IsNullOrEmpty(classFullName) && items.Length > 0)
                            classFullName = items[0]?.GetType().ToString();
                        element = CreateObject(name, null, classFullName, specification as IObjectElementSpec);
                        break;
                    case DataValueType.Schema:
                        break;
                    case DataValueType.SchemaZone:
                        break;
                    case DataValueType.StringValued:
                        break;
                }

                if (items != null)
                {
                    element?.SetItems(items);
                }
            }

            return element;
        }

        ///// <summary>
        ///// Creates a data element of the specified kind.
        ///// </summary>
        ///// <param name="type">The value type to consider.</param>
        ///// <param name="name">The name to consider.</param>
        ///// <param name="scope">The scope to consider.</param>
        ///// <param name="specification">The specification to consider.</param>
        //public static IDataElement Create(
        //    string name,
        //    Type type,
        //    IBdoScope scope = null,
        //    IDataElementSpec specification = null)
        //{
        //    if (type == null) return null;

        //    IDataElement element = Create(name, type.GetValueType(), scope, specification);

        //    if (element?.Specification != null)
        //    {
        //        element.Specification.DesignStatement.ControlType = type.GetDefaultControlType();

        //        if (type.IsArray)
        //        {
        //            element.Specification.MaximumItemNumber = -1;
        //        }
        //        else if (type.IsEnum)
        //        {
        //            element.Specification.ConstraintStatement.AddConstraint(
        //               null,
        //               "standard$" + KnownRoutineKind.ItemMustBeInList,
        //               new DataElementSet(
        //                   CreateScalar(DataValueType.Text, type.GetFields().Select(p => p.Name).ToList().Cast<Object>())));
        //        }
        //    }

        //    return element;
        //}
    }
}
