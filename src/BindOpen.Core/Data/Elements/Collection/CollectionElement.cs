using BindOpen.Application.Scopes;
using BindOpen.Data.Common;
using BindOpen.Data.Helpers.Objects;
using BindOpen.Data.Items;
using BindOpen.System.Diagnostics;
using BindOpen.System.Scripting;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// This class represents a catalog element that is an element whose elements are entities.
    /// </summary>
    [XmlType("CatalogElement", Namespace = "https://storage.bindopen.org/pgrkhpym/docs/code/xsd/bindopen")]
    [XmlRoot(ElementName = "catalog", Namespace = "https://storage.bindopen.org/pgrkhpym/docs/code/xsd/bindopen", IsNullable = false)]
    public class CollectionElement : DataElement, ICollectionElement
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The elements of this instance.
        /// </summary>
        [XmlElement("carrier", typeof(CarrierElement))]
        [XmlElement("document", typeof(DocumentElement))]
        [XmlElement("object", typeof(ObjectElement))]
        [XmlElement("scalar", typeof(ScalarElement))]
        [XmlElement("source", typeof(SourceElement))]
        [XmlElement("collection", typeof(CollectionElement))]
        [XmlArrayElement("elements")]
        public List<DataElement> Elements
        {
            get;
            set;
        }

        // Specifcation -----------------------

        /// <summary>
        /// The specification of this instance.
        /// </summary>
        [XmlElement("specification")]
        public new CollectionElementSpec Specification
        {
            get { return base.Specification as CollectionElementSpec; }
            set { base.Specification = value; }
        }

        // IDataElementSet -----------------------

        List<IDataElement> ITDataItemSet<IDataElement>.Items
        {
            get => base.Items.Cast<IDataElement>().ToList();
            set { base.Items = value?.Cast<object>().ToList(); }
        }

        public int Count => base.Items.Count;

        public IDataElement this[string key] => Get(key);

        IDataElement ITDataItemSet<IDataElement>.this[int index] => base[index] as IDataElement;

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CollectionElement class.
        /// </summary>
        public CollectionElement() : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the CollectionElement class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="id">The ID to consider.</param>
        public CollectionElement(string name = null, string id = null)
            : base(name, "collectionElem_", id)
        {
            ValueType = DataValueType.Element;
        }

        #endregion

        // --------------------------------------------------
        // ITEMS
        // --------------------------------------------------

        #region Items

        // Specification ---------------------

        /// <summary>
        /// Creates a new specification.
        /// </summary>
        /// <returns>Returns the new specifcation.</returns>
        public override IDataElementSpec NewSpecification()
        {
            return Specification = new CollectionElementSpec();
        }

        // Items ----------------------------

        /// <summary>
        /// Returns a text node representing this instance.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Join("|", Items.Select(p => (p as NamedDataItem)?.Key() ?? "").ToArray());
        }

        /// <summary>
        /// Returns the item with the specified name and group ID.
        /// </summary>
        /// <param name="name">The name of the item to return.</param>
        /// <param name="groupId">The ID of the group of the item to return.</param>
        /// <returns>Returns the item with the specified name and group ID.</returns>
        public IDataElement GetElement(string name, string groupId = null)
        {
            return Items?.FirstOrDefault(p =>
                p.KeyEquals(name)
                && ((p as DataElement)?.Specification?.GroupId.KeyEquals(groupId) != false)) as IDataElement;
        }

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param name="elementName">The element name to consider.</param>
        /// <param name="log">The log to populate.</param>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public object GetElementObject(
            string elementName = null,
            IBdoScope scope = null,
            IScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            IDataElement element = (elementName != null ? GetElement(elementName) : Elements[0]);
            return element?.GetValue(scope, scriptVariableSet, log);
        }

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param name="elementName">The element name to consider.</param>
        /// <param name="log">The log to populate.</param>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public T GetElementObject<T>(
            string elementName = null,
            IBdoScope scope = null,
            IScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            return (T)GetElementObject(elementName, scope, scriptVariableSet, log);
        }

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>Returns the items of this instance.</returns>
        public override object GetValue(
            IBdoScope scope = null,
            IScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            switch (ItemizationMode)
            {
                case DataItemizationMode.Valued:
                    if (Items.Count == 0) return null;
                    var aObject = new Dictionary<string, object>();
                    foreach (var item in Items)
                    {
                        if (item is DataElement element && !aObject.ContainsKey(element.Name))
                        {
                            aObject.Add(element.Name, element.GetValue(scope, scriptVariableSet, log));
                        }
                    }
                    return aObject;
                case DataItemizationMode.Referenced:
                case DataItemizationMode.Script:
                    return base.GetValue(scope, scriptVariableSet, log);
            }

            return null;
        }

        #endregion

        // --------------------------------------------------
        // SERIALIZATION
        // --------------------------------------------------

        #region Serialization

        /// <summary>
        /// Updates information for storage.
        /// </summary>
        /// <param name="log">The log to update.</param>
        public override void UpdateStorageInfo(IBdoLog log = null)
        {
            base.UpdateStorageInfo(log);

            Elements = Items?.Select(p =>
            {
                DataElement element = p as DataElement;
                element?.UpdateStorageInfo(log);
                return element;
            }).ToList();
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The set of script variables to consider.</param>
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(IBdoScope scope = null, IScriptVariableSet scriptVariableSet = null, IBdoLog log = null)
        {
            foreach (DataElement element in Elements)
            {
                element.UpdateRuntimeInfo(scope, scriptVariableSet, log);
                Add(element);
            }

            base.UpdateRuntimeInfo(scope, scriptVariableSet, log);
        }

        #endregion

        // --------------------------------------------------
        // CLONING
        // --------------------------------------------------

        #region Cloning

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns a cloned instance.</returns>
        public override object Clone(params string[] areas)
        {
            var element = base.Clone(areas) as CollectionElement;

            return element;
        }

        #endregion

        // --------------------------------------------------
        // IDATAELEMENT IMPLEMENTATION
        // --------------------------------------------------

        #region IDataElement Implementation

        /// <summary>
        /// Sets the specified single item of this instance.
        /// </summary>
        /// <param name="items">The items to apply to this instance.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the values will be the default ones..</remarks>
        public ITDataItemSet<IDataElement> WithItems(params IDataElement[] items)
        {
            base.WithItems(items?.Cast<object>().ToArray());

            return this;
        }

        // Element items ------------------------

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="items">The items of the item to add.</param>
        /// <returns>Returns the new item that has been added.
        /// Returns null if the new item is null or else its name is null.</returns>
        /// <remarks>The new item must have a name.</remarks>
        public ITDataItemSet<IDataElement> Add(params IDataElement[] items)
        {
            if (items != null)
            {
                foreach (IDataElement item in items)
                {
                    var key = item?.Key();
                    if (key != null)
                    {
                        if (Items == null)
                        {
                            Items = new List<object>();
                        }
                        else
                        {
                            Remove(key);
                        }

                        Items.Add(item);
                    }
                }
            }

            return this;
        }

        /// <summary>
        /// Removes the item with the specified name.
        /// </summary>
        /// <param name="keys">The keys of the item to remove.</param>
        public ITDataItemSet<IDataElement> Remove(params string[] keys)
        {
            Items?.RemoveAll(p => keys.Any(q => p.KeyEquals(q)));

            return this;
        }

        /// <summary>
        /// Returns the item of this instance.
        /// </summary>
        /// <param name="elementKey">The element key to consider.</param>
        /// <param name="item">The item to consider.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>Indicates whether the item has been set.</returns>
        public IDataElementSet AddValue(
            string elementKey,
            object item = null,
            IBdoLog log = null)
        {
            IDataElement element = Get(elementKey);
            element?.Add(item, log);

            return this;
        }

        /// <summary>
        /// Returns the items of this instance.
        /// </summary>
        /// <param name="elementKey">The element key to consider.</param>
        /// <param name="items">The items to consider.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>Returns the items of this instance.</returns>
        public IDataElementSet AddValue(
            string elementKey,
            object[] items = null,
            IBdoLog log = null)
        {
            IDataElement element = Get(elementKey);
            element?.Add(items, log);

            return this;
        }

        /// <summary>
        /// Clears the items of this instance.
        /// </summary>
        /// <returns>Returns this instance.</returns>
        ITDataItemSet<IDataElement> ITDataItemSet<IDataElement>.ClearItems()
        {
            return base.ClearItems() as ITDataItemSet<IDataElement>;
        }

        // Elements -----------------------------

        /// <summary>
        /// Returns the item with the specified name and group ID.
        /// </summary>
        /// <param name="name">The name of the item to return.</param>
        /// <param name="groupId">The ID of the group of the item to return.</param>
        /// <returns>Returns the item with the specified name and group ID.</returns>
        public IDataElement GetWithGroup(string name = null, string groupId = null)
        {
            return Items?.Cast<IDataElement>().FirstOrDefault(p =>
                (name == null || p.Name.KeyEquals(name))
                && (groupId == null || p.Specification?.GroupId.KeyEquals(groupId) != false));
        }

        // Groups -------------------------------

        /// <summary>
        /// Gets all the element groups IDs.
        /// </summary>
        /// <returns>Returns all the element group IDs.</returns>
        public List<string> GetGroupIds()
        {
            if (Items == null) return new List<string>();
            return Items.Cast<IDataElement>().Select(p => p.Specification?.GroupId).Distinct().ToList();
        }

        // Element items ------------------------

        /// <summary>
        /// Checks if this instance has an item with the specified name.
        /// </summary>
        /// <param name="key">The key of the item to check.</param>
        /// <returns>Returns true if the instance has an item with the specified name.</returns>
        public bool HasItem(string key = null)
        {
            if (key == null) return Items.Count > 0;
            return Items?.Any(p => p.KeyEquals(key)) == true;
        }

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public IDataElement Get(string key = null)
        {
            if (key == null) return this[0] as IDataElement;
            return this[key];
        }

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public Q Get<Q>(string key = null) where Q : class, IDataElement
        {
            return Get(key) as Q;
        }

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="index">The index to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public Q Get<Q>(int index) where Q : class, IDataElement
        {
            return this[index] as Q;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDataElement[] ToArray()
        {
            return Items.Cast<IDataElement>()?.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<IDataElement> ToList()
        {
            return Items.Cast<IDataElement>()?.ToList();
        }

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param name="elementKey">The element key to consider.</param>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>Returns the items of this instance.</returns>
        public virtual object GetValue(
            string elementKey,
            IBdoScope scope = null,
            IScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            IDataElement element = Get(elementKey);
            if (element != null)
            {
                return element.GetValue(scope, scriptVariableSet, log);
            }

            return null;
        }

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param name="elementKey">The element key to consider.</param>
        /// <param name="log">The log to populate.</param>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public virtual T GetValue<T>(
            string elementKey,
            IBdoScope scope = null,
            IScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            var aObject = GetValue(elementKey, scope, scriptVariableSet, log) ?? default(T);
            return (T)aObject;
        }

        #endregion
    }
}
