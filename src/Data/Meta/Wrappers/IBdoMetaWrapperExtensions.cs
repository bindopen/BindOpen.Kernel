using BindOpen.Data.Helpers;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a data element.
    /// </summary>
    public static class IBdoMetaWrapperExtensions
    {
        /// <summary>
        /// Gets the specified value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param key="propertyName">The calling property name to consider.</param>
        public static object GetData<T>(this T obj, Expression<Func<T, string>> expr)
            where T : IBdoMetaWrapper
        {
            if (obj?.Detail == null) return default;

            var outExpr = (MemberExpression)expr.Body;
            var propInfo = (PropertyInfo)outExpr.Member;

            IBdoSpec spec = BdoData.NewSpec();
            spec.UpdateFrom<BdoPropertyAttribute>(propInfo);

            var propName = spec.Name ?? propInfo.Name;

            if (propName != null
                && propName != nameof(IBdoMetaWrapper.Scope)
                && propName != nameof(IBdoMetaWrapper.Detail))
            {
                var meta = obj.Detail[propName];
                if (meta != null)
                {
                    return meta.GetData(obj.Scope);
                }
                else
                {
                    return propInfo?.GetValue(obj);
                }
            }

            return default;
        }

        /// <summary>
        /// Gets the specified value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param key="defaultValue">The default value to consider.</param>
        /// <param key="propertyName">The calling property name to consider.</param>
        public static Q GetProperty<T, Q>(this T obj, Expression<Func<T, string>> expr, Q defaultValue)
            where T : IBdoMetaWrapper
            where Q : struct, IConvertible
        {
            if (obj?.Detail == null) return default;

            var outExpr = (MemberExpression)expr.Body;
            var propInfo = (PropertyInfo)outExpr.Member;

            IBdoSpec spec = BdoData.NewSpec();
            spec.UpdateFrom<BdoPropertyAttribute>(propInfo);

            var propName = spec.Name ?? propInfo.Name;

            if (propName != null
                && propName != nameof(IBdoMetaWrapper.Scope)
                && propName != nameof(IBdoMetaWrapper.Detail))
            {
                var meta = obj.Detail[propName];
                if (meta != null)
                {
                    return meta.GetData<string>(obj.Scope)?.ToEnum(defaultValue) ?? defaultValue;
                }
                else
                {
                    return propInfo?.GetValue(obj).As<Q>() ?? defaultValue;
                }
            }

            return default;
        }

        /// <summary>
        /// Sets the specified value.
        /// </summary>
        /// <param key="value">The value to set.</param>
        /// <param key="propertyName">The calling property name to consider.</param>
        public static void SetProperty<T, TResult>(this T obj, Expression<Func<T, TResult>> expr, TResult value)
            where T : IBdoMetaWrapper
        {
            if (obj?.Detail == null) return;

            var outExpr = (MemberExpression)expr.Body;
            var propInfo = (PropertyInfo)outExpr.Member;

            IBdoSpec spec = BdoData.NewSpec();
            spec.UpdateFrom<BdoPropertyAttribute>(propInfo);

            var propName = spec.Name ?? propInfo.Name;

            if (propName != null
                && propName != nameof(IBdoMetaWrapper.Scope)
                && propName != nameof(IBdoMetaWrapper.Detail))
            {
                var meta = obj.Detail[propName];
                if (meta != null)
                {
                    meta.WithData(value);
                }
                else
                {
                    obj.Detail.Add((propName, spec.DataType?.ValueType ?? DataValueTypes.Any, value));
                }

                propInfo?.SetValue(obj, value);
            }
        }

    }
}
