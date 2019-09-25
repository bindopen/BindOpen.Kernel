using System;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Data.Items
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataItem : ICloneable, IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isExistenceChecked"></param>
        /// <param name="specificationAreas"></param>
        /// <returns></returns>
        ILog Check(bool isExistenceChecked = true, string[] specificationAreas = null);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="isExistenceChecked"></param>
        /// <param name="item"></param>
        /// <param name="specificationAreas"></param>
        /// <returns></returns>
        ILog Check<T>(bool isExistenceChecked = true, T item = default, string[] specificationAreas = null) where T : IDataItem;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="specificationAreas"></param>
        /// <param name="updateModes"></param>
        /// <returns></returns>
        ILog Repair(string[] specificationAreas = null, UpdateModes[] updateModes = null);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="specificationAreas"></param>
        /// <param name="updateModes"></param>
        /// <returns></returns>
        ILog Repair<T>(T item = default, string[] specificationAreas = null, UpdateModes[] updateModes = null) where T : IDataItem;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="specificationAreas"></param>
        /// <param name="updateModes"></param>
        /// <returns></returns>
        ILog Update(string[] specificationAreas = null, UpdateModes[] updateModes = null);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="specificationAreas"></param>
        /// <param name="updateModes"></param>
        /// <returns></returns>
        ILog Update<T>(T item = default, string[] specificationAreas = null, UpdateModes[] updateModes = null) where T : IDataItem;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appScope"></param>
        /// <param name="scriptVariableSet"></param>
        /// <param name="log"></param>
        void UpdateRuntimeInfo(IAppScope appScope = null, IScriptVariableSet scriptVariableSet = null, ILog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        void UpdateStorageInfo(ILog log = null);
    }
}