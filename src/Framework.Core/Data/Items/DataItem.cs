using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Data.Items
{
    /// <summary>
    /// This class represents a data item.
    /// </summary>
    /// <remarks>The data item has only an ID, a creation and a last-modification dates.</remarks>
    [Serializable()]
    [XmlType("DataItem", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot("dataItem", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public abstract class DataItem : MarshalByRefObject, IDataItem
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DataItem class.
        /// </summary>
        protected DataItem()
        {
        }

        /// <summary>
        /// Disposes this instance.
        /// </summary>
        ~DataItem()
        {
            this.Dispose(false);
        }

        #endregion

        // --------------------------------------------------
        // MUTATORS
        // --------------------------------------------------

        #region Mutators

        /// <summary>
        /// Initializes the life time service.
        /// </summary>
        /// <returns>Null to remain the object's life forever.</returns>
        public override object InitializeLifetimeService()
        {
            return null;
        }

        #endregion

        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns a cloned instance.</returns>
        public virtual Object Clone()
        {
            return this.MemberwiseClone() as DataItem;
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
        public virtual void UpdateStorageInfo(ILog log = null)
        {
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The set of script variables to consider.</param>
        /// <param name="log">The log to update.</param>
        public virtual void UpdateRuntimeInfo(IAppScope appScope = null, IScriptVariableSet scriptVariableSet = null, ILog log = null)
        {
        }

        #endregion

        // --------------------------------------------------
        // UPDATE, CHECK, REPAIR
        // --------------------------------------------------

        #region Update_Check_Repair

        /// <summary>
        /// Updates this instance.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <param name="updateModes">The update modes to consider.</param>
        /// <returns>The log of the operation.</returns>
        /// <remarks>Put reference collections as null if you do not want to repair this instance.</remarks>
        public virtual ILog Update<T>(
            T item = default,
            string[] specificationAreas = null,
            UpdateModes[] updateModes = null) where T : IDataItem
        {
            return new Log();
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <param name="updateModes">The update modes to consider.</param>
        /// <returns>The log of the operation.</returns>
        /// <remarks>Put reference collections as null if you do not want to repair this instance.</remarks>
        public ILog Update(
            string[] specificationAreas = null,
            UpdateModes[] updateModes = null)
        {
            return this.Update<DataItem>(null,specificationAreas, updateModes);
        }

        /// <summary>
        /// Checks this instance.
        /// </summary>
        /// <typeparam name="T">The data item class to consider.</typeparam>
        /// <param name="isExistenceChecked">Indicates whether the carrier existence is checked.</param>
        /// <param name="item">The item to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <returns>Returns the check log.</returns>
        public virtual ILog Check<T>(
            bool isExistenceChecked = true,
            T item = default,
            string[] specificationAreas = null) where T : IDataItem
        {
            return new Log();
        }

        /// <summary>
        /// Checks this instance.
        /// </summary>
        /// <param name="isExistenceChecked">Indicates whether the carrier existence is checked.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <returns>Returns the check log.</returns>
        public virtual ILog Check(
            bool isExistenceChecked = true,
            string[] specificationAreas = null)
        {
            return this.Check<DataItem>(isExistenceChecked, null, specificationAreas);
        }

        /// <summary>
        /// Repairs this instance with the specified definition.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <param name="updateModes">The update modes to consider.</param>
        /// <returns>The log of the operation.</returns>
        public virtual ILog Repair<T>(
            T item = default,
            string[] specificationAreas = null,
            UpdateModes[] updateModes = null) where T : IDataItem
        {
            return new Log();
        }

        /// <summary>
        /// Repairs this instance with the specified definition.
        /// </summary>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <param name="updateModes">The update modes to consider.</param>
        /// <returns>The log of the operation.</returns>
        public ILog Repair(
            string[] specificationAreas = null,
            UpdateModes[] updateModes = null)
        {
            return this.Repair<DataItem>(null, specificationAreas, updateModes);
        }

        #endregion

        // --------------------------------------------------
        // IDisposable IMPLEMENTATION
        // --------------------------------------------------

        #region IDisposable Implementation

        /// <summary>
        /// Indicates whether this instance is disposed.
        /// </summary>
        private bool IsDisposed = false;

        /// <summary>
        /// Disposes this instance.
        /// </summary>
        public virtual void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes specifying whether this instance is disposing.
        /// </summary>
        /// <param name="isDisposing">Indicates whether this instance is disposing</param>
        protected virtual void Dispose(bool isDisposing)
        {
            if (this.IsDisposed)
                return;

            if (isDisposing)
            {
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            this.IsDisposed = true;
        }

        #endregion
    }
}
