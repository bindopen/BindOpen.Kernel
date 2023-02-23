using BindOpen.Data.Helpers;
using BindOpen.Data.Items;
using BindOpen.Extensions.Scripting;
using BindOpen.Logging;
using BindOpen.Runtime.Scopes;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a data element.
    /// </summary>
    public abstract class BdoMetaData : BdoItem,
        IBdoMetaData
    {
        // --------------------------------------------------
        // VARIABLES
        // --------------------------------------------------

        #region Variables

        private string _namePreffix;
        private DataValueMode _valueMode = DataValueMode.Any;

        /// <summary>
        /// The item of this instance.
        /// </summary>
        protected object _data;

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
        /// <param key="name">The name to consider.</param>
        /// <param key="namePreffix">The name preffix to consider.</param>
        /// <param key="id">The ID to consider.</param>
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
        // IBdoMetaData Implementation
        // --------------------------------------------------

        #region IBdoMetaData

        // Items --------------------------------------------

        /// <summary>
        /// 
        /// </summary>
        public BdoMetaDataKind MetaDataKind
        {
            get
            {
                if (this is IBdoMetaDocument)
                    return BdoMetaDataKind.Document;
                else if (this is IBdoMetaObject)
                    return BdoMetaDataKind.Object;
                else if (this is IBdoMetaScalar)
                    return BdoMetaDataKind.Scalar;
                else if (this is IBdoMetaSet)
                    return BdoMetaDataKind.Set;
                return BdoMetaDataKind.None;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public IBdoMetaData Parent { get; set; }

        /// <summary>
        /// Get the root script word of this instance.
        /// </summary>
        /// <returns>The root script word of this instance.</returns>
        public IBdoMetaData Root(int levelMax = 50)
        {
            return levelMax > 0 ? (Parent == null ? this : Parent.Root(levelMax--)) : null;
        }

        /// <summary>
        /// The value type of this instance.
        /// </summary>
        public DataValueTypes DataValueType { get; set; } = DataValueTypes.Any;

        /// <summary>
        /// The itemization mode of this instance.
        /// </summary>
        public DataValueMode ValueMode
        {
            get
            {
                if (_valueMode != DataValueMode.Any)
                    return _valueMode;
                else
                    return DataExpression != null ? DataValueMode.Expression : DataValueMode.Value;
            }
            set { _valueMode = value; }
        }

        /// <summary>
        /// The script of this instance.
        /// </summary>
        public IBdoExpression DataExpression { get; set; }

        // Specification -------------------------------

        /// <summary>
        /// Specification of this instance.
        /// </summary>
        public List<IBdoSpec> Specs { get; set; }

        // Specification ---------------------

        /// <summary>
        /// Gets a new specification.
        /// </summary>
        /// <returns>Returns the new specifcation.</returns>
        public IBdoSpec NewSpec()
        {
            if (this is IBdoMetaObject)
            {
                return BdoMeta.NewSpec<BdoObjectSpec>();
            }
            else if (this is IBdoMetaScalar)
            {
                return BdoMeta.NewSpec<BdoScalarSpec>();
            }

            return null;
        }

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param key="log">The log to populate.</param>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="varSet">The variable element set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        protected object DataObject(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            object obj = default;

            switch (ValueMode)
            {
                case DataValueMode.Value:
                    obj = _data;
                    break;
                case DataValueMode.Expression:
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

                        obj = scope.Interpreter.Evaluate<object>(DataExpression, varSet, log);
                    }
                    break;
            }

            return obj;
        }

        // Specification

        /// <summary>
        /// 
        /// </summary>
        /// <param key="name"></param>
        /// <returns></returns>
        public IBdoSpec GetSpec(string name = null)
        {
            return Specs?.FirstOrDefault(
                q => (name == null && q.Name == null) || q.Name.BdoKeyEquals(name));
        }

        /// <summary>
        /// 
        /// </summary>
        public IBdoMetaData WithSpecs(params IBdoSpec[] specs)
        {
            Specs = specs?.ToList();

            return this;
        }

        // Data

        /// <summary>
        /// Clears the item of this instance.
        /// </summary>
        public virtual void Clear()
        {
            _data = null;
        }

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param key="log">The log to populate.</param>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="varSet">The variable element set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public object GetData(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
            => DataObject(scope, varSet, log);

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param key="log">The log to populate.</param>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="varSet">The variable element set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public virtual T GetData<T>(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
            => DataObject(scope, varSet, log).As<T>();

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

            el.DataExpression = DataExpression?.Clone<BdoExpression>();
            el.Specs = Specs?.Select(q => q?.Clone<BdoSpec>())
                .Cast<IBdoSpec>().ToList();

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
        /// <param key="isDisposing">Indicates whether this instance is disposing</param>
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
