using BindOpen.Framework.Core.Application.Configuration;
using System;
using System.Runtime.CompilerServices;

namespace BindOpen.Framework.Runtime.Application.Settings
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="Q"></typeparam>
    public interface ITBdoSettings<Q> : IBdoSettings
        where Q : IBdoBaseConfiguration, new()
    {
        /// <summary>
        /// 
        /// </summary>
        Q Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        T Get<T>(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        object Get(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        T GetProperty<T>([CallerMemberName] string propertyName = null);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="defaultValue"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        T GetProperty<T>(T defaultValue, [CallerMemberName] string propertyName = null)
            where T : struct, IConvertible;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        void Set(string name, object value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        void SetProperty(object value, [CallerMemberName] string propertyName = null);
    }
}