using BindOpen.Data.Specification;
using System;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// This static class provides methods to create element specifications.
    /// </summary>
    public static partial class BdoElements
    {
        /// <summary>
        /// Creates a data element of the specified kind.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        public static BdoElementSpec NewSpec(
            string name,
            DataValueTypes valueType)
        {
            if (valueType.IsScalar())
            {
                var scalarSpec = NewSpec<ScalarElementSpec>(name);
                scalarSpec.WithValueType(valueType);
                return scalarSpec;
            }
            else
            {
                switch (valueType)
                {
                    case DataValueTypes.Carrier:
                        return NewSpec<CarrierElementSpec>(name);
                    case DataValueTypes.Datasource:
                        return NewSpec<SourceElementSpec>(name);
                    case DataValueTypes.Object:
                        return NewSpec<ObjectElementSpec>(name);
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static BdoElementSpec NewSpec(
            string name,
            Type type)
        {
            var valueType = type.GetValueType();
            var spec = NewSpec(name, valueType);
            spec.AsType(type);
            return spec;
        }

        /// <summary>
        /// Creates a data element of the specified kind.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        public static TElementSpec NewSpec<TElementSpec>(string name = null)
        where TElementSpec : class, IBdoElementSpec, new()
        {
            var spec = new TElementSpec();
            spec.WithName(name);

            return spec;
        }

        /// <summary>
        /// Creates a data element of the specified kind.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="type">The value type to consider.</param>
        public static TElementSpec NewSpec<TElementSpec, T>(
            string name = null)
            where TElementSpec : class, IBdoElementSpec, new()
        {
            var spec = NewSpec<TElementSpec>(name, typeof(TElementSpec));
            return spec;
        }

        /// <summary>
        /// Creates a data element of the specified kind.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="type">The value type to consider.</param>
        public static TElementSpec NewSpec<TElementSpec>(
            string name,
            Type type)
            where TElementSpec : class, IBdoElementSpec, new()
        {
            if (type == null) return default;

            var spec = NewSpec<TElementSpec>(name);
            spec.WithValueType(type.GetValueType());
            spec.AsType(type);

            return spec;
        }

        private static IBdoElementSpec AsType(
            this IBdoElementSpec spec,
            Type type)
        {
            if (spec != null)
            {
                if (spec.GetType().IsAssignableFrom(typeof(IObjectElementSpec)))
                {
                    var objectSpec = spec as IObjectElementSpec;
                    objectSpec.ClassFilter.AddedValues.Add(spec.GetType().ToString());
                }

                if (type.IsArray)
                {
                    spec.WithMaximumItemNumber(-1);
                }
                else if (type.IsEnum)
                {
                    spec.WithConstraintStatement(spec.ConstraintStatement ?? new DataConstraintStatement());
                    //spec.ConstraintStatement.Add(
                    //    BdoMango.
                    //    null,
                    //    KnownRoutineKinds.ItemMustBeInList,
                    //    BdoElement.CreateSet(
                    //        BdoElement.CreateScalar(DataValueTypes.Text, type.GetEnumFields())));
                }
            }
            return spec;
        }

        /// <summary>
        /// Creates a data element of the specified kind.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        public static ScalarElementSpec NewScalarSpec<T>(string name = null)
        {
            return NewSpec<ScalarElementSpec, T>(name);
        }
    }
}
