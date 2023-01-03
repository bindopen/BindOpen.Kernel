using BindOpen.Data.Items;
using BindOpen.Data.Specification;
using BindOpen.Logging;
using BindOpen.Runtime.Scopes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// This class represents a data element.
    /// </summary>
    public abstract class BdoElement : BdoItem, IBdoElement
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
        protected BdoElement() : this(null)
        {
        }

        /// <summary>
        /// Initializes a new data element.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="namePreffix">The name preffix to consider.</param>
        /// <param name="id">The ID to consider.</param>
        protected BdoElement(
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
        public DataValueTypes ValueType => GetSpecification()?.ValueType ?? DataValueTypes.Any;

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
        public List<IBdoElementSpec> Specs { get; set; }

        // Specification ---------------------

        /// <summary>
        /// Gets a new specification.
        /// </summary>
        /// <returns>Returns the new specifcation.</returns>
        public IBdoElementSpec NewSpecification()
        {
            if (this is ICarrierElement)
            {
                return BdoElements.NewSpec<CarrierElementSpec>();
            }
            else if (this is ICollectionElement)
            {
                return BdoElements.NewSpec<CollectionElementSpec>();
            }
            else if (this is IObjectElement)
            {
                return BdoElements.NewSpec<ObjectElementSpec>();
            }
            else if (this is IScalarElement)
            {
                return BdoElements.NewSpec<ScalarElementSpec>();
            }
            else if (this is ISourceElement)
            {
                return BdoElements.NewSpec<SourceElementSpec>();
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
        public object GetItem(
            IBdoScope scope = null,
            IBdoElementSet varElementSet = null,
            IBdoLog log = null)
        {
            object object1 = default;

            switch (ItemizationMode)
            {
                case DataItemizationMode.Valued:
                    return _item;
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
                        return ItemReference.Get(scope, varElementSet, log);
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
                        object1 = scope.NewScriptInterpreter().Evaluate<object>(ItemScript, BdoExpressionKind.Script, varElementSet, log);
                        if (object1 != null)
                        {
                            return object1.GetType().IsList() ? object1 as List<object> : object1;
                        }
                    }

                    return default;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Q"></typeparam>
        /// <param name="scope"></param>
        /// <param name="varElementSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public Q GetItem<Q>(
            IBdoScope scope = null,
            IBdoElementSet varElementSet = null,
            IBdoLog log = null)
            => (Q)GetItem(scope, varElementSet, log);

        // Mutators ---------------------------

        /// <summary>
        /// 
        /// </summary>
        public IBdoElement WithItemizationMode(DataItemizationMode mode)
        {
            ItemizationMode = mode;

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        public IBdoElement WithItemReference(IBdoReference reference)
        {
            ItemReference = reference;

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        public IBdoElement WithItemScript(string script)
        {
            ItemScript = script;

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        public IBdoElement WithValueType(DataValueTypes valueType)
        {
            var spec = GetSpecification();
            if (spec == null)
            {
                spec = NewSpecification();
            }

            spec.WithValueType(valueType);

            return this;
        }

        // Specification

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IBdoElementSpec GetSpecification(string name = null)
        {
            return Specs?.FirstOrDefault(
                q => (name == null && q.Name == null) || q.Name.BdoKeyEquals(name));
        }

        /// <summary>
        /// 
        /// </summary>
        public IBdoElement WithSpecifications(params IBdoElementSpec[] specs)
        {
            Specs = specs?.ToList();

            return this;
        }

        // Clear

        /// <summary>
        /// Clears the item of this instance.
        /// </summary>
        public IBdoElement ClearItem()
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
        public IBdoElement WithItem(params object[] objs)
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

            var el = base.Clone<BdoElement>(areas);

            el.ItemReference = ItemReference?.Clone<BdoReference>();
            el.Specs = Specs?.Select(q => q?.Clone<BdoElementSpec>())
                .Cast<IBdoElementSpec>().ToList();

            if (Detail != null)
            {
                if (areas.Contains(nameof(DataAreaKind.Any)) || areas.Contains(nameof(DataAreaKind.Properties)))
                {
                    el.Detail = Detail.Clone() as BdoElementSet;
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
        public int? Index
        {
            get => GetSpecification()?.Index;
            set
            {
                var spec = GetSpecification();
                if (spec != null)
                {
                    spec.Index = value;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public IBdoElement WithIndex(int? index)
        {
            var spec = GetSpecification();
            if (spec == null)
            {
                spec = NewSpecification();
            }

            spec.WithIndex(index);

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
        public IBdoElement WithId(string id)
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
        public IBdoElement WithName(string name)
        {
            Name = BdoItems.NewName(name, _namePreffix);
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
        public IBdoElementSet Detail { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IBdoElement WithDetail(IBdoElementSet detail)
        {
            Detail = detail;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="detail"></param>
        public IBdoElement WithDetail(params IBdoElement[] elements)
        {
            Detail = BdoElements.NewSet(elements);
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

        public IBdoElement AddTitle(KeyValuePair<string, string> item)
        {
            Title ??= BdoItems.NewDictionary();
            Title.Add(item);
            return this;
        }

        public IBdoElement WithTitle(IBdoDictionary dictionary)
        {
            Title = dictionary;
            return this;
        }

        public string GetTitleText(string key = "*", string defaultKey = "*")
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

        public IBdoElement AddDescription(KeyValuePair<string, string> item)
        {
            Description ??= BdoItems.NewDictionary();
            Description.Add(item);
            return this;
        }

        public IBdoElement WithDescription(IBdoDictionary dictionary)
        {
            Description = dictionary;
            return this;
        }

        public string GetDescriptionText(string key = "*", string defaultKey = "*")
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
