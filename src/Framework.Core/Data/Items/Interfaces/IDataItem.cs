using System;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Data.Items
{
    public interface IDataItem : ICloneable, IDisposable
    {
        ILog Check(bool isExistenceChecked = true, string[] specificationAreas = null);
        ILog Check<T>(bool isExistenceChecked = true, T item = default, string[] specificationAreas = null) where T : IDataItem;
        ILog Repair(string[] specificationAreas = null, UpdateModes[] updateModes = null);
        ILog Repair<T>(T item = default, string[] specificationAreas = null, UpdateModes[] updateModes = null) where T : IDataItem;

        ILog Update(string[] specificationAreas = null, UpdateModes[] updateModes = null);
        ILog Update<T>(T item = default, string[] specificationAreas = null, UpdateModes[] updateModes = null) where T : IDataItem;

        void UpdateRuntimeInfo(IAppScope appScope = null, IScriptVariableSet scriptVariableSet = null, ILog log = null);
        void UpdateStorageInfo(ILog log = null);
    }
}