using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Schema;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Helpers.Serialization;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Data.Items
{
    /// <summary>
    /// This class represents a data item.
    /// </summary>
    /// <remarks>The data item has only an ID, a creation and a last-modification dates.</remarks>
    [Serializable()]
    [XmlType("DataItem", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot("dataItem", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public abstract class DataItem : MarshalByRefObject, ICloneable, IDisposable
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DataItem class.
        /// </summary>
        public DataItem()
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

        /// <summary>
        /// Sets the specified value.
        /// </summary>
        /// <param name="objectType">The object type to consider.</param>
        /// <param name="propertyName">The property name to consider.</param>
        /// <param name="attributeTypes"></param>
        /// <param name="attribute">The attribute to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        protected PropertyInfo GetPropertyInfo(
            Type objectType,
            String propertyName,
            Type[] attributeTypes,
            ref DataElementAttribute attribute,
            IAppScope appScope = null)
        {
            PropertyInfo propertyInfo = null;

            if (objectType != null && propertyName != null)
            {
                propertyInfo = objectType.GetProperty(propertyName);
                if (propertyInfo != null)
                {
                    foreach (Type attributeType in attributeTypes)
                    {
                        attribute = propertyInfo.GetCustomAttribute(attributeType) as DataElementAttribute;
                        if (attribute != null)
                            break;
                    }
                }
            }

            return propertyInfo;
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
        public virtual void UpdateStorageInfo(Log log = null)
        {
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="log">The log to update.</param>
        public virtual void UpdateRuntimeInfo(IAppScope appScope = null,  Log log = null)
        {
        }

        /// <summary>
        /// Gets the xml string of this instance.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <returns>The Xml string of this instance.</returns>
        public String ToXml(Log log = null)
        {
            this.UpdateStorageInfo();
            return XmlHelper.ToXml(this, log);
        }

        /// <summary>
        /// Saves this instance to the specified file path.
        /// </summary>
        /// <param name="filePath">The path of the file to save.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>True if the saving operation has been done. False otherwise.</returns>
        public virtual Boolean SaveXml(String filePath, Log log = null)
        {
            this.UpdateStorageInfo(log);
            return XmlHelper.SaveXml(this, filePath, log);
        }

        /// <summary>
        /// Instantiates a new instance of Log class from a xml file.
        /// </summary>
        /// <param name="filePath">The path of the Xml file to load.</param>
        /// <param name="loadLog">The output log of the load task.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="xmlSchemaSet">The XML schema set to consider for checking.</param>
        /// <param name="mustFileExist">Indicates whether the file must exist.</param>
        /// <returns>The load log.</returns>
        public static T Load<T>(
            String filePath,
            Log loadLog = null,
            IAppScope appScope = null,
            XmlSchemaSet xmlSchemaSet = null,
            Boolean mustFileExist = true) where T : DataItem, new()
        {
            T dataItem = XmlHelper.Load<T>(filePath, loadLog, xmlSchemaSet, mustFileExist);
            dataItem?.UpdateRuntimeInfo(appScope, loadLog);

            return dataItem;
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
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>Log of the operation.</returns>
        /// <remarks>Put reference collections as null if you do not want to repair this instance.</remarks>
        public virtual Log Update<T>(
            T item = null,
            List<String> specificationAreas = null,
            List<UpdateMode> updateModes = null,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null) where T : DataItem
        {
            return new Log();
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <param name="updateModes">The update modes to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>Log of the operation.</returns>
        /// <remarks>Put reference collections as null if you do not want to repair this instance.</remarks>
        public Log Update(
            List<String> specificationAreas = null,
            List<UpdateMode> updateModes = null,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null)
        {
            return this.Update<DataItem>(null,specificationAreas, updateModes, appScope, scriptVariableSet);
        }

        /// <summary>
        /// Checks this instance.
        /// </summary>
        /// <typeparam name="T">The data item class to consider.</typeparam>
        /// <param name="isExistenceChecked">Indicates whether the carrier existence is checked.</param>
        /// <param name="item">The item to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>Returns the check log.</returns>
        public virtual Log Check<T>(
            Boolean isExistenceChecked = true,
            T item = null,
            List<String> specificationAreas = null,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null) where T : DataItem
        {
            return new Log();
        }

        /// <summary>
        /// Checks this instance.
        /// </summary>
        /// <param name="isExistenceChecked">Indicates whether the carrier existence is checked.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>Returns the check log.</returns>
        public virtual Log Check(
            Boolean isExistenceChecked = true,
            List<String> specificationAreas = null,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null)
        {
            return this.Check<DataItem>(isExistenceChecked, null, specificationAreas,appScope, scriptVariableSet);
        }

        /// <summary>
        /// Repairs this instance with the specified definition.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <param name="updateModes">The update modes to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>Log of the operation.</returns>
        public virtual Log Repair<T>(
            T item = null,
            List<String> specificationAreas = null,
            List<UpdateMode> updateModes = null,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null) where T : DataItem
        {
            return new Log();
        }

        /// <summary>
        /// Repairs this instance with the specified definition.
        /// </summary>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <param name="updateModes">The update modes to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>Log of the operation.</returns>
        public Log Repair(
            List<String> specificationAreas = null,
            List<UpdateMode> updateModes = null,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null)
        {
            return this.Repair<DataItem>(null, specificationAreas, updateModes, appScope, scriptVariableSet);
        }

        #endregion

        // --------------------------------------------------
        // IDisposable IMPLEMENTATION
        // --------------------------------------------------

        #region IDisposable_Implementation

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
