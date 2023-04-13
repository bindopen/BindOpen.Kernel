using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data.Context
{
    /// <summary>
    /// This class represents a data context. A data context contains all the data related to a user session.
    /// </summary>
    public class BdoDataContext : BdoObject, IBdoDataContext
    {
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
        // IBdoDataContext Implementation
        // ------------------------------------------

        #region IBdoDataContext

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

        // --------------------------------------------------

        /// <summary>
        /// Merges this instance with the specified data context.
        /// </summary>
        /// <param key="dataContext">The data context to consider.</param>
        public IBdoDataContext Merge(IBdoDataContext dataContext)
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

            return this;
        }

        /// <summary>
        /// Clears all the data of this instance.
        /// </summary>
        public IBdoDataContext Clear()
        {
            SingletonItems.Clear();
            ScopedItems.Clear();
            TransientItems.Clear();

            return this;
        }

        // Items ------------------------------------

        /// <summary>
        /// Adds a new item to this instance.
        /// </summary>
        /// <param key="name">Name of the item to add.</param>
        /// <param key="item">Item to add.</param>
        /// <param key="contextSectionName">Name of the context section to consider.</param>
        /// <param key="persistenceLevel">Persistence level of the item to add.</param>
        public IBdoDataContext AddItem(
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

            return this;
        }

        /// <summary>
        /// Adds a new system item to this instance.
        /// </summary>
        /// <param key="name">Name of the item to add.</param>
        /// <param key="item">The item to consider.</param>
        public IBdoDataContext AddSystemItem(
            string name,
            object item)
        {
            AddSingletonItem(name, item, "#system");

            return this;
        }

        /// <summary>
        /// Adds a new singleton item to this instance.
        /// </summary>
        /// <param key="name">Name of the item to add.</param>
        /// <param key="item">The item to consider.</param>
        /// <param key="contextSectionName">Name of the context section to consider.</param>
        public IBdoDataContext AddSingletonItem(
            string name,
            object item,
            string contextSectionName = null)
        {
            string itemName = BdoDataContext.GetItemUniqueName(name, contextSectionName);

            SingletonItems.Remove(itemName);
            SingletonItems.Add(itemName, item);

            return this;
        }

        /// <summary>
        /// Adds a new scoped item to this instance.
        /// </summary>
        /// <param key="name">Name of the item to add.</param>
        /// <param key="item">The item to consider.</param>
        /// <param key="contextSectionName">Name of the context section to consider.</param>
        public IBdoDataContext AddScopedItem(
            string name,
            object item,
            string contextSectionName = null)
        {
            string itemName = BdoDataContext.GetItemUniqueName(name, contextSectionName);

            ScopedItems.Remove(itemName);
            ScopedItems.Add(name, item);

            return this;
        }

        /// <summary>
        /// Adds a new transient item to this instance.
        /// </summary>
        /// <param key="name">Name of the item to add.</param>
        /// <param key="item">The item to consider.</param>
        /// <param key="contextSectionName">Name of the context section to consider.</param>
        public IBdoDataContext AddTransientItem(
            string name,
            object item,
            string contextSectionName = null)
        {
            string itemName = BdoDataContext.GetItemUniqueName(name, contextSectionName);

            TransientItems.Remove(itemName);
            TransientItems.Add(itemName, item);

            return this;
        }

        /// <summary>
        /// Clears the specified items of this instance.
        /// </summary>
        /// <param key="persistenceLevel">Persistence level of the item to add.</param>
        public IBdoDataContext ClearItems(PersistenceLevels persistenceLevel = PersistenceLevels.Singleton)
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

            return this;
        }

        /// <summary>
        /// Removes the singleton items of a specific type.
        /// </summary>
        /// <param key="contextSectionName">Name of the context section to consider.</param>
        /// <param key="persistenceLevel">The persistence level to consider.</param>
        public IBdoDataContext RemoveItems(
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

            return this;
        }

        // Accessors

        // Items ------------------------------------

        private static string GetItemUniqueName(string name, string contextSectionName = null)
        {
            return ((contextSectionName ?? string.Empty) + "$" + (name ?? string.Empty)).ToLower();
        }

        /// <summary>
        /// Returns a specific system item.
        /// </summary>
        /// <param key="name">Name of the dynamic item to return.</param>
        /// <returns>The dynamic item with specified name and type.</returns>
        public object GetSystemItem(string name)
        {
            return GetSingletonItem(name, "#system");
        }

        /// <summary>
        /// Returns a specific singleton item.
        /// </summary>
        /// <param key="name">Name of the dynamic item to return.</param>
        /// <param key="contextSectionName">Name of the context section to consider.</param>
        /// <returns>The dynamic item with specified name and type.</returns>
        public object GetSingletonItem(string name, string contextSectionName = null)
        {
            string itemName = BdoDataContext.GetItemUniqueName(name, contextSectionName);
            return SingletonItems.ContainsKey(itemName) ? SingletonItems[itemName] : null;
        }

        /// <summary>
        /// Returns a specific scoped item.
        /// </summary>
        /// <param key="name">Name of the dynamic item to return.</param>
        /// <param key="contextSectionName">Name of the context section to consider.</param>
        /// <returns>The dynamic item with specified name and type.</returns>
        public object GetScopedItem(string name, string contextSectionName = null)
        {
            string itemName = BdoDataContext.GetItemUniqueName(name, contextSectionName);
            return ScopedItems.ContainsKey(itemName) ? ScopedItems[itemName] : null;
        }

        /// <summary>
        /// Returns a specific transient item.
        /// </summary>
        /// <param key="name">Name of the dynamic item to return.</param>
        /// <param key="contextSectionName">Name of the context section to consider.</param>
        /// <returns>The dynamic item with specified name and type.</returns>
        public object GetTransientItem(string name, string contextSectionName = null)
        {
            string itemName = BdoDataContext.GetItemUniqueName(name, contextSectionName);
            return TransientItems.ContainsKey(itemName) ? TransientItems[itemName] : null;
        }

        /// <summary>
        /// Returns a specific item.
        /// </summary>
        /// <param key="name">Name of the dynamic item to return.</param>
        /// <param key="contextSectionName">Name of the context section to consider.</param>
        /// <param key="persistenceLevel">The persistence level to consider.</param>
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
    }
}
