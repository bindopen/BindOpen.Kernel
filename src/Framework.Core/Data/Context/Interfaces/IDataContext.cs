using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Items;

namespace BindOpen.Framework.Core.Data.Context
{
    public interface IDataContext : IDataItem
    {
        Dictionary<string, object> SingletonItems { get; }
        Dictionary<string, object> ScopedItems { get; }
        Dictionary<string, object> TransientItems { get; }

        string Id { get; set; }

        void AddItem(string name, object item, string contextSectionName = null, PersistenceLevel persistenceLevel = PersistenceLevel.Singleton);
        void AddScopedItem(string name, object item, string contextSectionName = null);
        void AddSingletonItem(string name, object item, string contextSectionName = null);
        void AddSystemItem(string name, object item);
        void AddTransientItem(string name, object item, string contextSectionName = null);
        void Clear();
        void ClearItems(PersistenceLevel persistenceLevel = PersistenceLevel.Singleton);
        object GetItem(string name, string contextSectionName = null, PersistenceLevel persistenceLevel = PersistenceLevel.Any);
        object GetScopedItem(string name, string contextSectionName = null);
        object GetSingletonItem(string name, string contextSectionName = null);
        object GetSystemItem(string name);
        object GetTransientItem(string name, string contextSectionName = null);
        void Merge(IDataContext dataContext);
        void RemoveItems(string contextSectionName = null, PersistenceLevel persistenceLevel = PersistenceLevel.Singleton);
        bool Save(string filePath);
    }
}