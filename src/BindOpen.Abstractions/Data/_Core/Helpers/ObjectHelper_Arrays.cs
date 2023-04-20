using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data.Helpers
{
    /// <summary>
    /// This class represents a helper for objects.
    /// </summary>
    public static partial class ObjectHelper
    {
        /// <summary>
        /// Returns the string representation of the specified object.
        /// </summary>
        /// <param key="obj">The object to consider.</param>
        /// <param key="index">The index to consider.</param>
        /// <returns></returns>
        public static Q GetAt<Q>(
            this IList<Q> obj,
            int index)
            => obj != null
            && index >= 0
            && index < obj.Count ? obj[index] : default;

        /// <summary>
        /// Gets the string at the specified index from the specified index.
        /// </summary>
        /// <param key="arr">The objects to consider.</param>
        /// <param key="index">The index to consider.</param>
        /// <returns>Returns the normalized string.</returns>
        public static T GetAt<T>(
            this T[] arr,
            int index)
        {
            return arr != null
                && arr.Length > index
                && arr[index] != null ? arr[index] : default;
        }

        /// <summary>
        /// Gets the object at the specified index from the specified index.
        /// </summary>
        /// <param key="objects">The objects to consider.</param>
        /// <param key="index">The index to consider.</param>
        /// <returns>Returns the normalized string.</returns>
        public static string GetString(
            this IList<object> objects,
            int index)
        {
            return objects?.GetAt(index).ToString(DataValueTypes.Any);
        }

        /// <summary>
        /// Gets the object at the specified index from the specified index.
        /// </summary>
        /// <param key="objects">The objects to consider.</param>
        /// <param key="index">The index to consider.</param>
        /// <returns>Returns the normalized string.</returns>
        public static T Get<T>(
            this IList<object> objects,
            int index = 0,
            Func<object, T> converter = null)
        {
            var obj = objects != null && index >= 0 && objects.Count > index && objects[index] != null ?
                objects[index] : default;

            if (converter != null)
            {
                obj = converter.Invoke(obj);
            }

            return obj.As<T>();
        }

        /// <summary>
        /// Gets the object at the specified index from the specified index.
        /// </summary>
        /// <param key="objects">The objects to consider.</param>
        /// <param key="index">The index to consider.</param>
        /// <returns>Returns the normalized string.</returns>
        public static T First<T>(
            this IList<object> objects,
            Func<object, T> converter = null)
        {
            var obj = objects?.FirstOrDefault();

            if (converter != null)
            {
                obj = converter.Invoke(obj);
            }

            return obj.As<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="type"></param>
        /// <returns></returns>
        public static object[] ToObjectArray(this object obj)
            => obj.ToObjectList()?.ToArray();

        /// <summary>
        /// 
        /// </summary>
        /// <param key="type"></param>
        /// <returns></returns>
        public static IList<object> ToObjectList(this object obj)
        {
            IList<object> objList;
            if (obj?.GetType().IsList() == true)
            {
                objList = (obj as IEnumerable).Cast<object>().ToList();
            }
            else
            {
                objList = obj == null ? null : new List<object>() { obj };
            }

            return objList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="type"></param>
        /// <returns></returns>
        public static bool IsList(this object obj)
            => obj?.GetType().IsList() ?? false;

        /// <summary>
        /// 
        /// </summary>
        /// <param key="type"></param>
        /// <returns></returns>
        public static bool IsList(this Type type)
        {
            if (type == null) { return false; }

            if (type == typeof(string)) { return false; }

            if (type == typeof(byte[])) { return false; }

            return typeof(Array).IsAssignableFrom(type)
                || typeof(IEnumerable).IsAssignableFrom(type)
                || type.IsArray;
        }
    }
}
