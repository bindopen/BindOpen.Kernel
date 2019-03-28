using System;
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
    public abstract class TBaseItemReference<T> : DataItem where T : DataItem
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The reference of this instance.
        /// </summary>
        [XmlElement("reference")]
        public DataReference Reference { get; set; }

        /// <summary>
        /// Specification of the Reference property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean ReferenceSpecified
        {
            get
            {
                return this.Reference !=null;
            }
        }

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
        protected TBaseItemReference(DataReference reference)
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
        public virtual Log Update(
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null)
        {
            Log log = new Log();
            this.Item = this.Reference == null ? this.Item : this.Reference.Get(appScope, scriptVariableSet, log) as T;
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
        public override Object Clone()
        {
            TBaseItemReference<T> referencableItem = base.Clone() as TBaseItemReference<T>;
            referencableItem.Item = this.Item.Clone() as T;
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
        public override void UpdateStorageInfo(Log log = null)
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
        public override void UpdateRuntimeInfo(IAppScope appScope = null,  Log log = null)
        {
            base.UpdateRuntimeInfo(appScope, log);

            this.Item?.UpdateRuntimeInfo(appScope, log);
            this.Reference?.UpdateRuntimeInfo(appScope, log);
        }

        #endregion
    }
}
