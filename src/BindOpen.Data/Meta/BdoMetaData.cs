using BindOpen.Data.Elements;
using BindOpen.Data.Items;
using BindOpen.Data.Specification;
using BindOpen.Logging;
using BindOpen.Runtime.Scopes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a data element.
    /// </summary>
    public abstract class BdoMetaData : BdoItem, IBdoMetaData
    {
        // --------------------------------------------------
        // VARIABLES
        // --------------------------------------------------

        #region Variables

        private string _namePreffix;
        private DataItemizationMode _itemizationMode = DataItemizationMode.Any;

        #endregion

        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public BdoMetaDataKind Kind
        {
            get
            {
                if (this is IBdoMetaDocument)
                    return BdoMetaDataKind.Document;
                else if (this is IBdoMetaObject)
                    return BdoMetaDataKind.Object;
                else if (this is IBdoMetaScalar)
                    return BdoMetaDataKind.Scalar;
                return BdoMetaDataKind.None;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public IBdoMetaData Parent { get; set; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new data element.
        /// </summary>
        protected BdoMetaData() : this(null)
        {
        }

        /// <summary>
        /// Initializes a new data element.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="namePreffix">The name preffix to consider.</param>
        /// <param name="id">The ID to consider.</param>
        protected BdoMetaData(
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
                  (ItemReference != null ? DataItemizationMode.Reference : DataItemizationMode.Value));
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
        public List<IBdoMetaDataSpec> Specs { get; set; }

        // Specification ---------------------

        /// <summary>
        /// Gets a new specification.
        /// </summary>
        /// <returns>Returns the new specifcation.</returns>
        public IBdoMetaDataSpec NewSpecification()
        {
            if (this is IBdoMetaObject)
            {
                return BdoData.NewMetaSpec<BdoMetaObjectSpec>();
            }
            else if (this is IBdoMetaScalar)
            {
                return BdoData.NewMetaSpec<BdoMetaScalarSpec>();
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
        /// <param name="varSet">The variable element set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        protected object ItemObject(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            object obj = default;

            switch (ItemizationMode)
            {
                case DataItemizationMode.Value:
                    obj = _item;
                    break;
                case DataItemizationMode.Reference:
                    if (ItemReference == null)
                    {
                        log?.AddWarning(title: "Reference missing");
                    }
                    obj = ItemReference.Get(scope, varSet, log);
                    break;
                case DataItemizationMode.Script:
                    if (scope == null)
                    {
                        log?.AddWarning(title: "Application scope missing");
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(ItemScript))
                        {
                            log?.AddWarning(title: "Script missing");
                        }

                        var interpreter = scope.NewScriptInterpreter();
                        obj = interpreter.Evaluate<object>(ItemScript, BdoExpressionKind.Script, varSet, log);
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
        /// <param name="varSet">The variable element set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public List<object> Items(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            var obj = ItemObject(scope, varSet, log);

            var list = obj?.AsObjectList();
            return list;
        }

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param name="log">The log to populate.</param>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="varSet">The variable element set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public List<Q> Items<Q>(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            var list = Items(scope, varSet, log);
            return list?.Select(q =>
            {
                if (q is Q q_Q)
                    return q_Q;

                return default;
            }).ToList();
        }

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param name="log">The log to populate.</param>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="varSet">The variable element set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public object Item(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            var list = Items(scope, varSet, log);
            return list?.FirstOrDefault();
        }

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param name="log">The log to populate.</param>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="varSet">The variable element set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public Q Item<Q>(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            var list = Items<Q>(scope, varSet, log);
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
        public IBdoMetaData WithItemizationMode(DataItemizationMode mode)
        {
            ItemizationMode = mode;

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        public IBdoMetaData WithItemReference(IBdoReference reference)
        {
            ItemReference = reference;

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        public IBdoMetaData WithItemScript(string script)
        {
            ItemScript = script;

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        public IBdoMetaData WithValueType(DataValueTypes valueType)
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
        public IBdoMetaDataSpec GetSpecification(string name = null)
        {
            return Specs?.FirstOrDefault(
                q => (name == null && q.Name == null) || q.Name.BdoKeyEquals(name));
        }

        /// <summary>
        /// 
        /// </summary>
        public IBdoMetaData WithSpecifications(params IBdoMetaDataSpec[] specs)
        {
            Specs = specs?.ToList();

            return this;
        }

        // Clear

        /// <summary>
        /// Clears the item of this instance.
        /// </summary>
        public IBdoMetaData ClearItems()
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
        public IBdoMetaData WithItems(params object[] objs)
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

            var el = base.Clone<BdoMetaData>(areas);

            el.ItemReference = ItemReference?.Clone<BdoReference>();
            el.Specs = Specs?.Select(q => q?.Clone<BdoMetaDataSpec>())
                .Cast<IBdoMetaDataSpec>().ToList();

            if (Detail != null)
            {
                if (areas.Contains(nameof(DataAreaKind.Any)) || areas.Contains(nameof(DataAreaKind.Properties)))
                {
                    el.Detail = Detail.Clone() as BdoMetaSet;
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
        public IBdoMetaData WithIndex(int? index)
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
        public IBdoMetaData WithId(string id)
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
        public IBdoMetaData WithName(string name)
        {
            Name = BdoData.NewName(name, _namePreffix);
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
        public IBdoMetaSet Detail { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IBdoMetaData WithDetail(IBdoMetaSet detail)
        {
            Detail = detail;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="detail"></param>
        public IBdoMetaData WithDetail(params IBdoMetaData[] elems)
        {
            Detail = BdoData.NewMetaSet(elems);
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

        public IBdoMetaData AddTitle(KeyValuePair<string, string> item)
        {
            Title ??= BdoData.NewDictionary();
            Title.Add(item);
            return this;
        }

        public IBdoMetaData WithTitle(IBdoDictionary dico)
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

        public IBdoMetaData AddDescription(KeyValuePair<string, string> item)
        {
            Description ??= BdoData.NewDictionary();
            Description.Add(item);
            return this;
        }

        public IBdoMetaData WithDescription(IBdoDictionary dico)
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
