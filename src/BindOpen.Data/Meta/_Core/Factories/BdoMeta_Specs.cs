﻿using System;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This static class provides methods to create element specifications.
    /// </summary>
    public static partial class BdoMeta
    {
        /// <summary>
        /// Creates a data element of the specified kind.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="valueType">The value type to consider.</param>
        public static BdoSpec NewSpec(
            string name = null,
            DataValueTypes valueType = DataValueTypes.Any)
        {
            var spec = NewSpec<BdoSpec>(name);
            spec.WithDataValueType(valueType);
            return spec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="name"></param>
        /// <param key="type"></param>
        /// <returns></returns>
        public static BdoSpec NewSpec(
            Type type)
            => NewSpec(null, type);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="name"></param>
        /// <param key="type"></param>
        /// <returns></returns>
        public static BdoSpec NewSpec(
            string name,
            Type type)
        {
            var valueType = type.GetValueType();
            var spec = NewSpec(name, valueType);
            spec.AsType(type);
            return spec;
        }

        // NewSpec<T>

        /// <summary>
        /// Creates a data element of the specified kind.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="valueType">The value type to consider.</param>
        public static T NewSpec<T>(
            string name = null)
            where T : class, IBdoSpec, new()
        {
            var spec = new T();
            spec.WithName(name);

            return spec;
        }

        /// <summary>
        /// Creates a data element of the specified kind.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="type">The value type to consider.</param>
        public static T NewSpec<T>(
            Type type)
            where T : class, IBdoSpec, new()
            => NewSpec<T>(null, type);

        /// <summary>
        /// Creates a data element of the specified kind.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="type">The value type to consider.</param>
        public static T NewSpec<T>(
            string name,
            Type type)
            where T : class, IBdoSpec, new()
        {
            if (type == null) return default;

            var spec = NewSpec<T>(name)
                .WithDataValueType(type.GetValueType())
                .AsType(type);

            return spec;
        }

        public static T AsType<T>(
            this T spec,
            Type type)
            where T : IBdoSpec
        {
            if (spec != null)
            {
                //if (spec.GetType().IsAssignableFrom(typeof(IBdoObjectSpec)))
                //{
                //    var objectSpec = spec as IBdoObjectSpec;
                //    objectSpec.ClassFilter.AddedValues.Add(spec.GetType().ToString());
                //}

                if (type.IsArray)
                {
                    spec.WithMaxDataItemNumber();
                }
                else if (type.IsEnum)
                {
                    spec.Condition ??= BdoData.NewCondition(null);
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
    }
}