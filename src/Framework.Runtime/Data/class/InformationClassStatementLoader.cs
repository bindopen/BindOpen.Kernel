using cor_base_wdl.business.libraries;
using cor_base_wdl.data.carriers.database;
using cor_base_wdl.data.context;
using cor_base_wdl.data.information._class;
using cor_base_wdl.data.documents.format;
using cor_base_wdl.data.references;
using cor_base_wdl.system.script;
using cor_base_wdl.system.logging;
using cor_bizLibDatabase_blb.definition.connectors;
using cor_runtime_wdl.business.universe;
using System;

namespace cor_runtime_wdl.data._class
{

    /// <summary>
    /// This class represents a loader of an information class statement.
    /// </summary>
    public static class DataClassStatementLoader
    {
        // ------------------------------------------
        // LOADING
        // ------------------------------------------

        #region Loading

        /// <summary>
        /// Loads the object of the specified information configuration statement.
        /// </summary>
        /// <param name="aLog">The log of the task.</param>
        /// <param name="aInformationConfigurationStatement">The information configuration statement to load.</param>
        /// <param name="aRuntimeExtensionHandler">The runtime extension handler to use for loading.</param>
        /// <param name="aDataContext">The data context to use for loading.</param>
        /// <param name="aScriptInterpreter">The script interpreter to use for loading.</param>
        /// <param name="aDynamicDbConnectionManager">The database connector to use for loading.</param>
        /// <param name="aDbDataIdField">The Id field to use  for source Id mode.</param>
        /// <param name="aDbDataValueField">The value field to use for source Id mode.</param>
        /// <returns>The refered settings object of the specified information configuration.</returns>
        public static void LoadObjects(
            Log aLog,
            DataClassStatement aInformationConfigurationStatement,
            RuntimeExtensionHandler aRuntimeExtensionHandler,
            DataContext aDataContext,
            ScriptInterpreter aScriptInterpreter = null,
            DynamicDbConnectionManager aDynamicDbConnectionManager = null,
            DbField aDbDataIdField = null,
            DbField aDbDataValueField = null
            )
        {
            DataClassStatementLoader.LoadClassObject(
                aLog,
                aInformationConfigurationStatement,
                aRuntimeExtensionHandler,
                aDataContext,
                 aScriptInterpreter,
                 aDynamicDbConnectionManager,
                 aDbDataIdField,
                 aDbDataValueField);

            DataClassStatementLoader.LoadFormatObject(
                aLog,
                aInformationConfigurationStatement,
                aRuntimeExtensionHandler,
                aDataContext,
                 aScriptInterpreter,
                 aDynamicDbConnectionManager,
                 aDbDataIdField,
                 aDbDataValueField);
        }

        #endregion


        // ------------------------------------------
        // CLASS LOADING
        // ------------------------------------------

        #region Class_Loading

