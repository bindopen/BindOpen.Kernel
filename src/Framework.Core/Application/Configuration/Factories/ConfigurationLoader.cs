using System.IO;
using System.Xml.Schema;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Helpers.Serialization;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Application.Configuration
{
    /// <summary>
    /// This static class provides methods to handle configurations.
    /// </summary>
    public static class ConfigurationLoader
    {
        /// <summary>
        /// Instantiates a new instance of Configuration class from a xml file.
        /// </summary>
        /// <param name="filePath">The file path to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="xmlSchemaSet">The XML schema set to consider for checking.</param>
        /// <param name="mustFileExist">Indicates whether the file must exist.</param>
        /// <returns>The Xml operation project defined in the Xml file.</returns>
        public new static T Load<T>(
            string filePath,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null,
            XmlSchemaSet xmlSchemaSet = null,
            bool mustFileExist = true,
            bool isRuntimeUpdated = true) where T : class, IBaseConfiguration, new()
        {
            T unionConfiguration = new T();

            T topConfiguration = XmlHelper.Load<T>(filePath, appScope, scriptVariableSet, log, xmlSchemaSet, mustFileExist, isRuntimeUpdated) as T;
            if (topConfiguration!=null)
            {
                unionConfiguration.Update(topConfiguration);

                if (topConfiguration is UsableConfiguration topUsableConfiguration)
                {

                    foreach (string usingFilePath in topUsableConfiguration.UsingFilePaths)
                    {
                        string completeUsingFilePath = (usingFilePath.Contains(":") ?
                            usingFilePath :
                            Path.GetDirectoryName(filePath).GetEndedString(@"\") + usingFilePath).ToPath();
                        if (Load<T>(completeUsingFilePath, appScope, scriptVariableSet, log, xmlSchemaSet, mustFileExist) is T usingConfiguration)
                            unionConfiguration.Update(usingConfiguration);
                    }
                }
            }

            unionConfiguration.CurrentFilePath = filePath;

            return unionConfiguration;
        }

        /// <summary>
        /// Adds the specified elements into the specified group.
        /// </summary>
        /// <param name="groupId">The ID of the group.</param>
        /// <param name="items">The items to add.</param>
        /// <returns>Returns this instance.</returns>
        public static T AddGroup<T>(this T configuration, string groupId, params IDataElement[] items)
            where T : class, IBaseConfiguration
        {
            return configuration?.AddGroup(groupId, items) as T;
        }
    }
}
