using System.Xml;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Data.References
{
    /// <summary>
    /// This class represents a base item reference.
    /// </summary>
    public abstract class TBaseItemReference<T> : DataItem, ITBaseItemReference<T> where T : IDataItem
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The reference of this instance.
        /// </summary>
        [XmlElement("reference")]
        public IDataReference Reference { get; set; }

        /// <summary>
        /// Specification of the Reference property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool ReferenceSpecified => this.Reference != null;

        /// <summary>
        /// The item of this instance.
        /// </summary>
        [XmlIgnore()]
        public T Item { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Creates a new instance of the TBaseItemReference class.
        /// </summary>
        protected TBaseItemReference()
        {
        }

        /// <summary>
        /// Creates a new instance of the TBaseItemReference class.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        protected TBaseItemReference(T item)
        {
            this.Item = item;
        }

        /// <summary>
        /// Creates a new instance of the TBaseItemReference class.
        /// </summary>
        /// <param name="reference">The reference to consider.</param>
        protected TBaseItemReference(IDataReference reference)
        {
            this.Reference = reference;
        }

        #endregion

        // --------------------------------------------------
        // MUTATORS
        // --------------------------------------------------

        #region Mutators

        /// <summary>
        /// Updates the items of this instance.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public virtual ILog Update(
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null)
        {
            ILog log = new Log();
            this.Item = this.Reference == null ? this.Item : (T)this.Reference.Get(appScope, scriptVariableSet, log);
            return log;
        }

        #endregion

        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        // Cloning ------------------------------------

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns the cloned job.</returns>
        public override object Clone()
        {
            TBaseItemReference<T> referencableItem = base.Clone() as TBaseItemReference<T>;
            referencableItem.Item = (T)this.Item.Clone();
            referencableItem.Reference = this.Reference.Clone() as DataReference;

            return referencableItem;
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
        public override void UpdateStorageInfo(ILog log = null)
        {
            base.UpdateStorageInfo(log);

            this.Item?.UpdateStorageInfo(log);
            this.Reference?.UpdateStorageInfo(log);
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(IAppScope appScope = null,  ILog log = null)
        {
            base.UpdateRuntimeInfo(appScope, log);

            this.Item?.UpdateRuntimeInfo(appScope, log);
            this.Reference?.UpdateRuntimeInfo(appScope, log);
        }

        #endregion
    }
}
