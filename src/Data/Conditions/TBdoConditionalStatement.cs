using BindOpen.Kernel.Data.Meta;
using BindOpen.Kernel.Logging;
using BindOpen.Kernel.Scoping;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Kernel.Data.Conditions
{
    /// <summary>
    /// 
    /// </summary>
    public class TBdoConditionalStatement<TItem> : List<(TItem Item, IBdoCondition Condition)>,
        ITBdoConditionalStatement<TItem>
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The value that expresses that the condition is satisfied.
        /// </summary>
        public string Id { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the Condition class.
        /// </summary>
        public TBdoConditionalStatement()
        {
        }

        #endregion

        // ------------------------------------------
        // ITBdoConditionalStatement
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the Condition class.
        /// </summary>
        public TItem GetItem(IBdoScope scope = null, IBdoMetaSet varSet = null, IBdoLog log = null)
        {
            foreach (var (Item, Condition) in this)
            {
                if (Condition != null && scope.Evaluate(Condition, varSet, log))
                {
                    return Item;
                }
            }

            return this.FirstOrDefault(q => q.Condition == null).Item;
        }

        #endregion

        // ------------------------------------------
        // IReferenced Implementation
        // ------------------------------------------

        #region IReferenced

        public string Key() => Id;

        #endregion

        // --------------------------------------------------
        // IClonable
        // --------------------------------------------------

        #region IClonable

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns a cloned instance.</returns>
        public virtual object Clone()
        {
            var obj = BdoData.NewStatement(this?.ToArray());

            return obj;
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <param key="areas">The areas to consider.</param>
        /// <returns>Returns a cloned instance.</returns>
        public T Clone<T>() where T : class
        {
            return Clone() as T;
        }

        #endregion

        // ------------------------------------------
        // IDISPOSABLE METHODS
        // ------------------------------------------

        #region IDisposable_Methods

        /// <summary>
        /// Disposes this instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _isDisposed = false;

        /// <summary>
        /// Disposes specifying whether this instance is disposing.
        /// </summary>
        /// <param key="isDisposing">Indicates whether this instance is disposing</param>
        protected virtual void Dispose(bool isDisposing)
        {
            if (_isDisposed)
            {
                return;
            }

            _isDisposed = true;
        }

        #endregion
    }
}