using BindOpen.Data.Meta;
using BindOpen.Data.Specification;
using System;

namespace BindOpen.Data
{
    /// <summary>
    /// This static class provides methods to create element specifications.
    /// </summary>
    public static partial class BdoData
    {
        /// <summary>
        /// Creates a data element of the specified kind.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        public static BdoMetaDataSpec NewMetaSpec(
            string name,
            DataValueTypes valueType)
        {
            if (valueType.IsScalar())
            {
                var scalarSpec = NewMetaSpec<BdoMetaScalarSpec>(name);
                scalarSpec.WithValueType(valueType);
                return scalarSpec;
            }
            else
            {
                switch (valueType)
                {
                    case DataValueTypes.Object:
                        return NewMetaSpec<BdoMetaObjectSpec>(name);
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
        public static BdoMetaDataSpec NewMetaSpec(
            string name,
            Type type)
        {
            var valueType = type.GetValueType();
            var spec = NewMetaSpec(name, valueType);
            spec.AsType(type);
            return spec;
        }

        /// <summary>
        /// Creates a data element of the specified kind.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        public static TElementSpec NewMetaSpec<TElementSpec>(string name = null)
        where TElementSpec : class, IBdoMetaDataSpec, new()
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
        public static TElementSpec NewMetaSpec<TElementSpec, T>(
            string name = null)
            where TElementSpec : class, IBdoMetaDataSpec, new()
        {
            var spec = NewMetaSpec<TElementSpec>(name, typeof(TElementSpec));
            return spec;
        }

        /// <summary>
        /// Creates a data element of the specified kind.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="type">The value type to consider.</param>
        public static TElementSpec NewMetaSpec<TElementSpec>(
            string name,
            Type type)
            where TElementSpec : class, IBdoMetaDataSpec, new()
        {
            if (type == null) return default;

            var spec = NewMetaSpec<TElementSpec>(name);
            spec.WithValueType(type.GetValueType());
            spec.AsType(type);

            return spec;
        }

        private static IBdoMetaDataSpec AsType(
            this IBdoMetaDataSpec spec,
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
        public static BdoMetaScalarSpec NewMetaScalarSpec<T>(string name = null)
        {
            return NewMetaSpec<BdoMetaScalarSpec, T>(name);
        }
    }
}
