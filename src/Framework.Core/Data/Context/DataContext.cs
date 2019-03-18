using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Data.Context
{

    /// <summary>
    /// This class represents a data context. A data context contains all the data related to a user session.
    /// </summary>
    [Serializable()]
    public class DataContext : MarshalByRefObject
    {
        // --------------------------------------------------
        // VARIABLES
        // --------------------------------------------------

        #region Variables

        private readonly Dictionary<string, object> _singletonItems = new Dictionary<string, object>();
        private readonly Dictionary<string, object> _scopedItems = new Dictionary<string, object>();
        private readonly Dictionary<string, object> _transientItems = new Dictionary<string, object>();
        //private Dictionary<string, object> _Extensions;

        #endregion

        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// ID of this instance.
        /// </summary>
        public String Id { get; set; }

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
            //this._Extensions = new Dictionary<string, object>();
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
        public void Merge(DataContext dataContext)
        {
            if (dataContext != null)
            {
                //foreach (DictionaryEntry aDictionaryEntry in dataContext._Extensions)
                //    if (!this._Extensions.Contains(aDictionaryEntry.Key))
                //        this._Extensions.Add(aDictionaryEntry.Key, aDictionaryEntry.Value);

                //foreach (DictionaryEntry aDictionaryEntry in dataContext._dataSets)
                //    if (!this._dataSets.Contains(aDictionaryEntry.Key))
                //        this._dataSets.Add(aDictionaryEntry.Key, aDictionaryEntry.Value);

                foreach (KeyValuePair<string,object> entry in dataContext._singletonItems)
                {
                    if (!this._singletonItems.ContainsKey(entry.Key))
                    {
                        this._singletonItems.Add(entry.Key, entry.Value);
                    }
                }

                foreach (KeyValuePair<string, object> entry in dataContext._scopedItems)
                {
                    if (!this._scopedItems.ContainsKey(entry.Key))
                    {
                        this._scopedItems.Add(entry.Key, entry.Value);
                    }
                }

                foreach (KeyValuePair<string, object> entry in dataContext._transientItems)
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
            //this._dataSets.Clear();
            this._singletonItems.Clear();
            this._scopedItems.Clear();
            this._transientItems.Clear();
        }

        //// Extensions -------------------------

        ///// <summary>
        ///// Adds a new extension to this instance. An extension contains 
        ///// a set of methods allowing to edit the dynamic data of a context
        ///// at a business level.
        ///// </summary>
        ///// <param name="name">Name of the data context extension to add.</param>
        ///// <param name="dataContextExtension">The data context extension to add.</param>
        //public void AddExtension(
        //    String name,
        //    DataContextExtension dataContextExtension)
        //{
        //    name = name.ToUpper();
        //    if (!this._Extensions.ContainsKey(name))
        //    {
        //        dataContextExtension.SetDataContext(this);
        //        this._Extensions.Add(name, dataContextExtension);
        //    }
        //}

        ///// <summary>
        ///// Adds the extensions of the specified application extension.
        ///// at a business level.
        ///// </summary>
        ///// <param name="appExtension">The application extension to add.</param>
        ///// <param name="libraryNames">The names of the library to add.</param>
        //public Log LoadExtensions(
        //    AppExtension appExtension,
        //    List<String> libraryNames = null)
        //{
        //    Log log = new Log();

        //    this._Extensions = new Dictionary<string, object>();

        //    if (appExtension!= null)
        //        foreach(DataContextExtensionDefinition aExtensionDefinition in 
        //            appExtension.GetItemDefinitions<DataContextExtensionDefinition>(libraryNames))
        //        {
        //            DataContextExtension dataContextExtension = appExtension.CreateItemInstance(
        //                AppExtensionItemKind.ContextExtension, aExtensionDefinition.UniqueName,null, log) as DataContextExtension;
        //            this.AddExtension(aExtensionDefinition.UniqueName, dataContextExtension);
        //        }

        //    return log;
        //}

        ///// <summary>
        ///// Removes the data context extension with the specified name.
        ///// </summary>
        ///// <param name="name">Name of the data context extension to remove.</param>
        //public void RemoveExtension(String name)
        //{
        //    if (this._Extensions.ContainsKey(name))
        //        this._Extensions.Remove(name);
        //}

        // Data contexts ------------------------------------

        ///// <summary>
        ///// Adds a new dataset to this instance.
        ///// </summary>
        ///// <param name="name">Name of the dataset to add.</param>
        ///// <param name="dataSet">Dataset to add.</param>
        //public void AddDataSet(String name, DataSet dataSet)
        //{
        //    if (this._dataSets.Contains(name.ToUpper()))
        //        this._dataSets.Remove(name.ToUpper());
        //    this._dataSets.Add(name.ToUpper(), dataSet);
        //}

        // Items ------------------------------------

        /// <summary>
        /// Adds a new item to this instance.
        /// </summary>
        /// <param name="name">Name of the item to add.</param>
        /// <param name="contextSectionName">Name of the context section to consider.</param>
        /// <param name="item">Item to add.</param>
        /// <param name="persistenceLevel">Persistence level of the item to add.</param>
        public void AddItem(
            String name,
            Object item,
            String contextSectionName = null,
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
            String name,
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
            String name,
            Object item,
            String contextSectionName = null)
        {
            String itemName = DataContext.GetItemUniqueName(name, contextSectionName);

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
            String name,
            Object item,
            String contextSectionName = null)
        {
            String itemName = DataContext.GetItemUniqueName(name, contextSectionName);

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
            String name,
            Object item,
            String contextSectionName = null)
        {
            String itemName = DataContext.GetItemUniqueName(name, contextSectionName);

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
            String contextSectionName = null,
            PersistenceLevel persistenceLevel = PersistenceLevel.Singleton)
        {
            if ((persistenceLevel == PersistenceLevel.Any) | (persistenceLevel == PersistenceLevel.Singleton))
            {
                var items = this._singletonItems.Keys.Where(p =>
                    (String.IsNullOrEmpty(contextSectionName))
                    || (p?.ToString().ToLower().StartsWith(contextSectionName.ToLower() + "$") == true));
                foreach (string key in items)
                {
                    this._singletonItems.Remove(key);
                }
            }
            if ((persistenceLevel == PersistenceLevel.Any) | (persistenceLevel == PersistenceLevel.Scoped))
            {
                var items = this._scopedItems.Keys.Where(p =>
                    (String.IsNullOrEmpty(contextSectionName))
                    || (p?.ToString().ToLower().StartsWith(contextSectionName.ToLower() + "$") == true));
                foreach (string key in items)
                {
                    this._scopedItems.Remove(key);
                }
            }
            if ((persistenceLevel == PersistenceLevel.Any) | (persistenceLevel == PersistenceLevel.Transient))
            {
                var items = this._transientItems.Keys.Where(p =>
                    (String.IsNullOrEmpty(contextSectionName))
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

        //// Extensions ------------------------------------

        ///// <summary>
        ///// Returns the extension with the specified name.
        ///// </summary>
        ///// <param name="name">Name of the data context extension of this instance to return.</param>
        //public DataContextExtension GetExtension(String name)
        //{
        //    name = name.ToUpper();
        //    if (this._Extensions.Contains(name))
        //        return (DataContextExtension)this._Extensions[name];
        //    return null;
        //}

        //// Datasets ------------------------------------

        ///// <summary>
        ///// Returns a specific dataset.
        ///// </summary>
        ///// <param name="name">The name of the dataset to return.</param>
        ///// <returns>The dataset with the specified name.</returns>
        //public DataSet GetDataSet(String name)
        //{
        //    if (this._dataSets.Contains(name.ToUpper()))
        //        return (DataSet)this._dataSets[name.ToUpper()];
        //    return null;
        //}

        // Items ------------------------------------

        private static String GetItemUniqueName(String name, String contextSectionName = null)
        {
            return ((contextSectionName ?? "") + "$" + (name ?? "")).ToLower();
        }

        /// <summary>
        /// Returns a specific system item.
        /// </summary>
        /// <param name="name">Name of the dynamic item to return.</param>
        /// <returns>The dynamic item with specified name and type.</returns>
        public Object GetSystemItem(String name)
        {
            return this.GetSingletonItem(name, "#system");
        }

        /// <summary>
        /// Returns a specific singleton item.
        /// </summary>
        /// <param name="name">Name of the dynamic item to return.</param>
        /// <param name="contextSectionName">Name of the context section to consider.</param>
        /// <returns>The dynamic item with specified name and type.</returns>
        public Object GetSingletonItem(String name, String contextSectionName = null)
        {
            String itemName = DataContext.GetItemUniqueName(name,contextSectionName);
            return this._singletonItems.ContainsKey(itemName) ? this._singletonItems[itemName] : null;
        }

        /// <summary>
        /// Returns a specific scoped item.
        /// </summary>
        /// <param name="name">Name of the dynamic item to return.</param>
        /// <param name="contextSectionName">Name of the context section to consider.</param>
        /// <returns>The dynamic item with specified name and type.</returns>
        public Object GetScopedItem(String name, String contextSectionName = null)
        {
            String itemName = DataContext.GetItemUniqueName(name, contextSectionName);
            return (_scopedItems.ContainsKey(itemName) ? this._scopedItems[itemName] : null);
        }

        /// <summary>
        /// Returns a specific transient item.
        /// </summary>
        /// <param name="name">Name of the dynamic item to return.</param>
        /// <param name="contextSectionName">Name of the context section to consider.</param>
        /// <returns>The dynamic item with specified name and type.</returns>
        public Object GetTransientItem(String name, String contextSectionName = null)
        {
            String itemName = DataContext.GetItemUniqueName(name, contextSectionName);
            return this._transientItems.ContainsKey(itemName) ? this._transientItems[itemName] : null;
        }

        /// <summary>
        /// Returns a specific item.
        /// </summary>
        /// <param name="name">Name of the dynamic item to return.</param>
        /// <param name="contextSectionName">Name of the context section to consider.</param>
        /// <param name="persistenceLevel">The persistence level to consider.</param>
        /// <returns>The dynamic item with specified name and type.</returns>
        public Object GetItem(String name,
            String contextSectionName = null,
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
        public Boolean Save(String filePath)
        {
            Stream fileStream = null;
            Boolean isSaved = false;

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
        public static DataContext Load(String filePath, ref Log log)
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
