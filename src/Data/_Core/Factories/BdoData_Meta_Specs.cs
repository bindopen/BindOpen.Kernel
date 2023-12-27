using BindOpen.Data.Meta;
using BindOpen.Data.Meta.Reflection;
using System;
using System.Linq;

namespace BindOpen.Data
{
    /// <summary>
    /// This static class provides methods to create element specifications.
    /// </summary>
    public static partial class BdoData
    {
        // NewSpec

        /// <summary>
        /// Creates a data element of the specified kind.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="valueType">The value type to consider.</param>
        public static BdoSpec NewSpec(
            string name = null,
            DataValueTypes valueType = DataValueTypes.Any,
            object defaultData = null)
            => NewSpec<BdoSpec>(name, valueType, defaultData);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="name"></param>
        /// <param key="type"></param>
        /// <returns></returns>
        public static BdoSpec NewSpec(string name, Type type)
            => NewSpec<BdoSpec>(name, type);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="name"></param>
        /// <param key="type"></param>
        /// <returns></returns>
        public static BdoSpec NewSpec(Type type)
            => NewSpec<BdoSpec>(null, type);

        // NewSpec<T>

        /// <summary>
        /// Creates a data element of the specified kind.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="valueType">The value type to consider.</param>
        public static T NewSpec<T>(
            string name = null,
            DataValueTypes valueType = DataValueTypes.Any,
            object defaultData = null)
            where T : IBdoSpec, new()
        {
            var spec = new T();
            spec.WithName(name)
                .WithDataType(valueType)
                .WithDefaultData(defaultData);

            return spec;
        }

        public static T NewSpec<T>(
            string name,
            Type type)
            where T : IBdoSpec, new()
        {
            if (type == null) return default;

            var spec = NewSpec<T>(name)
                .WithDataType(type.GetValueType())
                .AsType(type);

            return spec;
        }

        /// <summary>
        /// Creates a data element of the specified kind.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="type">The value type to consider.</param>
        public static T NewSpec<T>(
            Type type)
            where T : IBdoSpec, new()
            => NewSpec<T>(null, type);

        // AsType

        /// <summary>
        /// Creates a data element of the specified kind.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="type">The value type to consider.</param>
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
                    //spec.Add(
                    //    NewSpecRule(
                    //        nameof(BdoMetaSpecRuleGroupIds.ItemMustBeInList),
                    //        NewCondition("$eq($item, true)"),
                    //        BdoMetaSpecRuleResultCodes.BadItem));
                }
            }
            return spec;
        }

        // with children

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="specs">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoSpec NewSpec(
            string name,
            params IBdoNodeSpec[] specs)
        {
            var list = NewSpec<BdoSpec>()
                .WithChildren(specs)
                .WithName(name);

            return list;
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="specs">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoSpec NewSpec(
            params IBdoNodeSpec[] specs)
            => NewSpec<BdoSpec, IBdoNodeSpec>(specs);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoSpec NewSpec(
            string name,
            params (string Name, DataValueTypes ValueType)[] pairs)
        {
            var spec = NewSpec<BdoSpec, IBdoNodeSpec>(
                pairs.Select(q => NewSpec<BdoSpec>(q.Name, q.ValueType)).ToArray())
                .WithName(name);

            return spec;
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoSpec NewSpec(
            params (string Name, DataValueTypes ValueType)[] pairs)
            => NewSpec<BdoSpec>(pairs);

        // Static T creators -------------------------
        // with children

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="specs">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewSpec<T>(
            string name,
            params IBdoNodeSpec[] specs)
            where T : IBdoNodeSpec, new()
            => NewSpec<T, IBdoNodeSpec>(name, specs);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="specs">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewSpec<T>(
            params IBdoNodeSpec[] specs)
            where T : IBdoNodeSpec, new()
            => NewSpec<T>(null, specs);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewSpec<T>(
            string name,
            params (string Name, DataValueTypes ValueType)[] pairs)
            where T : IBdoNodeSpec, new()
        {
            var spec = NewSpec<T>()
                .WithChildren(pairs.Select(q => NewSpec<BdoSpec>(q.Name, q.ValueType)).Cast<IBdoNodeSpec>().ToArray())
                .WithName(name);

            return spec;
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewSpec<T>(
            params (string Name, DataValueTypes ValueType)[] pairs)
            where T : IBdoNodeSpec, new()
            => NewSpec<T>(null, pairs);

        // Static TParent, TChild creators -------------------------
        // with children

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="specs">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static TParent NewSpec<TParent, TChild>(
            string name,
            params TChild[] specs)
            where TParent : ITBdoNodeSpec<TChild>, new()
            where TChild : IBdoSpec
        {
            var spec = NewSpec<TParent>()
                .WithChildren(specs.ToArray())
                .WithName(name);

            return spec;
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="specs">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static TParent NewSpec<TParent, TChild>(
            params TChild[] specs)
            where TParent : ITBdoNodeSpec<TChild>, new()
            where TChild : IBdoSpec
            => NewSpec<TParent, TChild>(null, specs);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static TParent NewSpec<TParent, TChild>(
            string name,
            params (string Name, DataValueTypes ValueType)[] pairs)
            where TParent : ITBdoNodeSpec<TChild>, new()
            where TChild : IBdoSpec, new()
        {
            var spec = NewSpec<TParent, TChild>(
                pairs.Select(q => NewSpec<TChild>(q.Name, q.ValueType)).ToArray())
                .WithName(name);

            return spec;
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static TParent NewSpec<TParent, TChild>(
            params (string Name, DataValueTypes ValueType)[] pairs)
            where TParent : ITBdoNodeSpec<TChild>, new()
            where TChild : IBdoSpec, new()
            => NewSpec<TParent, TChild>(null, pairs);

        // From

        /// <summary>
        /// Creates a meta data of the specified object.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static Q NewSpecFrom<T, Q>(
            string name = null,
            bool onlyMetaAttributes = true)
            where Q : IBdoNodeSpec, new()
            => typeof(T).ToSpec<Q>(name, onlyMetaAttributes);

        /// <summary>
        /// Creates a meta data of the specified object.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static IBdoNodeSpec NewSpecFrom<T>(
            string name = null,
            bool onlyMetaAttributes = true)
            => typeof(T).ToSpec(name, onlyMetaAttributes);
    }
}
