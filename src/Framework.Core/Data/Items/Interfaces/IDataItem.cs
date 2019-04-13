using System;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Data.Items
{
    public interface IDataItem : ICloneable, IDisposable
    {
        ILog Check(bool isExistenceChecked = true, string[] specificationAreas = null);
        ILog Check<T>(bool isExistenceChecked = true, T item = default, string[] specificationAreas = null) where T : IDataItem;
        ILog Repair(string[] specificationAreas = null, UpdateMode[] updateModes = null);
        ILog Repair<T>(T item = default, string[] specificationAreas = null, UpdateMode[] updateModes = null) where T : IDataItem;

        ILog Update(string[] specificationAreas = null, UpdateMode[] updateModes = null);
        ILog Update<T>(T item = default, string[] specificationAreas = null, UpdateMode[] updateModes = null) where T : IDataItem;

        void UpdateRuntimeInfo(ILog log = null);
        void UpdateStorageInfo(ILog log = null);
    }
}