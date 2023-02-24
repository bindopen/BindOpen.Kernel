using BindOpen.Data.Items;
using System.Collections.Generic;

namespace BindOpen.Data.Context
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoDataContext : IBdoItem
    {
        /// <summary>
        /// 
        /// </summary>
        Dictionary<string, object> SingletonItems { get; }

        /// <summary>
        /// 
        /// </summary>
        Dictionary<string, object> ScopedItems { get; }

        /// <summary>
        /// 
        /// </summary>
        Dictionary<string, object> TransientItems { get; }

        /// <summary>
        /// 
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="name"></param>
        /// <param key="item"></param>
        /// <param key="contextSectionName"></param>
        /// <param key="persistenceLevel"></param>
        IBdoDataContext AddItem(string name, object item, string contextSectionName = null, PersistenceLevels persistenceLevel = PersistenceLevels.Singleton);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="name"></param>
        /// <param key="item"></param>
        /// <param key="contextSectionName"></param>
        IBdoDataContext AddScopedItem(string name, object item, string contextSectionName = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="name"></param>
        /// <param key="item"></param>
        /// <param key="contextSectionName"></param>
        IBdoDataContext AddSingletonItem(string name, object item, string contextSectionName = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="name"></param>
        /// <param key="item"></param>
        IBdoDataContext AddSystemItem(string name, object item);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="name"></param>
        /// <param key="item"></param>
        /// <param key="contextSectionName"></param>
        IBdoDataContext AddTransientItem(string name, object item, string contextSectionName = null);

        /// <summary>
        /// 
        /// </summary>
        IBdoDataContext Clear();

        /// <summary>
        /// 
        /// </summary>
        /// <param key="persistenceLevel"></param>
        IBdoDataContext ClearItems(PersistenceLevels persistenceLevel = PersistenceLevels.Singleton);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="name"></param>
        /// <param key="contextSectionName"></param>
        /// <param key="persistenceLevel"></param>
        /// <returns></returns>
        object GetItem(string name, string contextSectionName = null, PersistenceLevels persistenceLevel = PersistenceLevels.Any);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="name"></param>
        /// <param key="contextSectionName"></param>
        /// <returns></returns>
        object GetScopedItem(string name, string contextSectionName = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="name"></param>
        /// <param key="contextSectionName"></param>
        /// <returns></returns>
        object GetSingletonItem(string name, string contextSectionName = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="name"></param>
        /// <returns></returns>
        object GetSystemItem(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="name"></param>
        /// <param key="contextSectionName"></param>
        /// <returns></returns>
        object GetTransientItem(string name, string contextSectionName = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="dataContext"></param>
        IBdoDataContext Merge(IBdoDataContext dataContext);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="contextSectionName"></param>
        /// <param key="persistenceLevel"></param>
        IBdoDataContext RemoveItems(string contextSectionName = null, PersistenceLevels persistenceLevel = PersistenceLevels.Singleton);
    }
}