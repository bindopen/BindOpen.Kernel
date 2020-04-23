using BindOpen.Application.Scopes;
using BindOpen.Data.Common;
using BindOpen.System.Diagnostics;
using BindOpen.System.Scripting;
using System;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataItem : ICloneable, IDisposable
    {
        // Check / Update / Repair ------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isExistenceChecked"></param>
        /// <param name="areas"></param>
        /// <returns></returns>
        IBdoLog Check(bool isExistenceChecked = true, string[] areas = null);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="isExistenceChecked"></param>
        /// <param name="item"></param>
        /// <param name="areas"></param>
        /// <returns></returns>
        IBdoLog Check<T>(bool isExistenceChecked = true, T item = default, string[] areas = null) where T : IDataItem;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="areas"></param>
        /// <param name="updateModes"></param>
        /// <returns></returns>
        IBdoLog Repair(string[] areas = null, UpdateModes[] updateModes = null);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="areas"></param>
        /// <param name="updateModes"></param>
        /// <returns></returns>
        IBdoLog Repair<T>(T item = default, string[] areas = null, UpdateModes[] updateModes = null) where T : IDataItem;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="areas"></param>
        /// <param name="updateModes"></param>
        /// <returns></returns>
        IBdoLog Update(string[] areas = null, UpdateModes[] updateModes = null);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="areas"></param>
        /// <param name="updateModes"></param>
        /// <returns></returns>
        IBdoLog Update<T>(T item = default, string[] areas = null, UpdateModes[] updateModes = null) where T : IDataItem;

        // Serialization ----------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log"></param>
        void UpdateRuntimeInfo(IBdoScope scope = null, IScriptVariableSet scriptVariableSet = null, IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        void UpdateStorageInfo(IBdoLog log = null);

        // Clone ------------------------------------

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <param name="areas">The areas to consider.</param>
        /// <returns>Returns a cloned instance.</returns>
        object Clone(params string[] areas);

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <param name="areas">The areas to consider.</param>
        /// <returns>Returns a cloned instance.</returns>
        T Clone<T>(params string[] areas) where T : class;
    }
}