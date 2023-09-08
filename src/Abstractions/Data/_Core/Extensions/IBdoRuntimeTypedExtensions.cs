using System;

namespace BindOpen.Kernel.Data
{
    /// <summary>
    /// This interface represents an named data item.
    /// </summary>
    public static class IBdoRuntimeTypedExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="runtimeType"></param>
        public static T WithRuntimeType<T>(
            this T obj,
            Type runtimeType)
            where T : IBdoRuntimeTyped
        {
            if (obj != null)
            {
                obj.RuntimeType = runtimeType;
            }

            return obj;
        }
    }
}