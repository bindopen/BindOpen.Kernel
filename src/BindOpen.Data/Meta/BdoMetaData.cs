﻿using BindOpen.Data.Items;
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
        private DataItemizationMode _itemizationMode = DataItemizationMode.Any;

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
        // IBdoMetaData Implementation
        // --------------------------------------------------

        #region IBdoMetaData

        // Items --------------------------------------------

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
                else if (this is IBdoMetaList)
                    return BdoMetaDataKind.Set;
                return BdoMetaDataKind.None;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public IBdoMetaData Parent { get; set; }

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
                if (_itemizationMode != DataItemizationMode.Any)
                    return _itemizationMode;
                else if (Expression != null)
                    return DataItemizationMode.Expression;
                else if (Reference != null)
                    return DataItemizationMode.Reference;

                return DataItemizationMode.Value;
            }
            set { _itemizationMode = value; }
        }

        /// <summary>
        /// Item reference of this instance.
        /// </summary>
        public IBdoReference Reference { get; set; }

        /// <summary>
        /// The script of this instance.
        /// </summary>
        public IBdoExpression Expression { get; set; }

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
                return BdoMeta.NewSpec<BdoMetaScalarSpec>();
            }

            return null;
        }

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param name="log">The log to populate.</param>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="varSet">The variable element set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        protected object DataObject(
            IBdoScope scope = null,
            IBdoMetaList varSet = null,
            IBdoLog log = null)
        {
            object obj = default;

            switch (ItemizationMode)
            {
                case DataItemizationMode.Value:
                    obj = _data;
                    break;
                case DataItemizationMode.Reference:
                    if (Reference == null)
                    {
                        log?.AddWarning(title: "Reference missing");
                    }
                    obj = Reference.Get(scope, varSet, log);
                    break;
                case DataItemizationMode.Expression:
                    if (scope == null)
                    {
                        log?.AddWarning(title: "Application scope missing");
                    }
                    else
                    {
                        if (Expression == null)
                        {
                            log?.AddWarning(title: "Script missing");
                        }

                        var interpreter = scope.NewScriptInterpreter();
                        obj = interpreter.Evaluate<object>(Expression, varSet, log);
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
        /// <param name="log">The log to populate.</param>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="varSet">The variable element set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public object GetData(
            IBdoScope scope = null,
            IBdoMetaList varSet = null,
            IBdoLog log = null)
            => DataObject(scope, varSet, log);

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

            el.Reference = Reference?.Clone<BdoReference>();
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
