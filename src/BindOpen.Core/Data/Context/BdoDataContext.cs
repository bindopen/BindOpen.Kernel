using BindOpen.Data.Common;
using BindOpen.Data.Items;
using BindOpen.System.Diagnostics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace BindOpen.Data.Context
{
    /// <summary>
    /// This class represents a data context. A data context contains all the data related to a user session.
    /// </summary>
    public class BdoDataContext : DataItem, IBdoDataContext
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// Singletons.
        /// </summary>
        public Dictionary<string, object> SingletonItems { get; } = new Dictionary<string, object>();

        /// <summary>
        /// Scoped items.
        /// </summary>
        public Dictionary<string, object> ScopedItems { get; } = new Dictionary<string, object>();

        /// <summary>
        /// Transient items.
        /// </summary>
        public Dictionary<string, object> TransientItems { get; } = new Dictionary<string, object>();

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
        public BdoDataContext()
        {
            ScopedItems = new Dictionary<string, object>();
            SingletonItems = new Dictionary<string, object>();
            TransientItems = new Dictionary<string, object>();
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
        public void Merge(IBdoDataContext dataContext)
        {
            if (dataContext != null)
            {
                foreach (KeyValuePair<string, object> entry in dataContext.SingletonItems)
                {
                    if (!SingletonItems.ContainsKey(entry.Key))
                    {
                        SingletonItems.Add(entry.Key, entry.Value);
                    }
                }

                foreach (KeyValuePair<string, object> entry in dataContext.ScopedItems)
                {
                    if (!ScopedItems.ContainsKey(entry.Key))
                    {
                        ScopedItems.Add(entry.Key, entry.Value);
                    }
                }

                foreach (KeyValuePair<string, object> entry in dataContext.TransientItems)
                {
                    if (!TransientItems.ContainsKey(entry.Key))
                    {
                        TransientItems.Add(entry.Key, entry.Value);
                    }
                }
            }
        }

        /// <summary>
        /// Clears all the data of this instance.
        /// </summary>
        public void Clear()
        {
            SingletonItems.Clear();
            ScopedItems.Clear();
            TransientItems.Clear();
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
            PersistenceLevels persistenceLevel = PersistenceLevels.Singleton)
        {
            switch (persistenceLevel)
            {
                case PersistenceLevels.Singleton:
                    AddSingletonItem(name, item, contextSectionName);
                    break;
                case PersistenceLevels.Scoped:
                    AddScopedItem(name, item, contextSectionName);
                    break;
                case PersistenceLevels.Transient:
                    AddTransientItem(name, item, contextSectionName);
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
            AddSingletonItem(name, item, "#system");
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
            string itemName = BdoDataContext.GetItemUniqueName(name, contextSectionName);

            SingletonItems.Remove(itemName);
            SingletonItems.Add(itemName, item);
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
            string itemName = BdoDataContext.GetItemUniqueName(name, contextSectionName);

            ScopedItems.Remove(itemName);
            ScopedItems.Add(name, item);
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
            string itemName = BdoDataContext.GetItemUniqueName(name, contextSectionName);

            TransientItems.Remove(itemName);
            TransientItems.Add(itemName, item);
        }

        /// <summary>
        /// Clears the specified items of this instance.
        /// </summary>
        /// <param name="persistenceLevel">Persistence level of the item to add.</param>
        public void ClearItems(PersistenceLevels persistenceLevel = PersistenceLevels.Singleton)
        {
            switch (persistenceLevel)
            {
                case PersistenceLevels.Singleton:
                    SingletonItems.Clear();
                    break;
                case PersistenceLevels.Scoped:
                    ScopedItems.Clear();
                    break;
                case PersistenceLevels.Transient:
                    TransientItems.Clear();
                    break;
                case PersistenceLevels.Any:
                    SingletonItems.Clear();
                    ScopedItems.Clear();
                    TransientItems.Clear();
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
            PersistenceLevels persistenceLevel = PersistenceLevels.Singleton)
        {
            if ((persistenceLevel == PersistenceLevels.Any) || (persistenceLevel == PersistenceLevels.Singleton))
            {
                var items = SingletonItems.Keys.Where(p =>
                    (string.IsNullOrEmpty(contextSectionName))
                    || (p?.ToString().ToLower().StartsWith(contextSectionName.ToLower() + "$") == true));
                foreach (string key in items)
                {
                    SingletonItems.Remove(key);
                }
            }
            if ((persistenceLevel == PersistenceLevels.Any) || (persistenceLevel == PersistenceLevels.Scoped))
            {
                var items = ScopedItems.Keys.Where(p =>
                    (string.IsNullOrEmpty(contextSectionName))
                    || (p?.ToString().ToLower().StartsWith(contextSectionName.ToLower() + "$") == true));
                foreach (string key in items)
                {
                    ScopedItems.Remove(key);
                }
            }
            if ((persistenceLevel == PersistenceLevels.Any) || (persistenceLevel == PersistenceLevels.Transient))
            {
                var items = TransientItems.Keys.Where(p =>
                    (string.IsNullOrEmpty(contextSectionName))
                    || (p?.ToString().ToLower().StartsWith(contextSectionName.ToLower() + "$") == true));
                foreach (string key in items)
                {
                    TransientItems.Remove(key);
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
            return ((contextSectionName ?? string.Empty) + "$" + (name ?? string.Empty)).ToLower();
        }

        /// <summary>
        /// Returns a specific system item.
        /// </summary>
        /// <param name="name">Name of the dynamic item to return.</param>
        /// <returns>The dynamic item with specified name and type.</returns>
        public object GetSystemItem(string name)
        {
            return GetSingletonItem(name, "#system");
        }

        /// <summary>
        /// Returns a specific singleton item.
        /// </summary>
        /// <param name="name">Name of the dynamic item to return.</param>
        /// <param name="contextSectionName">Name of the context section to consider.</param>
        /// <returns>The dynamic item with specified name and type.</returns>
        public object GetSingletonItem(string name, string contextSectionName = null)
        {
            string itemName = BdoDataContext.GetItemUniqueName(name, contextSectionName);
            return SingletonItems.ContainsKey(itemName) ? SingletonItems[itemName] : null;
        }

        /// <summary>
        /// Returns a specific scoped item.
        /// </summary>
        /// <param name="name">Name of the dynamic item to return.</param>
        /// <param name="contextSectionName">Name of the context section to consider.</param>
        /// <returns>The dynamic item with specified name and type.</returns>
        public object GetScopedItem(string name, string contextSectionName = null)
        {
            string itemName = BdoDataContext.GetItemUniqueName(name, contextSectionName);
            return ScopedItems.ContainsKey(itemName) ? ScopedItems[itemName] : null;
        }

        /// <summary>
        /// Returns a specific transient item.
        /// </summary>
        /// <param name="name">Name of the dynamic item to return.</param>
        /// <param name="contextSectionName">Name of the context section to consider.</param>
        /// <returns>The dynamic item with specified name and type.</returns>
        public object GetTransientItem(string name, string contextSectionName = null)
        {
            string itemName = BdoDataContext.GetItemUniqueName(name, contextSectionName);
            return TransientItems.ContainsKey(itemName) ? TransientItems[itemName] : null;
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
            PersistenceLevels persistenceLevel = PersistenceLevels.Any)
        {
            if (persistenceLevel == PersistenceLevels.Any)
            {
                return (GetSingletonItem(name, contextSectionName)
                ?? GetScopedItem(name, contextSectionName))
                ?? GetTransientItem(name, contextSectionName);
            }
            else
            {
                switch (persistenceLevel)
                {
                    case PersistenceLevels.Singleton:
                        return GetSingletonItem(name, contextSectionName);
                    case PersistenceLevels.Scoped:
                        return GetScopedItem(name, contextSectionName);
                    case PersistenceLevels.Transient:
                        return GetTransientItem(name, contextSectionName);
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
        public static BdoDataContext Load(string filePath, ref BdoLog log)
        {
            BdoDataContext dataContext = null;
            Stream fileStream = null;
            try
            {
                fileStream = File.Open(filePath, FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                dataContext = (BdoDataContext)formatter.Deserialize(fileStream);
            }
            catch (IOException exception)
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
