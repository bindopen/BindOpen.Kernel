using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Specification;
using BindOpen.Framework.Core.Extensions.Runtime;
using System;
using System.Linq;

namespace BindOpen.Framework.Core.Data.Elements
{
    /// <summary>
    /// This static class provides methods to create element specifications.
    /// </summary>
    public static partial class ElementSpecFactory
    {
        // Static creators -------------------------

        /// <summary>
        /// Creates a data element of the specified kind.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        public static DataElementSpec Create(string name, DataValueType valueType)
        {
            if (valueType.IsScalar())
            {
                return new ScalarElementSpec(name, valueType);
            }
            else
            {
                switch (valueType)
                {
                    case DataValueType.Carrier:
                        return new CarrierElementSpec() { Name = name };
                    case DataValueType.Document:
                        return new DocumentElementSpec() { Name = name };
                    case DataValueType.Object:
                        return new ObjectElementSpec() { Name = name };
                    case DataValueType.Datasource:
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
                spec.DesignStatement.ControlType = type.GetDefaultControlType();

                if (type.IsArray)
                {
                    spec.MaximumItemNumber = -1;
                }
                else if (type.IsEnum)
                {
                    spec.ConstraintStatement.AddConstraint(
                       null,
                       "standard$" + KnownRoutineKind.ItemMustBeInList,
                       new DataElementSet(
                           ElementFactory.CreateScalar(DataValueType.Text, type.GetFields().Select(p => p.Name).ToList().Cast<Object>())));
                }
            }

            return spec;
        }
    }
}