        /// <summary>
        /// Loads the kind object of the specified information configuration statement.
        /// </summary>
        /// <param name="aLog">The log of the task.</param>
        /// <param name="aDataClassStatement">The information class statement to load.</param>
        /// <param name="aRuntimeExtensionHandler">The runtime extension handler of this instance.</param>
        /// <param name="aDataContext">The data context to use for loading.</param>
        /// <param name="aScriptInterpreter">The script interpreter to use for loading.</param>
        /// <param name="aDynamicDbConnectionManager">The database connector to use for loading.</param>
        /// <param name="aDbDataIdField">The Id field to use  for source Id mode.</param>
        /// <param name="aDbDataValueField">The value field to use for source Id mode.</param>
        /// <returns>The refered settings object of the specified information configuration.</returns>
        public static void LoadClassObject(
            Log aLog,
            DataClassStatement aDataClassStatement,
            RuntimeExtensionHandler aRuntimeExtensionHandler,
            DataContext aDataContext,
            ScriptInterpreter aScriptInterpreter = null,
            DynamicDbConnectionManager aDynamicDbConnectionManager = null,
            DbField aDbDataIdField = null,
            DbField aDbDataValueField = null
            )
        {
            if ((aDataClassStatement == null) || (aDataClassStatement.ClassReference == null))
                return;

            if (aDataClassStatement.ClassReference.Kind == DataBindingKind.None)
                return;

            Type aObjectType = null;
            if (aRuntimeExtensionHandler != null)
                aObjectType = aRuntimeExtensionHandler.GetObjectType(
                    BusinessLibraryElementKind.DataClass,
                    aDataClassStatement.ClassUniqueName);

            if (aObjectType == null)
                aLog.AddError(
                    "Could not find the information kind whose ID is '" + aDataClassStatement.ClassUniqueName + "'.",
                    EventCriticality.High,
                    "",
                    "Could not find the information kind whose ID is '" + aDataClassStatement.ClassUniqueName + "' in runtime extension handler.",
                    "BUSINESSTASK_ENTITYNOTFOUND"
                    );
            else
                aDataClassStatement.ClassReference.Object = (DataClass)DataBindingLoader.Load(
                    aDataClassStatement.ClassReference,
                    aObjectType,
                    aLog,
                    aDataContext,
                    aScriptInterpreter,
                    aDynamicDbConnectionManager,
                    aDbDataIdField,
                    aDbDataValueField);

        }

        #endregion


        // ------------------------------------------
        // FORMAT LOADING
        // ------------------------------------------

        #region Format_Loading

        /// <summary>
        /// Loads the format object of the specified information configuration statement.
        /// </summary>
        /// <param name="aLog">The log of the task.</param>
        /// <param name="aDataClassStatement">The information class statement to load.</param>
        /// <param name="aRuntimeExtensionHandler">The runtime extension handler to use for loading.</param>
        /// <param name="aDataContext">The data context to use for loading.</param>
        /// <param name="aScriptInterpreter">The script interpreter to use for loading.</param>
        /// <param name="aDynamicDbConnectionManager">The database connector to use for loading.</param>
        /// <param name="aDbDataIdField">The Id field to use  for source Id mode.</param>
        /// <param name="aDbDataValueField">The value field to use for source Id mode.</param>
        /// <returns>The refered settings object of the specified information configuration.</returns>
        public static void LoadFormatObject(
            Log aLog,
            DataClassStatement aDataClassStatement,
            RuntimeExtensionHandler aRuntimeExtensionHandler,
            DataContext aDataContext,
            ScriptInterpreter aScriptInterpreter = null,
            DynamicDbConnectionManager aDynamicDbConnectionManager = null,
            DbField aDbDataIdField = null,
            DbField aDbDataValueField = null
            )
        {
            if ((aDataClassStatement == null) || (aDataClassStatement.FormatReference == null))
                return;

            if (aDataClassStatement.FormatReference.Kind == DataBindingKind.None)
                return;

            Type aObjectType = null;
            if (aRuntimeExtensionHandler != null)
                aObjectType = aRuntimeExtensionHandler.GetObjectType(
                    BusinessLibraryElementKind.DataFormat,
                    aDataClassStatement.FormatUniqueName);

            if (aObjectType == null)
                aLog.AddError(
                    "Could not find the information kind whose ID is '" + aDataClassStatement.ClassUniqueName + "'.",
                    EventCriticality.High,
                    "",
                    "Could not find the information kind whose ID is '" + aDataClassStatement.ClassUniqueName + "' in runtime extension handler.",
                    "BUSINESSTASK_ENTITYNOTFOUND"
                    );
            else
                aDataClassStatement.FormatReference.Object = (DataFormat)DataBindingLoader.Load(
                    aDataClassStatement.FormatReference,
                    aObjectType,
                    aLog,
                    aDataContext,
                    aScriptInterpreter,
                    aDynamicDbConnectionManager,
                    aDbDataIdField,
                    aDbDataValueField);
        }

        #endregion

    }
}