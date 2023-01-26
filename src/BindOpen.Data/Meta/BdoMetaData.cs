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
            this.WithName(name);
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
                return BdoMeta.NewSpec<BdoMetaObjectSpec>();
            }
            else if (this is IBdoMetaScalar)
            {
                return BdoMeta.NewSpec<BdoMetaScalarSpec>();
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
        // IIndexed Implementation
        // ------------------------------------------

        #region IIndexed

        /// <summary>
        /// The index of this instance.
        /// </summary>
        public int? Index { get; set; }

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
        // IIdentified Implementation
        // ------------------------------------------

        #region IIdentified

        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        #endregion

        // ------------------------------------------
        // INamed Implementation
        // ------------------------------------------

        #region INamed

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        #endregion

        // ------------------------------------------
        // IDetailed Implementation
        // ------------------------------------------

        #region IDetailed

        /// <summary>
        /// 
        /// </summary>
        public IBdoMetaSet Detail { get; set; }

        #endregion

        // ------------------------------------------
        // IGloballyTitled Implementation
        // ------------------------------------------

        #region IGloballyTitled

        /// <summary>
        /// 
        /// </summary>
        public IBdoDictionary Title { get; set; }

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
