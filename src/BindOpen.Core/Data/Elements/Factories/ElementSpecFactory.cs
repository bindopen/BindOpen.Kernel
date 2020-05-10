using BindOpen.Data.Common;
using BindOpen.Data.Helpers.Objects;
using BindOpen.Extensions.Runtime;
using System;
using System.Collections.Generic;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// This static class provides methods to create element specifications.
    /// </summary>
    public static partial class ElementSpecFactory
    {
        // Element specification -------------------------

        /// <summary>
        /// Creates a data element of the specified kind.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        public static DataElementSpec Create(string name, DataValueTypes valueType)
        {
            if (valueType.IsScalar())
            {
                return new ScalarElementSpec(name, valueType);
            }
            else
            {
                switch (valueType)
                {
                    case DataValueTypes.Carrier:
                        return new CarrierElementSpec() { Name = name };
                    case DataValueTypes.Document:
                        return new DocumentElementSpec() { Name = name };
                    case DataValueTypes.Object:
                        return new ObjectElementSpec() { Name = name };
                    case DataValueTypes.Datasource:
                        return new SourceElementSpec() { Name = name };
                }
            }

            return null;
        }

        /// <summary>
        /// Creates a data element of the specified kind.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="type">The value type to consider.</param>
        public static DataElementSpec Create(string name, Type type)
        {
            if (type == null) return null;

            DataElementSpec spec = Create(name, type.GetValueType());

            if (spec != null)
            {
                if (type.IsArray)
                {
                    spec.MaximumItemNumber = -1;
                }
                else if (type.IsEnum)
                {
                    spec.ConstraintStatement.AddConstraint(
                        null,
                        "standard$" + KnownRoutineKind.ItemMustBeInList,
                        ElementFactory.CreateSet(
                            ElementFactory.CreateScalar(DataValueTypes.Text, type.GetEnumFields())));
                }
            }

            return spec;
        }
    }
}
