using BindOpen.Meta;
using BindOpen.Meta.Elements;
using BindOpen.Runtime.Scopes;
using System.IO;
using System.Xml.Schema;
using BindOpen.Logging;

namespace BindOpen.Runtime.Dtos.MetaData.Elements.Configuration
{
    /// <summary>
    /// This static class provides methods to handle configurations.
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Instantiates a new instance of Configuration class from a xml file.
        /// </summary>
        /// <param name="filePath">The file path to consider.</param>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="varElementSet">The set of script variables to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="xmlSchemaSet">The XML schema set to consider for checking.</param>
        /// <param name="mustFileExist">Indicates whether the file must exist.</param>
        /// <param name="isRuntimeUpdated">Indicates whether the runtime is updated.</param>
        /// <returns>The Xml operation project defined in the Xml file.</returns>
        public static T LoadXml<T>(
            string filePath,
            IBdoScope scope = null,
            IBdoElementSet varElementSet = null,
            IBdoLog log = null,
            XmlSchemaSet xmlSchemaSet = null,
            bool mustFileExist = true) where T : BdoConfigurationDto, new()
        {
            T unionConfiguration = new();

            if (XmlHelper.LoadXml<T>(filePath, log, xmlSchemaSet, mustFileExist) is T topConfiguration)
            {
                unionConfiguration.Update(topConfiguration);

                if (topConfiguration is BdoConfigurationDto topDynamicConfiguration)
                {
                    foreach (string usingFilePath in topDynamicConfiguration.UsingFilePaths)
                    {
                        string completeUsingFilePath = (usingFilePath.Contains(':') ?
                            usingFilePath :
                            Path.GetDirectoryName(filePath).EndingWith(@"\") + usingFilePath).ToPath();
                        if (LoadXml<T>(completeUsingFilePath, scope, varElementSet, log, xmlSchemaSet, mustFileExist) is T usingConfiguration)
                            unionConfiguration.Update(usingConfiguration);
                    }
                }
            }

            return unionConfiguration;
        }
    }
}
