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
        // VARIABLES
        // --------------------------------------------------

        #region Variabes

        private DataReference _Reference;
        private T _Item;

        #endregion


        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The reference of this instance.
        /// </summary>
        [XmlElement("reference")]
        public DataReference Reference
        {
            get
            {
                return this._Reference;
            }
            set
            {
                this._Reference = value;
            }
        }

        /// <summary>
        /// Specification of the Reference property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean ReferenceSpecified
        {
            get
            {
                return this._Reference !=null;
            }
        }

        /// <summary>
        /// The item of this instance.
        /// </summary>
        [XmlIgnore()]
        public T Item
        {
            get
            {
                return this._Item;
            }
            set
            {
                this._Item = value;
            }
        }

        #endregion


        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Creates a new instance of the TBaseItemReference class.
        /// </summary>
        public TBaseItemReference()
        {
        }

        /// <summary>
        /// Creates a new instance of the TBaseItemReference class.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        public TBaseItemReference(T item)
        {
            this._Item = item;
        }

        /// <summary>
        /// Creates a new instance of the TBaseItemReference class.
        /// </summary>
        /// <param name="reference">The reference to consider.</param>
        public TBaseItemReference(DataReference reference)
        {
            this._Reference = reference;
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
            this._Item = this._Reference == null ? this._Item : this._Reference.Get(appScope, scriptVariableSet, log) as T;
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
            referencableItem.Item = this._Item.Clone() as T;
            referencableItem.Reference = this._Reference.Clone() as DataReference;

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

            if (this._Item != null)
                this._Item.UpdateStorageInfo(log);
            if (this._Reference != null)
                this._Reference.UpdateStorageInfo(log);
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(IAppScope appScope = null,  Log log = null)
        {
            base.UpdateRuntimeInfo(appScope, log);

            if (this._Item != null)
                this._Item.UpdateRuntimeInfo(appScope, log);
            if (this._Reference != null)
                this._Reference.UpdateRuntimeInfo(appScope, log);
        }

        #endregion

    }

}
