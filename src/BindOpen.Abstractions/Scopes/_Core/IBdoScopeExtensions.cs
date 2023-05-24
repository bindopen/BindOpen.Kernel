﻿using BindOpen.Data;

namespace BindOpen.Scopes
{
    /// <summary>
    /// This static class provides methods to create extension items.
    /// </summary>
    public static class IBdoScopeExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="assemblyFileName"></param>
        public static bool IsScope(this BdoDataType dataType)
        {
            return dataType >= typeof(IBdoScope);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="execution"></param>
        /// <returns></returns>
        public static T WithScope<T>(
            this T scoped,
            IBdoScope scope)
            where T : IBdoScoped
        {
            if (scoped != null)
            {
                scoped.Scope = scope;
            }

            return scoped;
        }

        public static T CreateScoped<T>(
            this IBdoScope scope)
            where T : IBdoScoped, new()
        {
            T scoped = default;

            if (scope != null)
            {
                scoped = new()
                {
                    Scope = scope
                };
            }

            return scoped;
        }
    }
}
