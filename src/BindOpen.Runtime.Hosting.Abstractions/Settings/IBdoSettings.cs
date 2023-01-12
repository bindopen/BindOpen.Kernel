using BindOpen.Abstractions.Meta.Configuration;
using BindOpen.Meta;
using BindOpen.Meta.Items;
using BindOpen.Runtime.Scopes;
using System;
using System.Runtime.CompilerServices;

namespace BindOpen.Runtime.Settings
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoSettings : IBdoItem, IReferenced, IBdoScoped
    {
        /// <summary>
        /// 
        /// </summary>
        IBdoConfiguration Configuration { get; }

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
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        void SetProperty(object value, [CallerMemberName] string propertyName = null);
    }
}