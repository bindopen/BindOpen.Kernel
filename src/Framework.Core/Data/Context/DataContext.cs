using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Context;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Data.Context
{
    /// <summary>
    /// This class represents a data context. A data context contains all the data related to a user session.
    /// </summary>
    [Serializable()]
    public class DataContext : DataItem, IDataContext
    {
        // --------------------------------------------------
        // VARIABLES
        // --------------------------------------------------

        #region Variables

        public Dictionary<string, object> _singletonItems = new Dictionary<string, object>();
        public Dictionary<string, object> _scopedItems = new Dictionary<string, object>();
        public Dictionary<string, object> _transientItems = new Dictionary<string, object>();

        #endregion

        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// Singletons.
        /// </summary>
        public Dictionary<string, object> SingletonItems { get => _singletonItems; }

        /// <summary>
        /// Scoped items.
        /// </summary>
        public Dictionary<string, object> ScopedItems { get => _scopedItems; }

        /// <summary>
        /// Transient items.
        /// </summary>
        public Dictionary<string, object> TransientItems { get => _transientItems; }

        /// <summary>
        /// ID of this instance.
        /// </summary>
        public string Id { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of DataContext class.
        /// </summary>
        public DataContext()
        {
            this._scopedItems = new Dictionary<string, object>();
            this._singletonItems = new Dictionary<string, object>();
            this._transientItems = new Dictionary<string, object>();
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Initializes the life time service.
        /// </summary>
        /// <returns>Null to remain the object's life forever.</returns>
        public override object InitializeLifetimeService()
        {
            return null;
        }

        // --------------------------------------------------

        /// <summary>
        /// Merges this instance with the specified data context.
        /// </summary>
        /// <param name="dataContext">The data context to consider.</param>
        public void Merge(IDataContext dataContext)
        {
            if (dataContext != null)
            {
                foreach (KeyValuePair<string,object> entry in dataContext.SingletonItems)
                {
                    if (!this._singletonItems.ContainsKey(entry.Key))
                    {
                        this._singletonItems.Add(entry.Key, entry.Value);
                    }
                }

                foreach (KeyValuePair<string, object> entry in dataContext.ScopedItems)
                {
                    if (!this._scopedItems.ContainsKey(entry.Key))
                    {
                        this._scopedItems.Add(entry.Key, entry.Value);
                    }
                }

                foreach (KeyValuePair<string, object> entry in dataContext.TransientItems)
                {
                    if (!this._transientItems.ContainsKey(entry.Key))
                    {
                        this._transientItems.Add(entry.Key, entry.Value);
                    }
                }
            }
        }

        /// <summary>
        /// Clears all the data of this instance.
        /// </summary>
        public void Clear()
        {
            this._singletonItems.Clear();
            this._scopedItems.Clear();
            this._transientItems.Clear();
        }

        // Items ------------------------------------

        /// <summary>
        /// Adds a new item to this instance.
        /// </summary>
        /// <param name="name">Name of the item to add.</param>
        /// <param name="item">Item to add.</param>
        /// <param name="contextSectionName">Name of the context section to consider.</param>
        /// <param name="persistenceLevel">Persistence level of the item to add.</param>
        public void AddItem(
            string name,
            object item,
            string contextSectionName = null,
            PersistenceLevel persistenceLevel = PersistenceLevel.Singleton)
        {
            switch (persistenceLevel)
            {
                case PersistenceLevel.Singleton:
                    this.AddSingletonItem(name, item, contextSectionName);
                    break;
                case PersistenceLevel.Scoped:
                    this.AddScopedItem(name, item, contextSectionName);
                    break;
                case PersistenceLevel.Transient:
                    this.AddTransientItem(name, item, contextSectionName);
                    break;
            }
        }

        /// <summary>
        /// Adds a new system item to this instance.
        /// </summary>
        /// <param name="name">Name of the item to add.</param>
        /// <param name="item">The item to consider.</param>
        public void AddSystemItem(
            string name,
            Object item)
        {
            this.AddSingletonItem(name, item, "#system");
        }

        /// <summary>
        /// Adds a new singleton item to this instance.
        /// </summary>
        /// <param name="name">Name of the item to add.</param>
        /// <param name="item">The item to consider.</param>
        /// <param name="contextSectionName">Name of the context section to consider.</param>
        public void AddSingletonItem(
            string name,
            object item,
            string contextSectionName = null)
        {
            string itemName = DataContext.GetItemUniqueName(name, contextSectionName);

            this._singletonItems.Remove(itemName);
            this._singletonItems.Add(itemName, item);
        }

        /// <summary>
        /// Adds a new scoped item to this instance.
        /// </summary>
        /// <param name="name">Name of the item to add.</param>
        /// <param name="item">The item to consider.</param>
        /// <param name="contextSectionName">Name of the context section to consider.</param>
        public void AddScopedItem(
            string name,
            object item,
            string contextSectionName = null)
        {
            string itemName = DataContext.GetItemUniqueName(name, contextSectionName);

            this._scopedItems.Remove(itemName);
            this._scopedItems.Add(name, item);
        }

        /// <summary>
        /// Adds a new transient item to this instance.
        /// </summary>
        /// <param name="name">Name of the item to add.</param>
        /// <param name="item">The item to consider.</param>
        /// <param name="contextSectionName">Name of the context section to consider.</param>
        public void AddTransientItem(
            string name,
            object item,
            string contextSectionName = null)
        {
            string itemName = DataContext.GetItemUniqueName(name, contextSectionName);

            this._transientItems.Remove(itemName);
            this._transientItems.Add(itemName, item);
        }

        /// <summary>
        /// Clears the specified items of this instance.
        /// </summary>
        /// <param name="persistenceLevel">Persistence level of the item to add.</param>
        public void ClearItems(PersistenceLevel persistenceLevel = PersistenceLevel.Singleton)
        {
            switch (persistenceLevel)
            {
                case PersistenceLevel.Singleton:
                    this._singletonItems.Clear();
                    break;
                case PersistenceLevel.Scoped:
                    this._scopedItems.Clear();
                    break;
                case PersistenceLevel.Transient:
                    this._transientItems.Clear();
                    break;
                case PersistenceLevel.Any:
                    this._singletonItems.Clear();
                    this._scopedItems.Clear();
                    this._transientItems.Clear();
                    break;
            }
        }

        /// <summary>
        /// Removes the singleton items of a specific type.
        /// </summary>
        /// <param name="contextSectionName">Name of the context section to consider.</param>
        /// <param name="persistenceLevel">The persistence level to consider.</param>
        public void RemoveItems(
            string contextSectionName = null,
            PersistenceLevel persistenceLevel = PersistenceLevel.Singleton)
        {
            if ((persistenceLevel == PersistenceLevel.Any) || (persistenceLevel == PersistenceLevel.Singleton))
            {
                var items = this._singletonItems.Keys.Where(p =>
                    (string.IsNullOrEmpty(contextSectionName))
                    || (p?.ToString().ToLower().StartsWith(contextSectionName.ToLower() + "$") == true));
                foreach (string key in items)
                {
                    this._singletonItems.Remove(key);
                }
            }
            if ((persistenceLevel == PersistenceLevel.Any) || (persistenceLevel == PersistenceLevel.Scoped))
            {
                var items = this._scopedItems.Keys.Where(p =>
                    (string.IsNullOrEmpty(contextSectionName))
                    || (p?.ToString().ToLower().StartsWith(contextSectionName.ToLower() + "$") == true));
                foreach (string key in items)
                {
                    this._scopedItems.Remove(key);
                }
            }
            if ((persistenceLevel == PersistenceLevel.Any) || (persistenceLevel == PersistenceLevel.Transient))
            {
                var items = this._transientItems.Keys.Where(p =>
                    (string.IsNullOrEmpty(contextSectionName))
                    || (p?.ToString().ToLower().StartsWith(contextSectionName.ToLower() + "$") == true));
                foreach (string key in items)
                {
                    this._transientItems.Remove(key);
                }
            }
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        // Items ------------------------------------

        private static string GetItemUniqueName(string name, string contextSectionName = null)
        {
            return ((contextSectionName ?? "") + "$" + (name ?? "")).ToLower();
        }

        /// <summary>
        /// Returns a specific system item.
        /// </summary>
        /// <param name="name">Name of the dynamic item to return.</param>
        /// <returns>The dynamic item with specified name and type.</returns>
        public object GetSystemItem(string name)
        {
            return this.GetSingletonItem(name, "#system");
        }

        /// <summary>
        /// Returns a specific singleton item.
        /// </summary>
        /// <param name="name">Name of the dynamic item to return.</param>
        /// <param name="contextSectionName">Name of the context section to consider.</param>
        /// <returns>The dynamic item with specified name and type.</returns>
        public object GetSingletonItem(string name, string contextSectionName = null)
        {
            string itemName = DataContext.GetItemUniqueName(name,contextSectionName);
            return this._singletonItems.ContainsKey(itemName) ? this._singletonItems[itemName] : null;
        }

        /// <summary>
        /// Returns a specific scoped item.
        /// </summary>
        /// <param name="name">Name of the dynamic item to return.</param>
        /// <param name="contextSectionName">Name of the context section to consider.</param>
        /// <returns>The dynamic item with specified name and type.</returns>
        public object GetScopedItem(string name, string contextSectionName = null)
        {
            string itemName = DataContext.GetItemUniqueName(name, contextSectionName);
            return _scopedItems.ContainsKey(itemName) ? this._scopedItems[itemName] : null;
        }

        /// <summary>
        /// Returns a specific transient item.
        /// </summary>
        /// <param name="name">Name of the dynamic item to return.</param>
        /// <param name="contextSectionName">Name of the context section to consider.</param>
        /// <returns>The dynamic item with specified name and type.</returns>
        public object GetTransientItem(string name, string contextSectionName = null)
        {
            string itemName = DataContext.GetItemUniqueName(name, contextSectionName);
            return this._transientItems.ContainsKey(itemName) ? this._transientItems[itemName] : null;
        }

        /// <summary>
        /// Returns a specific item.
        /// </summary>
        /// <param name="name">Name of the dynamic item to return.</param>
        /// <param name="contextSectionName">Name of the context section to consider.</param>
        /// <param name="persistenceLevel">The persistence level to consider.</param>
        /// <returns>The dynamic item with specified name and type.</returns>
        public object GetItem(string name,
            string contextSectionName = null,
            PersistenceLevel persistenceLevel = PersistenceLevel.Any)
        {
            if (persistenceLevel == PersistenceLevel.Any)
            {
                return (this.GetSingletonItem(name, contextSectionName)
                ?? this.GetScopedItem(name, contextSectionName))
                ?? this.GetTransientItem(name, contextSectionName);
            }
            else
            {
                switch (persistenceLevel)
                {
                    case PersistenceLevel.Singleton:
                        return this.GetSingletonItem(name, contextSectionName);
                    case PersistenceLevel.Scoped:
                        return this.GetScopedItem(name, contextSectionName);
                    case PersistenceLevel.Transient:
                        return this.GetTransientItem(name, contextSectionName);
                }
            }

            return null;
        }

        #endregion

        // ------------------------------------------
        // SERIALIZATION
        // ------------------------------------------

        #region Serialization

        /// <summary>
        /// Saves this instance to a file.
        /// </summary>
        /// <param name="filePath">Path of the file to save.</param>
        /// <returns>true if the file has been well saved. false otherwise.</returns>
        public bool Save(string filePath)
        {
            Stream fileStream = null;
            bool isSaved = false;

            try
            {
                fileStream = File.Open(filePath, FileMode.Create);
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fileStream, this);
                isSaved = true;
            }
            catch
            {
                isSaved = false;
            }
            finally
            {
                fileStream?.Close();
            }
            return isSaved;
        }

        /// <summary>
        /// Loads a data context from file.
        /// </summary>
        /// <param name="filePath">The path of the file to load.</param>
        /// <param name="log">The log that receives the log of this loading task.</param>
        /// <returns>Returns the data context loaded from the specified file.</returns>
        public static DataContext Load(string filePath, ref Log log)
        {
            DataContext dataContext = null;
            Stream fileStream = null;
            try
            {
                fileStream = File.Open(filePath, FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                dataContext = (DataContext)formatter.Deserialize(fileStream);
            }
            catch (Exception exception)
            {
                log.AddException(exception);
            }
            finally
            {
                fileStream?.Close();
            }

            return dataContext;
        }

        #endregion
    }
}
