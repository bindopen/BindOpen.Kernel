using BindOpen.Application.Scopes;
using BindOpen.Data.Helpers.Files;
using BindOpen.Data.Helpers.Serialization;
using BindOpen.Data.Helpers.Strings;
using BindOpen.Data.Items;
using BindOpen.System.Diagnostics;
using BindOpen.System.Scripting;
using System.IO;
using System.Xml.Schema;

namespace BindOpen.Application.Configuration
{
    /// <summary>
    /// This static class provides methods to handle configurations.
    /// </summary>
    public static partial class ConfigurationFactory
    {
        /// <summary>
        /// Instantiates a new instance of Configuration class from a xml file.
        /// </summary>
        /// <param name="filePath">The file path to consider.</param>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The set of script variables to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="xmlSchemaSet">The XML schema set to consider for checking.</param>
        /// <param name="mustFileExist">Indicates whether the file must exist.</param>
        /// <param name="isRuntimeUpdated">Indicates whether the runtime is updated.</param>
        /// <returns>The Xml operation project defined in the Xml file.</returns>
        public static T Load<T>(
            string filePath,
            IBdoScope scope = null,
            IScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null,
            XmlSchemaSet xmlSchemaSet = null,
            bool mustFileExist = true,
            bool isRuntimeUpdated = true) where T : class, IBdoBaseConfiguration, new()
        {
            T unionConfiguration = new T();

            if (XmlHelper.Load<T>(filePath, scope, scriptVariableSet, log, xmlSchemaSet, mustFileExist, isRuntimeUpdated) is T topConfiguration)
            {
                unionConfiguration.Update(topConfiguration);

                if (topConfiguration is BdoUsableConfiguration topUsableConfiguration)
                {

                    foreach (string usingFilePath in topUsableConfiguration.UsingFilePaths)
                    {
                        string completeUsingFilePath = (usingFilePath.Contains(":") ?
                            usingFilePath :
                            Path.GetDirectoryName(filePath).GetEndedString(@"\") + usingFilePath).ToPath();
                        if (Load<T>(completeUsingFilePath, scope, scriptVariableSet, log, xmlSchemaSet, mustFileExist) is T usingConfiguration)
                            unionConfiguration.Update(usingConfiguration);
                    }
                }
            }

            unionConfiguration.CurrentFilePath = filePath;

            return unionConfiguration;
        }

        /// <summary>
        /// Instantiates a new instance of the ConfigurationBundle class.
        /// </summary>
        /// <param name="values">The values to consider.</param>
        public static BdoConfigurationBundle CreateBundle(params IDataKeyValue[] values)
        {
            BdoConfigurationBundle bundle = new BdoConfigurationBundle();
            foreach (DataKeyValue value in values)
            {
                if (value != null)
                {
                    bundle.Add(value.Key, value.Content);
                }
            }

            return bundle;
        }
    }
}
