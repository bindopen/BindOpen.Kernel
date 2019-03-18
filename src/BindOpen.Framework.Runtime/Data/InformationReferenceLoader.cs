using cor_base_wdl.data.connectors;
using cor_base_wdl.data.carriers.database;
using cor_base_wdl.data.carriers.database.query;
using cor_base_wdl.data.context;
using cor_base_wdl.data.references;
using cor_base_wdl.system.script;
using cor_base_wdl.system.logging;
using cor_bizLibDatabase_blb.definition.connectors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace dkm.runtime.data
{

    /// <summary>
    /// This class represents a loader of an information reference.
    /// </summary>
    public static class DataBindingLoader
    {

        // ------------------------------------------
        // LOAD
        // ------------------------------------------

        #region Load

        /// <summary>
        /// Loads the refered object of the specified information reference.
        /// </summary>
        /// <param name="aDataBinding">The information reference to load.</param>
        /// <param name="aObjectType">The type of the output information object.</param>
        /// <param name="aLog">The log of the task.</param>
        /// <param name="aDataContext">The data context to use for loading.</param>
        /// <param name="aScriptInterpreter">The script interpreter to use for loading.</param>
        /// <param name="aDynamicDbConnectionManager">The database connector to use for loading.</param>
        /// <param name="aDbDataIdField">The Id field to use  for source Id mode.</param>
        /// <param name="aDbDataValueField">The value field to use for source Id mode.</param>
        /// <param name="aScriptVariableSet">The script variable set to consider.</param>
        /// <returns>The refered object of the specified information reference.</returns>
        public static Object Load(
            this DataBinding aDataBinding,
            Type aObjectType,
            Log aLog,
            DataContext aDataContext,
            ScriptInterpreter aScriptInterpreter = null,
            DynamicDbConnectionManager aDynamicDbConnectionManager = null,
            DbField aDbDataIdField=null,
            DbField aDbDataValueField = null,
            ScriptVariableSet aScriptVariableSet=null
            )
        {
            Object aObject = null;

            if (aDataBinding == null)
                return null;

            XmlSerializer aXmlSerializer = null;
            StreamReader aStreamReader = null;
            StringReader aStringReader = null;
            String aXmlString = "";
            String aXmlFilePath = "";

            try
            {
                switch (aDataBinding.Kind)
                {
                    case DataBindingKind.XmlFile:
                        if (aDataBinding.FilePath != null)
                        {
                            String aFilePath = (new DataExpression(aDataBinding.FilePath)).GetValue(
                                DataExpressionKind.Script,
                                aScriptInterpreter,
                                null);
                            if (System.IO.File.Exists(aFilePath))
                            {
                                try
                                {
                                    aXmlSerializer = new XmlSerializer(aObjectType);
                                    aStreamReader = new StreamReader(aFilePath);
                                    aObject = aXmlSerializer.Deserialize(XmlReader.Create(aStreamReader));
                                }
                                catch
                                {
                                }
                                finally
                                {
                                    if (aStreamReader != null)
                                        aStreamReader.Close();
                                }
                            }
                        }
                        break;
                    case DataBindingKind.TextFile:
                        if (aDataBinding.FilePath != null)
                        {
                            String aFilePath = (new DataExpression(aDataBinding.FilePath)).GetValue(
                                DataExpressionKind.Script,
                                aScriptInterpreter,
                                null);
                            if (System.IO.File.Exists(aFilePath))
                            {
                                    aStreamReader = new StreamReader(aFilePath);
                                    String aValue = aStreamReader.ReadToEnd();
                                    if (aObjectType.Namespace.StartsWith("System"))
                                        aObject = aValue;
                                    else
                                    {
                                        System.Reflection.MethodInfo aMethodInfo = aObjectType.GetMethod(
                                            "LoadFromTextString",
                                            BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy, 
                                            null, new Type[1] { typeof(String) }, null);
                                        if (aMethodInfo != null)
                                            aObject = aMethodInfo.Invoke(null, new String[1] { aValue });
                                    }
                            }
                        }
                        break;
                    case DataBindingKind.XmlString:
                        aXmlSerializer = new XmlSerializer(aObjectType);
                        aStringReader = new StringReader(aDataBinding.DataString);
                        aObject = aXmlSerializer.Deserialize(XmlReader.Create(aStringReader));
                        break;
                    case DataBindingKind.TextString:                           
                        if (aObjectType.Namespace.StartsWith("System"))
                            aObject = aDataBinding.DataString;
                        else
                        {
                            System.Reflection.MethodInfo aMethodInfo = aObjectType.GetMethod("LoadFromTextString", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy, null, new Type[1] { typeof(String) }, null);
                            if (aMethodInfo != null)
                                aObject = aMethodInfo.Invoke(null, new String[1] { aDataBinding.DataString });
                        }
                        break;
                    case DataBindingKind.Database:
                        aXmlString = "";

                        if (aDataBinding.DataQuery != null)
                            if ((aDynamicDbConnectionManager != null) &&
                                (!aDynamicDbConnectionManager.OpenDataModule(
                                    aDataBinding.DataQuery.DataModule, BasicConnectorDefinitionName.OleDb).HasErrorOrException()))
                            {
                                OleDbDataReader aOleDbDataReader = null;
                                aLog.AddEvents(aDynamicDbConnectionManager.ExecuteDbDataQuery(aDataBinding.DataQuery, ref aOleDbDataReader));
                                if (!aLog.HasErrorOrException())
                                    if (aOleDbDataReader.Read())
                                    {
                                        aXmlString = aOleDbDataReader[aDataBinding.DataFieldName].ToString();

                                        // then we instantiate the object
                                        aXmlSerializer = new XmlSerializer(aObjectType);
                                        aStringReader = new StringReader(aXmlString);
                                        aObject = aXmlSerializer.Deserialize(XmlReader.Create(aStringReader));
                                    }
                                aDynamicDbConnectionManager.Close();
                            }
                        break;
                    case DataBindingKind.XmlFileReferenceInDatabase:
                        aXmlFilePath = "";

                        if (aDataBinding.DataQuery != null)
                            if ((aDynamicDbConnectionManager != null) &&
                                (!aDynamicDbConnectionManager.OpenDataModule(
                                    aDataBinding.DataQuery.DataModule, BasicConnectorDefinitionName.OleDb).HasErrorOrException()))
                            {
                                OleDbDataReader aOleDbDataReader = null;
                                aLog.AddEvents(aDynamicDbConnectionManager.ExecuteDbDataQuery(aDataBinding.DataQuery, ref aOleDbDataReader));
                                if (!aLog.HasErrorOrException())
                                    if (aOleDbDataReader.Read())
                                    {
                                        aXmlFilePath = aOleDbDataReader[aDataBinding.DataFieldName].ToString();
                                        if (System.IO.File.Exists(aXmlFilePath))
                                        {
                                            // then we instantiate the object
                                            aXmlSerializer = new XmlSerializer(aObjectType);
                                            aStreamReader = new StreamReader(aXmlFilePath);
                                            aObject = aXmlSerializer.Deserialize(XmlReader.Create(aStreamReader));
                                        }
                                    }
                            }
                        break;
                    case DataBindingKind.DynamicDataContext:
                        if (aDataContext != null)
                            if ((aDataBinding.ObjectId!=null)&(!String.IsNullOrEmpty(aDataBinding.DataBusinessEntity)))
                            aObject = aDataContext.GetDynamicElement(aDataBinding.ObjectId, aDataBinding.DataBusinessEntity);
                        break;
                    case DataBindingKind.SourceId:
                        if (aDataBinding.DataQuery != null)
                            if ((aDbDataIdField != null) &&
                                (aDbDataValueField != null) &&
                                (aDynamicDbConnectionManager != null) &&
                                (!aDynamicDbConnectionManager.OpenDataModule(
                                    aDataBinding.DataQuery.DataModule, BasicConnectorDefinitionName.OleDb).HasErrorOrException()))
                            {
                                OleDbDataReader aOleDbDataReader = null;

                                DbDataQuerySimple aDbDataQuerySimple = new DbDataQuerySimple()
                                {
                                    DataModule = aDbDataValueField.DataModule,
                                    Name = aDbDataValueField.Name,
                                    IdFields = new List<DbField>()
                                 {
                                     aDbDataIdField
                                 }
                                };

                                aLog.AddEvents(aDynamicDbConnectionManager.ExecuteDbDataQuery(aDataBinding.DataQuery, ref aOleDbDataReader));
                                if (!aLog.HasErrorOrException())
                                    if (aOleDbDataReader.Read())
                                    {
                                        aXmlString = aOleDbDataReader[aDbDataValueField.GetName()].ToString();

                                        // then we instantiate the object
                                        aXmlSerializer = new XmlSerializer(aObjectType);
                                        aStringReader = new StringReader(aXmlString);
                                        aObject = aXmlSerializer.Deserialize(XmlReader.Create(aStringReader));
                                    }
                            }
                        break;
                    case DataBindingKind.DkMScript:
                        aObject = new DataExpression("", aDataBinding.Script).GetValue(DataExpressionKind.Script, aScriptInterpreter, aScriptVariableSet);

                        break;
                    case DataBindingKind.FormatedDkMScript:
                        String[] someParams = (aDataBinding==null ? new string[1] { "" } : aDataBinding.ObjectId.Split(';'));
                        String aScript = String.Format(aDataBinding.Script, someParams);
                        aObject = new DataExpression("", aScript).Evaluate(aScriptInterpreter, ref aLog, aScriptVariableSet);

                        break;
                }
            }
            catch (Exception ex)
            {
                aLog.AddException(
                    ex,
                    EventCriticality.High,
                    ""
                    );
            }
            finally
            {
                if (aStreamReader != null)
                    aStreamReader.Close();
                if (aStringReader != null)
                    aStringReader.Close();
                //if (aDynamicDbConnectionManager != null)
                //    aDynamicDbConnectionManager.CloseConnection();
            }

            return aObject;
        }


        #endregion

    }
}