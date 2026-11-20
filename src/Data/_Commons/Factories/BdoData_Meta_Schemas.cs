using BindOpen.Data.Meta;
using BindOpen.Data.Meta.Reflection;
using BindOpen.Data.Schema;
using System;
using System.Linq;

namespace BindOpen.Data
{
    /// <summary>
    /// This static class provides methods to create element schemas.
    /// </summary>
    public static partial class BdoData
    {
        // NewSchema

        /// <summary>
        /// Creates a data element of the specified kind.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="valueType">The value type to consider.</param>
        public static BdoSchema NewSchema(
            string name = null,
            DataValueTypes valueType = DataValueTypes.Any,
            object defaultData = null)
            => NewSchema<BdoSchema>(name, valueType, defaultData);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="name"></param>
        /// <param key="type"></param>
        /// <returns></returns>
        public static BdoSchema NewSchema(string name, Type type)
            => NewSchema<BdoSchema>(name, type);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="name"></param>
        /// <param key="type"></param>
        /// <returns></returns>
        public static BdoSchema NewSchema(Type type)
            => NewSchema<BdoSchema>(null, type);

        // NewSchema<T>

        /// <summary>
        /// Creates a data element of the specified kind.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="valueType">The value type to consider.</param>
        public static T NewSchema<T>(
            string name = null,
            DataValueTypes valueType = DataValueTypes.Any,
            object defaultData = null)
            where T : IBdoSchema, new()
        {
            var schema = new T();
            schema.WithName(name)
                .WithDataType(valueType)
                .WithDefaultData(defaultData);

            return schema;
        }

        public static T NewSchema<T>(
            string name,
            Type type)
            where T : IBdoSchema, new()
        {
            if (type == null) return default;

            var schema = NewSchema<T>(name)
                .WithDataType(type.GetValueType())
                .AsType(type);

            return schema;
        }

        /// <summary>
        /// Creates a data element of the specified kind.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="type">The value type to consider.</param>
        public static T NewSchema<T>(
            Type type)
            where T : IBdoSchema, new()
            => NewSchema<T>(null, type);

        // with children

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="specs">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoSchema NewSchema(
            string name,
            params IBdoSchema[] specs)
        {
            var list = NewSchema<BdoSchema>()
                .WithChildren(specs)
                .WithName(name);

            return list;
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="specs">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoSchema NewSchema(
            params IBdoSchema[] specs)
            => NewSchema(null, specs);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoSchema NewSchema(
            string name,
            params (string Name, DataValueTypes ValueType)[] pairs)
        {
            var schema = NewSchema(
                pairs.Select(q => NewSchema<BdoSchema>(q.Name, q.ValueType)).ToArray())
                .WithName(name);

            return schema;
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoSchema NewSchema(
            params (string Name, DataValueTypes ValueType)[] pairs)
            => NewSchema<BdoSchema>(pairs);

        // Static T creators -------------------------
        // with children

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="specs">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewSchema<T>(
            string name,
            params IBdoSchema[] specs)
            where T : IBdoSchema, new()
        {
            var schema = NewSchema<T>()
                .WithChildren(specs)
                .WithName(name);

            return schema;
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="specs">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewSchema<T>(
            params IBdoSchema[] specs)
            where T : IBdoSchema, new()
            => NewSchema<T>(null, specs);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewSchema<T>(
            string name,
            params (string Name, DataValueTypes ValueType)[] pairs)
            where T : IBdoSchema, new()
        {
            var schema = NewSchema<T>()
                .WithChildren(pairs.Select(q => NewSchema<BdoSchema>(q.Name, q.ValueType)).Cast<IBdoSchema>().ToArray())
                .WithName(name);

            return schema;
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewSchema<T>(
            params (string Name, DataValueTypes ValueType)[] pairs)
            where T : IBdoSchema, new()
            => NewSchema<T>(null, pairs);

        // AsType

        /// <summary>
        /// Creates a data element of the specified kind.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="type">The value type to consider.</param>
        public static T AsType<T>(
            this T schema,
            Type type)
            where T : IBdoSchema
        {
            if (schema != null)
            {
                if (type.IsArray)
                {
                    schema.WithMaxDataItemNumber();
                }
            }
            return schema;
        }

        // From

        /// <summary>
        /// Creates a meta data of the specified object.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static Q NewSchemaFrom<T, Q>(
            string name = null,
            bool onlyMetaAttributes = true)
            where Q : IBdoSchema, new()
            => typeof(T).ToSpec<Q>(name, onlyMetaAttributes);

        /// <summary>
        /// Creates a meta data of the specified object.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static IBdoSchema NewSchemaFrom<T>(
            string name = null,
            bool onlyMetaAttributes = true)
            => typeof(T).ToSpec(name, onlyMetaAttributes);
    }
}
