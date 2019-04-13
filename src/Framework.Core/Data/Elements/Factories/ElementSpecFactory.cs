using System;
using System.Linq;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements._Object;
using BindOpen.Framework.Core.Data.Elements.Carrier;
using BindOpen.Framework.Core.Data.Elements.Document;
using BindOpen.Framework.Core.Data.Elements.Scalar;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Elements.Source;
using BindOpen.Framework.Core.Data.Specification.Design;
using BindOpen.Framework.Core.Extensions.Items.Routines;

namespace BindOpen.Framework.Core.Data.Elements.Factories
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
        /// <param name="valueType">The value type to consider.</param>
        public static DataElementSpec Create(DataValueType valueType)
        {
            if (valueType.IsScalar())
            {
                return new ScalarElementSpec(null, valueType);
            }
            else
            {
                switch (valueType)
                {
                    case DataValueType.Carrier:
                        return new CarrierElementSpec();
                    case DataValueType.Document:
                        return new DocumentElementSpec();
                    case DataValueType.Object:
                        return new ObjectElementSpec();
                    case DataValueType.DataSource:
                        return new SourceElementSpec();
                }
            }

            return null;
        }

        /// <summary>
        /// Creates a data element of the specified kind.
        /// </summary>
        /// <param name="type">The value type to consider.</param>
        public static DataElementSpec Create(Type type)
        {
            if (type == null) return null;

            DataElementSpec spec = Create(type.GetValueType());

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
