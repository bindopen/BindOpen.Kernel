using BindOpen.MetaData.Elements;
using BindOpen.MetaData.Specification;
using System;

namespace BindOpen.MetaData
{
    /// <summary>
    /// This static class provides methods to create element specifications.
    /// </summary>
    public static partial class BdoMeta
    {
        /// <summary>
        /// Creates a data element of the specified kind.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        public static BdoMetaElementSpec NewSpec(
            string name,
            DataValueTypes valueType)
        {
            if (valueType.IsScalar())
            {
                var scalarSpec = NewSpec<BdoMetaScalarSpec>(name);
                scalarSpec.WithValueType(valueType);
                return scalarSpec;
            }
            else
            {
                switch (valueType)
                {
                    case DataValueTypes.Carrier:
                        return NewSpec<BdoMetaCarrierSpec>(name);
                    case DataValueTypes.Datasource:
                        return NewSpec<BdoMetaSourceSpec>(name);
                    case DataValueTypes.Object:
                        return NewSpec<BdoMetaObjectSpec>(name);
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
        public static BdoMetaElementSpec NewSpec(
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
        where TElementSpec : class, IBdoMetaElementSpec, new()
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
            where TElementSpec : class, IBdoMetaElementSpec, new()
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
            where TElementSpec : class, IBdoMetaElementSpec, new()
        {
            if (type == null) return default;

            var spec = NewSpec<TElementSpec>(name);
            spec.WithValueType(type.GetValueType());
            spec.AsType(type);

            return spec;
        }

        private static IBdoMetaElementSpec AsType(
            this IBdoMetaElementSpec spec,
            Type type)
        {
            if (spec != null)
            {
                if (spec.GetType().IsAssignableFrom(typeof(IBdoMetaObjectSpec)))
                {
                    var objectSpec = spec as IBdoMetaObjectSpec;
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
        public static BdoMetaScalarSpec NewScalarSpec<T>(string name = null)
        {
            return NewSpec<BdoMetaScalarSpec, T>(name);
        }
    }
}
