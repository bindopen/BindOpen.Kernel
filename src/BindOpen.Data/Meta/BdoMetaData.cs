using BindOpen.Data.Elements;
using BindOpen.Data.Items;
using BindOpen.Data.Specification;
using BindOpen.Logging;
using BindOpen.Runtime.Scopes;
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

        // ------------------------------------------
        // IIdentified Implementation
        // ------------------------------------------

        #region IIdentified

        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

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
        public DataValueTypes DataValueType { get; set; } = DataValueTypes.Any;

        /// <summary>
        /// The itemization mode of this instance.
        /// </summary>
        public DataItemizationMode ItemizationMode
        {
            get
            {
                if (_itemizationMode != DataItemizationMode.Any)
                    return _itemizationMode;
                else if (DataExpression != null)
                    return DataItemizationMode.Expression;
                else if (DataReference != null)
                    return DataItemizationMode.Reference;

                return DataItemizationMode.Value;
            }
            set { _itemizationMode = value; }
        }

        /// <summary>
        /// Item reference of this instance.
        /// </summary>
        public IBdoReference DataReference { get; set; }

        /// <summary>
        /// The script of this instance.
        /// </summary>
        public IBdoExpression DataExpression { get; set; }

        // Specification -------------------------------

        /// <summary>
        /// Specification of this instance.
        /// </summary>
        public List<IBdoMetaSpec> Specs { get; set; }

        // Specification ---------------------

        /// <summary>
        /// Gets a new specification.
        /// </summary>
        /// <returns>Returns the new specifcation.</returns>
        public IBdoMetaSpec NewSpecification()
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
            return (DataValueType == DataValueTypes.Any || item.GetValueType().IsCompatibleWith(DataValueType));
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
                    if (DataReference == null)
                    {
                        log?.AddWarning(title: "Reference missing");
                    }
                    obj = DataReference.Get(scope, varSet, log);
                    break;
                case DataItemizationMode.Expression:
                    if (scope == null)
                    {
                        log?.AddWarning(title: "Application scope missing");
                    }
                    else
                    {
                        if (DataExpression == null)
                        {
                            log?.AddWarning(title: "Script missing");
                        }

                        var interpreter = scope.NewScriptInterpreter();
                        obj = interpreter.Evaluate<object>(DataExpression, varSet, log);
                    }
                    break;
            }

            return obj;
        }

        // Specification

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IBdoMetaSpec GetSpecification(string name = null)
        {
            return Specs?.FirstOrDefault(
                q => (name == null && q.Name == null) || q.Name.BdoKeyEquals(name));
        }

        /// <summary>
        /// 
        /// </summary>
        public IBdoMetaData WithSpecifications(params IBdoMetaSpec[] specs)
        {
            Specs = specs?.ToList();

            return this;
        }

        // Clear

        /// <summary>
        /// Clears the item of this instance.
        /// </summary>
        public IBdoMetaData ClearData()
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
        public IBdoMetaData WithData(object obj)
        {
            _item = obj.ToBdoElementItem(GetSpecification());

            return this;
        }

        /// <summary>
        /// Sets the item of this instance.
        /// </summary>
        /// <param name="item">The string item of this instance.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the items will be the default ones..</remarks>
        /// <returns>Returns True if the specified has been well added.</returns>
        public IBdoMetaData WithDataList(params object[] objs)
        {
            _item = objs?.ToList().ToBdoElementItem(GetSpecification());

            return this;
        }

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param name="log">The log to populate.</param>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="varSet">The variable element set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public List<object> GetDataList(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            var obj = ItemObject(scope, varSet, log);

            var list = obj?.ToObjectList();
            return list;
        }

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param name="log">The log to populate.</param>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="varSet">The variable element set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public List<Q> GetDataList<Q>(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            var list = GetDataList(scope, varSet, log);
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
        public object GetData(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            var list = GetDataList(scope, varSet, log);
            return list?.FirstOrDefault();
        }

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param name="log">The log to populate.</param>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="varSet">The variable element set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public Q GetData<Q>(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            var list = GetDataList<Q>(scope, varSet, log);
            if (list == null)
            {
                return default;
            }

            return list.FirstOrDefault();
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

            el.DataReference = DataReference?.Clone<BdoReference>();
            el.Specs = Specs?.Select(q => q?.Clone<BdoMetaSpec>())
                .Cast<IBdoMetaSpec>().ToList();

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
        // INamed Implementation
        // ------------------------------------------

        #region INamed

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

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

            _isDisposed = true;

            base.Dispose(isDisposing);
        }

        #endregion
    }
}
