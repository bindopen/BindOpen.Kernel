using BindOpen.Meta.Items;
using System.Collections.Generic;

namespace BindOpen.Meta.Context
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
        /// <param name="name"></param>
        /// <param name="item"></param>
        /// <param name="contextSectionName"></param>
        /// <param name="persistenceLevel"></param>
        void AddItem(string name, object item, string contextSectionName = null, PersistenceLevels persistenceLevel = PersistenceLevels.Singleton);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="item"></param>
        /// <param name="contextSectionName"></param>
        void AddScopedItem(string name, object item, string contextSectionName = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="item"></param>
        /// <param name="contextSectionName"></param>
        void AddSingletonItem(string name, object item, string contextSectionName = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="item"></param>
        void AddSystemItem(string name, object item);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="item"></param>
        /// <param name="contextSectionName"></param>
        void AddTransientItem(string name, object item, string contextSectionName = null);

        /// <summary>
        /// 
        /// </summary>
        void Clear();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="persistenceLevel"></param>
        void ClearItems(PersistenceLevels persistenceLevel = PersistenceLevels.Singleton);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="contextSectionName"></param>
        /// <param name="persistenceLevel"></param>
        /// <returns></returns>
        object GetItem(string name, string contextSectionName = null, PersistenceLevels persistenceLevel = PersistenceLevels.Any);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="contextSectionName"></param>
        /// <returns></returns>
        object GetScopedItem(string name, string contextSectionName = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="contextSectionName"></param>
        /// <returns></returns>
        object GetSingletonItem(string name, string contextSectionName = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        object GetSystemItem(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="contextSectionName"></param>
        /// <returns></returns>
        object GetTransientItem(string name, string contextSectionName = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataContext"></param>
        void Merge(IBdoDataContext dataContext);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contextSectionName"></param>
        /// <param name="persistenceLevel"></param>
        void RemoveItems(string contextSectionName = null, PersistenceLevels persistenceLevel = PersistenceLevels.Singleton);
    }
}