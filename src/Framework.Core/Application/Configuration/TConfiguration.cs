using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Elements.Factories;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Attributes;

namespace BindOpen.Framework.Core.Application.Configuration
{
    /// <summary>
    /// This class represents a configuration.
    /// </summary>
    public class TConfiguration<T> : DataItem, ITConfiguration<T> where T : IConfigurationDto
    {
        private T _dto;

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The item of this instance.
        /// </summary>
        public T Dto { get => _dto; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the Configuration class.
        /// </summary>
        public TConfiguration()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the Configuration class.
        /// </summary>
        /// <param name="dto">The item to consider.</param>
        public TConfiguration(T dto)
        {
            _dto = dto;
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        ///// <summary>
        ///// Sets the specified value.
        ///// </summary>
        ///// <param name="value">The value to set.</param>
        ///// <param name="propertyName">The calling property name to consider.</param>
        //public void Set(object value, [CallerMemberName] string propertyName = null)
        //{
        //    if (propertyName != null)
        //    {
        //        DataElementAttribute attribute = null;
        //        PropertyInfo propertyInfo = GetType().GetPropertyInfo(
        //            propertyName,
        //            new Type[] { typeof(DetailPropertyAttribute) },
        //            out attribute);

        //        if (attribute is DetailPropertyAttribute)
        //        {
        //            Dto?.AddElement(ElementFactory.CreateScalar(attribute.Name, propertyInfo.PropertyType.GetValueType(), value));
        //        }
        //    }
        //}

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Returns the key of this instance.
        /// </summary>
        /// <returns></returns>
        public string Key()
        {
            return _dto?.Name;
        }

        ///// <summary>
        ///// Gets the specified value.
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="propertyName">The calling property name to consider.</param>
        //public T Get<T>([CallerMemberName] string propertyName = null)
        //{
        //    if (Dto == null) return default;

        //    if (propertyName != null)
        //    {
        //        IDataElement element = Dto.GetItem(propertyName);
        //        if (element != null)
        //        {
        //            return (T)Dto.GetElementObject(propertyName, _appScope);
        //        }
        //        else
        //        {
        //            DataElementAttribute attribute = null;
        //            PropertyInfo propertyInfo = GetType().GetPropertyInfo(
        //                propertyName,
        //                new Type[] { typeof(DetailPropertyAttribute) },
        //                out attribute);

        //            if (attribute is DetailPropertyAttribute)
        //            {
        //                Object value = Dto.GetElementObject(attribute.Name, _appScope);
        //                if (value is T t)
        //                    return t;
        //            }
        //        }
        //    }

        //    return default(T);
        //}

        ///// <summary>
        ///// Gets the specified value.
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="propertyName">The calling property name to consider.</param>
        ///// <param name="defaultValue">The default value to consider.</param>
        //public T Get<T>(T defaultValue, [CallerMemberName] string propertyName = null) where T : struct, IConvertible
        //{
        //    if (Dto == null) return default;

        //    if (propertyName != null)
        //    {
        //        IDataElement element = Dto.GetItem(propertyName);
        //        if (element != null)
        //        {
        //            return (T)Dto.GetElementObject(propertyName, _appScope);
        //        }
        //        else
        //        {
        //            DataElementAttribute attribute = null;
        //            PropertyInfo propertyInfo = GetType().GetPropertyInfo(
        //                propertyName,
        //                new Type[] { typeof(DetailPropertyAttribute) },
        //                out attribute);

        //            if (attribute is DetailPropertyAttribute)
        //                return (Dto.GetElementObject(attribute.Name, _appScope) as string)?.ToEnum<T>(defaultValue) ?? default(T); ;
        //        }
        //    }

        //    return default;
        //}

        #endregion
    }
}
