using System.IO;
using System.Xml.Schema;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Helpers.Serialization;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Application.Configuration.Serialization
{
    /// <summary>
    /// This static class provides methods to handle configurations.
    /// </summary>
    public static class ConfigurationDtoLoader
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
            Log log,
            IAppScope appScope = null,
            XmlSchemaSet xmlSchemaSet = null,
            bool mustFileExist = true) where T : ConfigurationDto, new()
        {
            T unionConfiguration = new T();

            T topConfiguration = XmlHelper.Load<T>(filePath, log, xmlSchemaSet, mustFileExist) as T;
            if (topConfiguration is UsableConfigurationDto topUsableConfiguration)
            {
                foreach (string usingFilePath in topUsableConfiguration.UsingFilePaths)
                {
                    string completeUsingFilePath = (usingFilePath.Contains(":") ?
                        usingFilePath :
                        Path.GetDirectoryName(filePath).GetEndedString(@"\") + usingFilePath).ToPath();
                    if (Load<T>(completeUsingFilePath, log, null, xmlSchemaSet, mustFileExist) is T usingConfiguration)
                        unionConfiguration.Update(usingConfiguration);
                }
            }

            unionConfiguration.CurrentFilePath = filePath;
            unionConfiguration.Update(topConfiguration);

            return unionConfiguration;
        }
    }
}
