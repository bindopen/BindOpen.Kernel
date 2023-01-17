using BindOpen.Logging;
using BindOpen.MetaData.Items;
using BindOpen.MetaData.Specification;
using BindOpen.Runtime.Scopes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.MetaData.Elements
{
    /// <summary>
    /// This class represents a data element.
    /// </summary>
    public abstract class BdoMetaElement : BdoItem, IBdoMetaElement
    {
        // --------------------------------------------------
        // VARIABLES
        // --------------------------------------------------

        #region Variables

        private string _namePreffix;
        private DataItemizationMode _itemizationMode = DataItemizationMode.Any;

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new data element.
        /// </summary>
        protected BdoMetaElement() : this(null)
        {
        }

        /// <summary>
        /// Initializes a new data element.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="namePreffix">The name preffix to consider.</param>
        /// <param name="id">The ID to consider.</param>
        protected BdoMetaElement(
            string name = null,
            string namePreffix = null,
            string id = null) : base()
        {
            _namePreffix = namePreffix ?? "element_";
            WithName(name);
            Id = id;
        }

        #endregion

        // --------------------------------------------------
        // IBdoElement Implementation
        // --------------------------------------------------

        #region IBdoElement

        /// <summary>
        /// The item of this instance.
        /// </summary>
        protected object _item;

        // Items --------------------------------------------

        /// <summary>
        /// The value type of this instance.
        /// </summary>
        public DataValueTypes ValueType { get; set; } = DataValueTypes.Any;

        /// <summary>
        /// The itemization mode of this instance.
        /// </summary>
        public DataItemizationMode ItemizationMode
        {
            get
            {
                return _itemizationMode != DataItemizationMode.Any ? _itemizationMode :
                  (!string.IsNullOrEmpty(ItemScript) ? DataItemizationMode.Script :
                  (ItemReference != null ? DataItemizationMode.Referenced : DataItemizationMode.Valued));
            }
            set { _itemizationMode = value; }
        }

        /// <summary>
        /// Item reference of this instance.
        /// </summary>
        public IBdoReference ItemReference { get; set; }

        /// <summary>
        /// The script of this instance.
        /// </summary>
        public string ItemScript { get; set; }

        // Specification -------------------------------

        /// <summary>
        /// Specification of this instance.
        /// </summary>
        public List<IBdoMetaElementSpec> Specs { get; set; }

        // Specification ---------------------

        /// <summary>
        /// Gets a new specification.
        /// </summary>
        /// <returns>Returns the new specifcation.</returns>
        public IBdoMetaElementSpec NewSpecification()
        {
            if (this is IBdoMetaCarrier)
            {
                return BdoMeta.NewSpec<BdoMetaCarrierSpec>();
            }
            else if (this is IBdoMetaCollection)
            {
                return BdoMeta.NewSpec<BdoMetaCollectionSpec>();
            }
            else if (this is IBdoMetaObject)
            {
                return BdoMeta.NewSpec<BdoMetaObjectSpec>();
            }
            else if (this is IBdoMetaScalar)
            {
                return BdoMeta.NewSpec<BdoMetaScalarSpec>();
            }
            else if (this is IBdoMetaSource)
            {
                return BdoMeta.NewSpec<BdoMetaSourceSpec>();
            }

            return null;
        }

        /// <summary>
        /// Indicates whether this instance is compatible with the specified item.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <returns></returns>
        public bool IsCompatibleWithItem(object item)
        {
            return (ValueType == DataValueTypes.Any || item.GetValueType().IsCompatibleWith(ValueType));
        }

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param name="log">The log to populate.</param>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="varElementSet">The variable element set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public object ItemObject(
            IBdoScope scope = null,
            IBdoMetaElementSet varElementSet = null,
            IBdoLog log = null)
        {
            object obj = default;

            switch (ItemizationMode)
            {
                case DataItemizationMode.Valued:
                    obj = _item;
                    break;
                case DataItemizationMode.Referenced:
                    if (scope == null)
                    {
                        log?.AddError(title: "Application scope missing");
                    }
                    else if (ItemReference == null)
                    {
                        log?.AddWarning(title: "Reference missing");
                    }
                    else
                    {
                        obj = ItemReference.Get(scope, varElementSet, log);
                    }
                    break;
                case DataItemizationMode.Script:
                    if (scope == null)
                    {
                        log?.AddWarning(title: "Application scope missing");
                    }
                    else if (string.IsNullOrEmpty(ItemScript))
                    {
                        log?.AddWarning(title: "Script missing");
                    }
                    else
                    {
                        obj = scope.NewScriptInterpreter().Evaluate<object>(ItemScript, BdoExpressionKind.Script, varElementSet, log);
                    }
                    break;
            }

            return obj;
        }

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param name="log">The log to populate.</param>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="varElementSet">The variable element set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public List<object> Items(
            IBdoScope scope = null,
            IBdoMetaElementSet varElementSet = null,
            IBdoLog log = null)
        {
            var obj = ItemObject(scope, varElementSet, log);

            var list = obj?.GetType().IsList() == true
                ? (obj as IEnumerable<object>)?.ToList()
                : new List<object>() { obj };

            return list;
        }

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param name="log">The log to populate.</param>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="varElementSet">The variable element set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public List<Q> Items<Q>(
            IBdoScope scope = null,
            IBdoMetaElementSet varElementSet = null,
            IBdoLog log = null)
        {
            var list = Items(scope, varElementSet, log);
            return list?.Cast<Q>().ToList();
        }

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param name="log">The log to populate.</param>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="varElementSet">The variable element set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public object Item(
            IBdoScope scope = null,
            IBdoMetaElementSet varElementSet = null,
            IBdoLog log = null)
        {
            var list = Items(scope, varElementSet, log);
            return list?.FirstOrDefault();
        }

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param name="log">The log to populate.</param>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="varElementSet">The variable element set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public Q Item<Q>(
            IBdoScope scope = null,
            IBdoMetaElementSet varElementSet = null,
            IBdoLog log = null)
        {
            var list = Items<Q>(scope, varElementSet, log);
            if (list == null)
            {
                return default;
            }

            return list.FirstOrDefault();
        }

        // Mutators ---------------------------

        /// <summary>
        /// 
        /// </summary>
        public IBdoMetaElement WithItemizationMode(DataItemizationMode mode)
        {
            ItemizationMode = mode;

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        public IBdoMetaElement WithItemReference(IBdoReference reference)
        {
            ItemReference = reference;

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        public IBdoMetaElement WithItemScript(string script)
        {
            ItemScript = script;

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        public IBdoMetaElement WithValueType(DataValueTypes valueType)
        {
            ValueType = valueType;
            return this;
        }

        // Specification

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IBdoMetaElementSpec GetSpecification(string name = null)
        {
            return Specs?.FirstOrDefault(
                q => (name == null && q.Name == null) || q.Name.BdoKeyEquals(name));
        }

        /// <summary>
        /// 
        /// </summary>
        public IBdoMetaElement WithSpecifications(params IBdoMetaElementSpec[] specs)
        {
            Specs = specs?.ToList();

            return this;
        }

        // Clear

        /// <summary>
        /// Clears the item of this instance.
        /// </summary>
        public IBdoMetaElement ClearItems()
        {
            _item = null;

            return this;
        }

        /// <summary>
        /// Sets the item of this instance.
        /// </summary>
        /// <param name="item">The string item of this instance.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the items will be the default ones..</remarks>
        /// <returns>Returns True if the specified has been well added.</returns>
        public IBdoMetaElement WithItems(params object[] objs)
        {
            _item = objs.ToList().ToBdoElementItem(GetSpecification());

            return this;
        }

        // Accessors --------------------------

        /// <summary>
        /// Returns a text node representing this instance.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Empty;
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
            if (areas == null)
            {
                areas = new[] { nameof(DataAreaKind.Any) };
            }

            var el = base.Clone<BdoMetaElement>(areas);

            el.ItemReference = ItemReference?.Clone<BdoReference>();
            el.Specs = Specs?.Select(q => q?.Clone<BdoMetaElementSpec>())
                .Cast<IBdoMetaElementSpec>().ToList();

            if (Detail != null)
            {
                if (areas.Contains(nameof(DataAreaKind.Any)) || areas.Contains(nameof(DataAreaKind.Properties)))
                {
                    el.Detail = Detail.Clone() as BdoMetaElementSet;
                }
            }

            return el;
        }

        #endregion

        // ------------------------------------------
        // IIndexedPoco Implementation
        // ------------------------------------------

        #region IIndexedPoco


        /// <summary>
        /// The index of this instance.
        /// </summary>
        public int? Index { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public IBdoMetaElement WithIndex(int? index)
        {
            Index = index;

            return this;
        }

        #endregion

        // ------------------------------------------
        // IReferenced Implementation
        // ------------------------------------------

        #region IReferenced

        /// <summary>
        /// 
        /// </summary>
        public string Key() => Name;

        #endregion

        // ------------------------------------------
        // IIdentifiedPoco Implementation
        // ------------------------------------------

        #region IIdentifiedPoco

        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IBdoMetaElement WithId(string id)
        {
            Id = id;
            return this;
        }

        #endregion

        // ------------------------------------------
        // INamedPoco Implementation
        // ------------------------------------------

        #region INamedPoco

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IBdoMetaElement WithName(string name)
        {
            Name = BdoMeta.NewName(name, _namePreffix);
            return this;
        }

        #endregion

        // ------------------------------------------
        // ITDetailedPoco Implementation
        // ------------------------------------------

        #region ITDetailedPoco

        /// <summary>
        /// 
        /// </summary>
        public IBdoMetaElementSet Detail { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IBdoMetaElement WithDetail(IBdoMetaElementSet detail)
        {
            Detail = detail;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="detail"></param>
        public IBdoMetaElement WithDetail(params IBdoMetaElement[] elements)
        {
            Detail = BdoMeta.NewSet(elements);
            return this;
        }

        #endregion

        // ------------------------------------------
        // IGloballyTitled Implementation
        // ------------------------------------------

        #region IGloballyTitled

        /// <summary>
        /// 
        /// </summary>
        public IBdoDictionary Title { get; set; }

        public IBdoMetaElement AddTitle(KeyValuePair<string, string> item)
        {
            Title ??= BdoMeta.NewDictionary();
            Title.Add(item);
            return this;
        }

        public IBdoMetaElement WithTitle(IBdoDictionary dico)
        {
            Title = dico;
            return this;
        }

        public string GetTitleText(string key = StringHelper.__Star, string defaultKey = StringHelper.__Star)
        {
            return Title?[key, defaultKey];
        }

        #endregion

        // ------------------------------------------
        // IGloballyDescribed Implementation
        // ------------------------------------------

        #region IGloballyDescribed

        /// <summary>
        /// 
        /// </summary>
        public IBdoDictionary Description { get; set; }

        public IBdoMetaElement AddDescription(KeyValuePair<string, string> item)
        {
            Description ??= BdoMeta.NewDictionary();
            Description.Add(item);
            return this;
        }

        public IBdoMetaElement WithDescription(IBdoDictionary dico)
        {
            Description = dico;
            return this;
        }

        public string GetDescriptionText(string key = StringHelper.__Star, string defaultKey = StringHelper.__Star)
        {
            return Description?[key, defaultKey];
        }

        #endregion

        // ------------------------------------------
        // IDISPOSABLE METHODS
        // ------------------------------------------

        #region IDisposable_Methods

        private bool _isDisposed = false;

        /// <summary>
        /// Disposes this instance. 
        /// </summary>
        /// <param name="isDisposing">Indicates whether this instance is disposing</param>
        protected override void Dispose(bool isDisposing)
        {
            if (_isDisposed)
            {
                return;
            }

            Detail?.Dispose();

            _isDisposed = true;

            base.Dispose(isDisposing);
        }

        #endregion
    }
}
